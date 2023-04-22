using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using NOBRAIN.KAPUTT.Utils;
using Netcode.Transports.Facepunch;
using Steamworks.Data;
using Steamworks;
using VContainer;

namespace NOBRAIN.KAPUTT.ConnectionManagement{
public enum ConnectStatus{
    Undefined,
    Success,
    ServerFull,
    LoggedInAgain,
    GenericDisconnect,
    IncompatibleBuildType,
    HostEndedSession,
    StartHostFailed,
    StartClientFailed
}

public struct ReconnectMessage
{
    public int CurrentAttempt;
    public int MaxAttempt;

    public ReconnectMessage(int currentAttempt, int maxAttempt)
    {
        CurrentAttempt = currentAttempt;
        MaxAttempt = maxAttempt;
    }
}

public struct ConnectionEventMessage : INetworkSerializeByMemcpy{
    public ConnectStatus Status;
    public FixedPlayerName PlayerName;
}

[Serializable]
public class ConnectionPayload{
    public string playerId;
    public SteamId steamId;
    public string playerName;
    public bool isDebug;
}

public class ConnectionManager : MonoBehaviour
    {
        ConnectionState m_CurrentState;

        [Inject]
        NetworkManager m_NetworkManager;
        public NetworkManager NetworkManager => m_NetworkManager;

        [Inject]
        IObjectResolver m_Resolver;


        [SerializeField]
        int m_NbReconnectAttempts = 2;

        public int NbReconnectAttempts => m_NbReconnectAttempts;

        public int MaxConnectedPlayers = 8;

        internal readonly OfflineState m_Offline = new OfflineState();
        internal readonly ClientConnectingState m_ClientConnecting = new ClientConnectingState();
        internal readonly ClientConnectedState m_ClientConnected = new ClientConnectedState();
        internal readonly StartingHostState m_StartingHost = new StartingHostState();
        internal readonly HostingState m_Hosting = new HostingState();

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            List<ConnectionState> states = new() { m_Offline, m_ClientConnecting, m_ClientConnected, m_StartingHost, m_Hosting };
            foreach (var connectionState in states)
            {
                m_Resolver.Inject(connectionState);
            }

            m_CurrentState = m_Offline;

            NetworkManager.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
            NetworkManager.ConnectionApprovalCallback += ApprovalCheck;
            NetworkManager.OnTransportFailure += OnTransportFailure;

            SteamMatchmaking.OnLobbyCreated += OnServerStarted;
            SteamMatchmaking.OnLobbyEntered += OnLobbyEntered;
            SteamMatchmaking.OnLobbyGameCreated += OnLobbyGameCreated;
        }

        void OnDestroy()
        {
            NetworkManager.OnClientConnectedCallback -= OnClientConnectedCallback;
            NetworkManager.OnClientDisconnectCallback -= OnClientDisconnectCallback;
            SteamMatchmaking.OnLobbyCreated -= OnServerStarted;
            NetworkManager.ConnectionApprovalCallback -= ApprovalCheck;
            NetworkManager.OnTransportFailure -= OnTransportFailure;
        }

        internal void ChangeState(ConnectionState nextState)
        {
            Debug.Log($"{name}: Changed connection state from {m_CurrentState.GetType().Name} to {nextState.GetType().Name}.");

            if (m_CurrentState != null)
            {
                m_CurrentState.Exit();
            }
            m_CurrentState = nextState;
            m_CurrentState.Enter();
        }

        void OnLobbyEntered(Lobby _lobby){
            m_CurrentState.StartClient();
        }

        void OnClientDisconnectCallback(ulong clientId)
        {
            m_CurrentState.OnClientDisconnect(clientId);
        }

        private void OnLobbyGameCreated(Lobby _lobby, uint _ip, ushort _port, SteamId _steamId)
        {
            Debug.Log($"Lobby was created");

        }

        void OnClientConnectedCallback(ulong clientId)
        {
            m_CurrentState.OnClientConnected(clientId);
        }

        void OnServerStarted(Result _result, Lobby lobby)
        {
            m_CurrentState.OnServerStarted();
        }

        void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            m_CurrentState.ApprovalCheck(request, response);
        }

        void OnTransportFailure()
        {
            m_CurrentState.OnTransportFailure();
        }


        public void StartClientIp(string playerName, string ipaddress, int port)
        {
            Debug.Log("start client");
        }

        public void StartHostIp(string playerName, string ipaddress, int port)
        {
            m_CurrentState.StartServer(playerName, ipaddress, port);
        }

        public void RequestShutdown()
        {
            m_CurrentState.OnUserRequestedShutdown();
        }
    }
}
