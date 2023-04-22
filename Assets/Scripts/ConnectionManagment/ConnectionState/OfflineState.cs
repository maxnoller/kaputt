using System;
using UnityEngine;
using NOBRAIN.KAPUTT.Utils;
using UnityEngine.SceneManagement;
using VContainer;
using Steamworks;
using Steamworks.Data;

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
            SteamFriends.OnGameLobbyJoinRequested += OnGameLobbyJoinRequested;
        }

        public override void Exit() {
            SteamFriends.OnGameLobbyJoinRequested -= OnGameLobbyJoinRequested;
         }

        private async void OnGameLobbyJoinRequested(Lobby _lobby, SteamId steamId){
            var connectionMethod = new ConnectionMethodSteam(m_ConnectionManager);
            connectionMethod.setLobby(_lobby);
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_ClientConnecting.Configure(connectionMethod));

        }

        public override void StartServer(string playerName, string ipaddress, int port)
        {
            var connectionMethod = new ConnectionMethodSteam(m_ConnectionManager);
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_StartingHost.Configure(connectionMethod));
        }
    }
}