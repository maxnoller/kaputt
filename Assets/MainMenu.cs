using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using VContainer;
using NOBRAIN.KAPUTT.ConnectionManagement;

public class MainMenu : MonoBehaviour
{
    [Inject] ConnectionManager m_ConnectionManager;
    public void StartHost(){
        m_ConnectionManager.StartHostIp("test", "127.0.0.1", 7777);
    }

    void Update(){
    }

    public void StartClient(){
        NetworkManager.Singleton.StartClient();
    }
}