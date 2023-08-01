using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraHandler : NetworkBehaviour
{
    [SerializeField] GameObject mainCamera;
    public override void OnNetworkSpawn(){
        if(IsLocalPlayer){
            mainCamera.SetActive(true);
        }
    }
}
