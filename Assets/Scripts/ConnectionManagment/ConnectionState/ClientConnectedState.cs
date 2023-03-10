using System;
using UnityEngine;
using VContainer;
using Unity.Netcode;

namespace NOBRAIN.KAPUTT.ConnectionManagement
{
    class ClientConnectedState : ConnectionState
    {
        public override void Enter()
        {
            //BEGIN TRACKING THING?
        }

        public override void Exit() { }

        public override void OnClientDisconnect(ulong _)
        {
            var disconnectReason = m_ConnectionManager.NetworkManager.DisconnectReason;
            var connectStatus = JsonUtility.FromJson<ConnectStatus>(disconnectReason);
            m_ConnectStatusPublisher.Publish(connectStatus);
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_Offline);
        }

        public override void OnUserRequestedShutdown()
        {
            m_ConnectStatusPublisher.Publish(ConnectStatus.GenericDisconnect);
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_Offline);
        }
    }
}
