using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using kaputt.Character.Input;

namespace kaputt.Character{
public class MovementController : NetworkBehaviour {
    [SerializeField]float jumpHeight = 20;
    [SerializeField]float gravity = 9.81f;
    [SerializeField]float stepDown = 0.3f;
    [SerializeField]float strafeSpeed = 1f;

    //Input
    MainInput input;
    Vector2 currentMovement;
    bool runPressed;
    //Component References
    CharacterController characterController;
    Animator animator;

    Vector3 rootMotion;
    Vector3 velocity;
    bool isJumping;

    void Awake(){
        input = new MainInput();

        input.Foot.Movement.performed += ctx => {
            currentMovement = ctx.ReadValue<Vector2>();
        };
        input.Foot.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        input.Foot.Movement.canceled += ctx => currentMovement = Vector2.zero;
        input.Foot.Jump.performed += ctx => jump();
    }

    void Start(){
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

	public override void OnNetworkSpawn(){
		enabled = true;
	}

    void Update(){
        if(IsLocalPlayer)
            SetMovementServerRpc(currentMovement, runPressed);
    }
    void OnEnable(){
        if(IsLocalPlayer)
            input.Foot.Enable();
    }
    void OnDisable(){
        if(IsLocalPlayer)
            input.Foot.Disable();
    }

    [ServerRpc]
    public void SetMovementServerRpc(Vector2 movement, bool runPressed){
        handleMovement(movement, runPressed);
    }

    void handleMovement(Vector2 movement, bool run){
        if(!IsServer) {return;}
        float y = movement.y/2;
        if(run){
            y = y*2;
        }
        animator.SetFloat("speed_y", y, 0.05f, Time.deltaTime);
        animator.SetFloat("speed_x", movement.x, 0.05f, Time.deltaTime);
    }

    private void OnAnimatorMove(){
        rootMotion += animator.deltaPosition;
    }

    void FixedUpdate(){
        if(isJumping){
            performAirMovement();
        } else {
            performMovement();
        }
        rootMotion = Vector3.zero;
    }

    void performMovement(){
        characterController.Move(rootMotion + Vector3.down * stepDown);
        if(!characterController.isGrounded){
            isJumping = true;
            velocity = animator.velocity;
            velocity.y = 0;
        }
    }

    void performAirMovement(){
        velocity.y -= gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.fixedDeltaTime + strafe());
        isJumping = !characterController.isGrounded;
    }

    void jump(){
        if(!isJumping){
            isJumping = true;
            velocity = animator.velocity;
            velocity.y = Mathf.Sqrt(2*gravity*jumpHeight);
        }
    }

    Vector3 strafe(){
        return (transform.right * currentMovement.x) * (strafeSpeed);
    }
}
}