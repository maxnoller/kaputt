using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using kaputt.Character.Input;

namespace kaputt.Character{
public class CameraController : NetworkBehaviour
{
    [SerializeField]GameObject mainCamera;
    [SerializeField]float mouseSensitivity = 1f;
    [SerializeField]float maxRotation = 50f;
    private float currentXRotation = 0;
    private float currentYRotation = 0;

    private Vector2 currentRotation;
    MainInput input;
    void Awake(){
        input = new MainInput();

        input.Foot.Look.performed += ctx => {
            currentRotation = ctx.ReadValue<Vector2>();
        };
    }

    void Update(){
        ChangeRotation(currentRotation);
    }

	public override void OnNetworkSpawn()
	{
        enabled = true;
        if(IsLocalPlayer)
            mainCamera.SetActive(true);
	}

    void OnEnable(){
        if(IsLocalPlayer)
            input.Foot.Enable();
    }

    void OnDisable(){
        if(IsLocalPlayer)
            input.Foot.Disable();
    }

    void ChangeRotation(Vector2 mouseInput){
        if(currentRotation == null) { return; }
        currentXRotation -= mouseInput.y*mouseSensitivity;
        currentYRotation += mouseInput.x * mouseSensitivity;
        currentXRotation = Mathf.Clamp(currentXRotation, -maxRotation, maxRotation);
        mainCamera.transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }
}
}