using System.Collections;
using System.Collections.Generic;
using NOBRAIN.KAPUTT.Infrastructure;
using UnityEngine;
using Unity.Netcode;
using VContainer;

namespace NOBRAIN.KAPUTT.ConnectionManagement{
abstract class ConnectionState
{
        [Inject]
        protected ConnectionManager m_ConnectionManager;

        [Inject]
        protected IPublisher<ConnectStatus> m_ConnectStatusPublisher;

        public abstract void Enter();

        public abstract void Exit();

        public virtual void OnClientConnected(ulong clientId) { }
        public virtual void OnClientDisconnect(ulong clientId) { }

        public virtual void OnServerStarted() { }
        public virtual void OnHostStarted() { }
        public virtual void OnServerStopped() { }

        public virtual void StartClient(string playerName, string ipaddress, int port) { }

        public virtual void StartServer(string ipaddress, int port) { }
        public virtual void StartHost(string ipaddress, int port) { }

        public virtual void OnUserRequestedShutdown() { }

        public virtual void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response) { }

        public virtual void OnTransportFailure() { }
}
}