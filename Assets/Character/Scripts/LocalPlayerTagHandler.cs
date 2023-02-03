using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace kaputt.Character{
public class LocalPlayerTagHandler : NetworkBehaviour
{
    void Start(){
        if(!IsLocalPlayer) return;
        gameObject.tag = "Player";
    }   
}
}
