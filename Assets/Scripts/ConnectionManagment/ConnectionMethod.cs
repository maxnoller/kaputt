using System;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using NOBRAIN.KAPUTT.Authentication;

namespace NOBRAIN.KAPUTT.ConnectionManagement
{
    public abstract class ConnectionMethodBase
    {
        protected ConnectionManager m_ConnectionManager;
        protected readonly string m_PlayerName;

        public abstract Task SetupServerConnectionAsync();

        public abstract Task SetupClientConnectionAsync();

        public ConnectionMethodBase(ConnectionManager connectionManager, string playerName)
        {
            m_ConnectionManager = connectionManager;
            m_PlayerName = playerName;
        }

        protected void SetConnectionPayload(string playerId, string playerName)
        {
            var payload = JsonUtility.ToJson(new ConnectionPayload()
            {
                playerName = playerName,
                isDebug = Debug.isDebugBuild
            });

            var payloadBytes = System.Text.Encoding.UTF8.GetBytes(payload);

            m_ConnectionManager.NetworkManager.NetworkConfig.ConnectionData = payloadBytes;
        }

        protected string GetPlayerId()
        {
           return Authentication.Authentication.Instance.PlayerId();
        }
    }

    /// <summary>
    /// Simple IP connection setup with UTP
    /// </summary>
    class ConnectionMethodIP : ConnectionMethodBase
    {
        string m_Ipaddress;
        ushort m_Port;

        public ConnectionMethodIP(string ip, ushort port, ConnectionManager connectionManager, string playerName)
            : base(connectionManager, playerName)
        {
            m_Ipaddress = ip;
            m_Port = port;
            m_ConnectionManager = connectionManager;
        }

        public override async Task SetupClientConnectionAsync()
        {
            SetConnectionPayload(GetPlayerId(), m_PlayerName);
            var utp = (UnityTransport)m_ConnectionManager.NetworkManager.NetworkConfig.NetworkTransport;
            utp.SetConnectionData(m_Ipaddress, m_Port);
        }

        public override async Task SetupServerConnectionAsync()
        {
            SetConnectionPayload(GetPlayerId(), m_PlayerName); // Need to set connection payload for host as well, as host is a client too
            var utp = (UnityTransport)m_ConnectionManager.NetworkManager.NetworkConfig.NetworkTransport;
            utp.SetConnectionData(m_Ipaddress, m_Port);
        }
    }

}