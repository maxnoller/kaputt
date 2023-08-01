using System;
using System.Collections;
using NOBRAIN.KAPUTT.ApplicationLifecycle.Messages;
using NOBRAIN.KAPUTT.ConnectionManagement;
using NOBRAIN.KAPUTT.Infrastructure;
using NOBRAIN.KAPUTT.Utils;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace NOBRAIN.KAPUTT.ApplicationLifecycle
{

    /// <summary>
    /// An entry point to the application, where we bind all the common dependencies to the root DI scope.
    /// </summary>
    public class ApplicationController : LifetimeScope
    {
        [SerializeField] ConnectionManager m_ConnectionManager;
        [SerializeField] NetworkManager m_NetworkManager;

        IDisposable m_Subscriptions;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterComponent(m_ConnectionManager);
            builder.RegisterComponent(m_NetworkManager);

            //the following singletons represent the local representations of the lobby that we're in and the user that we are
            //they can persist longer than the lifetime of the UI in MainMenu where we set up the lobby that we create or join
            //builder.Register<ProfileManager>(Lifetime.Singleton);

            //these message channels are essential and persist for the lifetime of the lobby and relay services
            // Registering as instance to prevent code stripping on iOS
            builder.RegisterInstance(new MessageChannel<QuitApplicationMessage>()).AsImplementedInterfaces();
            builder.RegisterInstance(new MessageChannel<ConnectStatus>()).AsImplementedInterfaces();

            //these message channels are essential and persist for the lifetime of the lobby and relay services
            //they are networked so that the clients can subscribe to those messages that are published by the server
            builder.RegisterComponent(new NetworkedMessageChannel<ConnectionEventMessage>()).AsImplementedInterfaces();

            //this message channel is essential and persists for the lifetime of the lobby and relay services
            builder.RegisterInstance(new MessageChannel<ReconnectMessage>()).AsImplementedInterfaces();

            //buffered message channels hold the latest received message in buffer and pass to any new subscribers
            //builder.RegisterInstance(new BufferedMessageChannel<LobbyListFetchedMessage>()).AsImplementedInterfaces();

            //all the lobby service stuff, bound here so that it persists through scene loads
            //builder.Register<AuthenticationServiceFacade>(Lifetime.Singleton); //a manager entity that allows us to do anonymous authentication with unity services

            //LobbyServiceFacade is registered as entrypoint because it wants a callback after container is built to do it's initialization
            //builder.RegisterEntryPoint<LobbyServiceFacade>(Lifetime.Singleton).AsSelf();
        }

        private void Start()
        {
            var quitApplicationSub = Container.Resolve<ISubscriber<QuitApplicationMessage>>();

            var subHandles = new DisposableGroup();
            subHandles.Add(quitApplicationSub.Subscribe(QuitGame));
            m_Subscriptions = subHandles;

            Application.wantsToQuit += OnWantToQuit;
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 120;

            SceneManager.LoadScene("MainMenu");
        }

        protected override void OnDestroy()
        {
            m_Subscriptions?.Dispose();
            base.OnDestroy();
        }

        /// <summary>
        ///     In builds, if we are in a lobby and try to send a Leave request on application quit, it won't go through if we're quitting on the same frame.
        ///     So, we need to delay just briefly to let the request happen (though we don't need to wait for the result).
        /// </summary>
        private IEnumerator LeaveBeforeQuit()
        {
            // We want to quit anyways, so if anything happens while trying to leave the Lobby, log the exception then carry on
            yield return null;
            Application.Quit();
        }

        private bool OnWantToQuit()
        {
            var canQuit = true;
            if (!canQuit)
            {
                StartCoroutine(LeaveBeforeQuit());
            }
            return canQuit;
        }

        private void QuitGame(QuitApplicationMessage msg)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
