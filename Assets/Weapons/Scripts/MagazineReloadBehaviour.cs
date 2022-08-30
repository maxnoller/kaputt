using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using kaputt.Character.Input;

namespace kaputt.Weapons {

[RequireComponent(typeof(IShootBehaviour))]
public class MagazineReloadBehaviour : NetworkBehaviour
{
    [SerializeField]int maxAmmo;
    [SerializeField]int ammoPerMagazine;
    
    [SerializeField]NetworkVariable<int> currentTotalAmmo = new NetworkVariable<int>();
    [SerializeField]NetworkVariable<int> currentAmmoInMagazine = new NetworkVariable<int>();
    IShootBehaviour shootBehaviour;
    MainInput input;

    void Awake(){
        input = new MainInput();
        input.Foot.Reload.performed += ctx => reloadServerRpc();
        input.Foot.Enable();
    }

    void Start(){
        shootBehaviour = GetComponent<IShootBehaviour>();
        shootBehaviour.OnShotFired += () => handleShotFired();

        currentTotalAmmo.Value = maxAmmo;
        currentAmmoInMagazine.Value = ammoPerMagazine;
        
    }

    void handleShotFired(){
        currentAmmoInMagazine.Value -= 1;
        if(currentAmmoInMagazine.Value == 0){
            shootBehaviour.canShoot.Value = false;
        }
    }

    [ServerRpc]
    void reloadServerRpc(){
        tryReload();
    }

    void tryReload(){
        if(currentAmmoInMagazine.Value >= ammoPerMagazine || currentTotalAmmo.Value == 0){
            return;
        }

        int difference = ammoPerMagazine - currentAmmoInMagazine.Value;
        if(currentTotalAmmo.Value >= difference){
            currentTotalAmmo.Value -= difference;
            currentAmmoInMagazine.Value = ammoPerMagazine;
        } else {
            currentAmmoInMagazine.Value += currentTotalAmmo.Value;
            currentTotalAmmo.Value = 0;
        }
        shootBehaviour.canShoot.Value = true;

    }


}
}