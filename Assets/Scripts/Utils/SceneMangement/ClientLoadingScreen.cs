using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace NOBRAIN.KAPUTT.Utils
{
    /// <summary>
    /// This script handles the use of a loading screen with a progress bar and the name of the loaded scene shown. It
    /// must be started and stopped from outside this script. It also allows updating the loading screen when a new
    /// loading operation starts before the loading screen is stopped.
    /// </summary>
    public class ClientLoadingScreen : MonoBehaviour
    {

        [SerializeField]
        CanvasGroup m_CanvasGroup;

        [SerializeField]
        float m_DelayBeforeFadeOut = 0.5f;

        [SerializeField]
        float m_FadeOutDuration = 0.1f;

        [SerializeField]
        Slider m_ProgressBar;

        [SerializeField]
        Text m_SceneName;

        [SerializeField]
        List<Slider> m_OtherPlayersProgressBars;

        [SerializeField]
        List<Text> m_OtherPlayerNamesTexts;

        bool m_LoadingScreenRunning;

        Coroutine m_FadeOutCoroutine;

        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            SetCanvasVisibility(false);
        }

        public void StopLoadingScreen()
        {
            if (m_LoadingScreenRunning)
            {
                if (m_FadeOutCoroutine != null)
                {
                    StopCoroutine(m_FadeOutCoroutine);
                }
                m_FadeOutCoroutine = StartCoroutine(FadeOutCoroutine());
            }
        }

        public void StartLoadingScreen(string sceneName)
        {
            SetCanvasVisibility(true);
            m_LoadingScreenRunning = true;
            UpdateLoadingScreen(sceneName);
        }

        public void UpdateLoadingScreen(string sceneName)
        {
            if (m_LoadingScreenRunning)
            {
                m_SceneName.text = sceneName;
                if (m_FadeOutCoroutine != null)
                {
                    StopCoroutine(m_FadeOutCoroutine);
                }
            }
        }

        void SetCanvasVisibility(bool visible)
        {
            m_CanvasGroup.alpha = visible ? 1 : 0;
            m_CanvasGroup.blocksRaycasts = visible;
        }

        IEnumerator FadeOutCoroutine()
        {
            yield return new WaitForSeconds(m_DelayBeforeFadeOut);
            m_LoadingScreenRunning = false;

            float currentTime = 0;
            while (currentTime < m_FadeOutDuration)
            {
                m_CanvasGroup.alpha = Mathf.Lerp(1, 0, currentTime / m_FadeOutDuration);
                yield return null;
                currentTime += Time.deltaTime;
            }

            SetCanvasVisibility(false);
        }
    }
}