using System;
using UnityEngine;
using NOBRAIN.KAPUTT.Utils;
using UnityEngine.SceneManagement;
using VContainer;

namespace NOBRAIN.KAPUTT.ConnectionManagement
{
    class OfflineState : ConnectionState
    {

        const string k_MainMenuSceneName = "MainMenu";

        public override void Enter()
        {
            m_ConnectionManager.NetworkManager.Shutdown();
            if (SceneManager.GetActiveScene().name != k_MainMenuSceneName)
            {
                SceneLoaderWrapper.Instance.LoadScene(k_MainMenuSceneName, useNetworkSceneManager: false);
            }
        }

        public override void Exit() { }

        public override void StartClient(string playerName, string ipaddress, int port)
        {
            var connectionMethod = new ConnectionMethodIP(ipaddress, (ushort)port, m_ConnectionManager, playerName);
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_ClientConnecting.Configure(connectionMethod));
        }

        public override void StartServer(string ipaddress, int port)
        {
            var connectionMethod = new ConnectionMethodIP(ipaddress, (ushort)port, m_ConnectionManager, "");
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_StartingServer.Configure(connectionMethod));
        }

        public override void StartHost(string ipaddress, int port)
        {
            var connectionMethod = new ConnectionMethodIP(ipaddress, (ushort)port, m_ConnectionManager, "");
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_StartingHost.Configure(connectionMethod));
        }
    }
}