using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace kaputt.UI {
public class MainMenu : NetworkBehaviour
{
    public void StartHost(){
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("Sandbox", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void StartClient(){
        NetworkManager.Singleton.StartClient();
    }
}
}