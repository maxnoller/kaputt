
namespace NOBRAIN.KAPUTT.ConnectionManagement
{
    abstract class OnlineState : ConnectionState
    {
        public const string k_DtlsConnType = "dtls";

        public override void OnUserRequestedShutdown()
        {
            // This behaviour will be the same for every online state
            m_ConnectStatusPublisher.Publish(ConnectStatus.GenericDisconnect);
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_Offline);
        }

        public override void OnTransportFailure()
        {
            // This behaviour will be the same for every online state
            m_ConnectionManager.ChangeState(m_ConnectionManager.m_Offline);
        }
    }
}