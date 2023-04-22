using System;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using NOBRAIN.KAPUTT.Authentication;
using Steamworks;
using Steamworks.Data;
using Netcode.Transports.Facepunch;

namespace NOBRAIN.KAPUTT.ConnectionManagement
{
    public abstract class ConnectionMethodBase
    {
        protected ConnectionManager m_ConnectionManager;
        protected readonly ulong m_steamid;

        public abstract Task SetupServerConnectionAsync();

        public abstract Task SetupClientConnectionAsync();

        public ConnectionMethodBase(ConnectionManager connectionManager)
        {
            m_ConnectionManager = connectionManager;
        }

        protected void SetConnectionPayload(string playerId, string playerName, SteamId steamId)
        {
            var payload = JsonUtility.ToJson(new ConnectionPayload()
            {
                playerId = playerId,
                steamId = steamId,
                playerName = playerName,
                isDebug = Debug.isDebugBuild
            });

            var payloadBytes = System.Text.Encoding.UTF8.GetBytes(payload);

            m_ConnectionManager.NetworkManager.NetworkConfig.ConnectionData = payloadBytes;
        }

        protected string GetPlayerId()
        {
           //return Authentication.Authentication.Instance.PlayerId();
           return CreateRandomString();
        }

        private string CreateRandomString(int stringLength = 10) {
        int _stringLength = stringLength - 1;
        string randomString = "";
        string[] characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        for (int i = 0; i <= _stringLength; i++) {
            randomString = randomString + characters[UnityEngine.Random.Range(0, characters.Length)];
        }
        return randomString;
    }
    }

    class ConnectionMethodSteam : ConnectionMethodBase {

        private FacepunchTransport transport = null;
        public Lobby? currentLobby {get; private set;} = null;
        
        public ConnectionMethodSteam(ConnectionManager connectionManager) : base (connectionManager){
            transport = connectionManager.NetworkManager.GetComponent<FacepunchTransport>();
        }

        public void setLobby(Lobby _lobby){
            currentLobby = _lobby;
        }

        public override async Task SetupClientConnectionAsync(){
            transport.targetSteamId = currentLobby.Value.Owner.Id;
        }

        public override async Task SetupServerConnectionAsync(){
            currentLobby = await SteamMatchmaking.CreateLobbyAsync(10);
        }

    }

}