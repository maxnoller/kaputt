using UnityEngine;
using Unity.Netcode;
using NOBRAIN.KAPUTT.Gameplay.Movement;
using kaputt.Character.Input;

namespace NOBRAIN.KAPUTT.Gameplay {
    [RequireComponent(typeof(NetworkMovementComponent))]
    public class PlayerController : NetworkBehaviour {
        Vector2 moveInput = new Vector2();
        Vector2 lookInput = new Vector2();
        MainInput input;
        [SerializeField]NetworkMovementComponent _playerMovement;
        [SerializeField]Animator _animator;

        void Awake() {
            input = new MainInput();
            input.Foot.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            input.Foot.Movement.canceled += ctx => moveInput = Vector2.zero;
            input.Foot.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            input.Foot.Look.canceled += ctx => lookInput = Vector2.zero;

            _animator = GetComponent<Animator>();
        }

        public override void OnNetworkSpawn() {
            if(IsLocalPlayer){
                input.Foot.Enable();
            }
        }

        void Start(){
            _playerMovement = GetComponent<NetworkMovementComponent>();
        }

        void FixedUpdate(){
            if(IsClient || IsServer){
                _playerMovement.ProcessLocalPlayerMovement(moveInput, lookInput);
            } else {
                _playerMovement.ProcessSimulatedPlayerMovement();
            }
        }
    }
}