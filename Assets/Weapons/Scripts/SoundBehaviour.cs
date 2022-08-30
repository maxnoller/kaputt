using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace kaputt.Weapons{

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(IShootBehaviour))]
public class SoundBehaviour : NetworkBehaviour
{
    IShootBehaviour shootBehaviour;

    AudioSource audioSource;

    void Start(){
        shootBehaviour = GetComponent<IShootBehaviour>();
        shootBehaviour.OnShotFired += () => handleShotFired();
        audioSource = GetComponent<AudioSource>();
    }

    public void handleShotFired(){
        handleShotSoundClientRpc();
    }

    [ClientRpc]
    void handleShotSoundClientRpc(){
        audioSource.Play();
    }


}
}
