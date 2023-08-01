using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using NOBRAIN.KAPUTT.Utils;
using VContainer;

namespace NOBRAIN.KAPUTT.ConnectionManagement{
public enum ConnectStatus{
    Undefined,
    Success,
    ServerFull,
    LoggedInAgain,
    GenericDisconnect,
    IncompatibleBuildType,
    ServerEndedSession,
    StartServerFailed,
    StartClientFailed,
    StartHostFailed
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
        internal readonly StartingServerState m_StartingServer = new StartingServerState();
        internal readonly ServerState m_Server = new ServerState();
        internal readonly StartingHostState m_StartingHost = new StartingHostState();
        internal readonly HostState m_Host = new HostState();

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            List<ConnectionState> states = new() { m_Offline, m_ClientConnecting, m_ClientConnected, m_StartingServer, m_Server, m_Host, m_StartingHost };
            foreach (var connectionState in states)
            {
                m_Resolver.Inject(connectionState);
            }

            m_CurrentState = m_Offline;

            NetworkManager.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
            NetworkManager.OnServerStarted += OnServerStarted;
            NetworkManager.ConnectionApprovalCallback += ApprovalCheck;
            NetworkManager.OnTransportFailure += OnTransportFailure;
        }

        void OnDestroy()
        {
            NetworkManager.OnClientConnectedCallback -= OnClientConnectedCallback;
            NetworkManager.OnClientDisconnectCallback -= OnClientDisconnectCallback;
            NetworkManager.OnServerStarted -= OnServerStarted;
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

        void OnClientDisconnectCallback(ulong clientId)
        {
            m_CurrentState.OnClientDisconnect(clientId);
        }

        void OnClientConnectedCallback(ulong clientId)
        {
            m_CurrentState.OnClientConnected(clientId);
        }

        void OnServerStarted()
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
            m_CurrentState.StartClient(playerName, ipaddress, port);
        }

        public void StartServer(string ipaddress, int port)
        {
            m_CurrentState.StartServer(ipaddress, port);
        }

        public void StartHost(string ipaddress, int port)
        {
            m_CurrentState.StartHost(ipaddress, port);
        }

        public void RequestShutdown()
        {
            m_CurrentState.OnUserRequestedShutdown();
        }
    }
}
