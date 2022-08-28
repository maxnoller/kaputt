using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kaputt.Character{
public class CursorController : MonoBehaviour
{
    bool cursorVisible = true;

    void Start(){
        setCursorVisibility(false);
    }

    public void setCursorVisibility(bool visible){
        cursorVisible = visible;
        Cursor.visible = cursorVisible;
        if(cursorVisible){
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
}