using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text pressAnyButton;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private CanvasGroup playButton;
    [SerializeField] private CanvasGroup settingsButton;
    [SerializeField] private CanvasGroup exitButton;
    [SerializeField] CinemachineVirtualCamera dollyCamera;
    [SerializeField] private float timeDolly;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeTime = 0;
    [SerializeField] private float buttonAppearDelay = 0.5f;
    private bool isFadingIn = false;
    private Vector3 originalScale;

    // Eventos
    public static event Action OnAnyKeyPress;
    public static event Action OnFadeInComplete;
    public static event Action OnPlayButtonShown;
    public static event Action OnSettingsButtonShown;

    private void OnEnable()
    {
        OnAnyKeyPress += StartDollyTrack;
        OnFadeInComplete += ShowPlayButton;
        OnPlayButtonShown += ShowSettingsButton;
        OnSettingsButtonShown += ShowExitButton;
    }
    private void OnDisable()
    {
        OnAnyKeyPress -= StartDollyTrack;
        OnFadeInComplete -= ShowPlayButton;
        OnPlayButtonShown -= ShowSettingsButton;
        OnSettingsButtonShown -= ShowExitButton;
    }
    private void Start()
    {
        pressAnyButton.enabled = true;
        titleText.alpha = 0f;
        dollyCamera.gameObject.SetActive(false);

        playButton.alpha = 0f;
        settingsButton.alpha = 0f;
        exitButton.alpha = 0f;

        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            OnAnyKeyPress?.Invoke();
        }
        if (isFadingIn)
        {
            FadeInTitle();
        }
    }
    private void StartDollyTrack()
    {
        pressAnyButton.enabled = false;
        dollyCamera.gameObject.SetActive(true);
        dollyCamera.Priority = 10;
        Invoke("StartFadeIn", timeDolly);
    }
    private void StartFadeIn()
    {
        isFadingIn = true;
        fadeTime = 0f;
    }
    private void FadeInTitle()
    {
        if (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, fadeTime / fadeDuration);
            Color originalColor = titleText.color;
            titleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
        else
        {
            isFadingIn = false;
            fadeTime = 0;
            OnFadeInComplete?.Invoke();
        }
    }
    private void ShowPlayButton()
    {
        playButton.gameObject.SetActive(true);
        fadeTime = 0f; 
        InvokeRepeating("FadeInPlayButton", 0f, Time.deltaTime);
    }
    private void FadeInPlayButton()
    {
        if (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            playButton.alpha = Mathf.Lerp(0, 1, fadeTime / fadeDuration);
        }
        else
        {
            CancelInvoke("FadeInPlayButton");
            OnPlayButtonShown?.Invoke();
        }
    }
    private void ShowSettingsButton()
    {
        settingsButton.gameObject.SetActive(true);
        fadeTime = 0f;  
        InvokeRepeating("FadeInSettingsButton", 0f, Time.deltaTime);
    }
    private void FadeInSettingsButton()
    {
        if (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            settingsButton.alpha = Mathf.Lerp(0, 1, fadeTime / fadeDuration);
        }
        else
        {
            CancelInvoke("FadeInSettingsButton");
            fadeTime = 0;
            OnSettingsButtonShown?.Invoke();
        }
    }
    private void ShowExitButton()
    {
        exitButton.gameObject.SetActive(true);
        fadeTime = 0f;  
        InvokeRepeating("FadeInExitButton", 0f, Time.deltaTime);
    }
    private void FadeInExitButton()
    {
        if (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            exitButton.alpha = Mathf.Lerp(0, 1, fadeTime / fadeDuration);
        }
        else
        {
            CancelInvoke("FadeInExitButton");
        }
    }
}
