using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using NOBRAIN.KAPUTT.ConnectionManagement;
using VContainer;

public class MainMenu : MonoBehaviour
{
    [Inject] ConnectionManager m_ConnectionManager;
    public void StartHost(){
        m_ConnectionManager.StartHost("127.0.0.1", 7777);
    }

    void Start(){
        if(!isThisServer())
            return;
        Debug.Log("Starting Dedicated Server");
        m_ConnectionManager.StartServer("0.0.0.0", 7777);
        return;
    }

    bool isThisServer(){
        foreach(string arg in System.Environment.GetCommandLineArgs()){
            if(arg == "-dedicatedServer")
                return true;
        }
        Debug.Log("Not dedicated server");
        return false;
    }

    void Update(){
    }

    public void StartClient(){
        m_ConnectionManager.StartClientIp("test2", "127.0.0.1", 7777);
    }
}
