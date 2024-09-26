using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [Header("Texto")]
    [SerializeField] private TMP_Text pressAnyButton;
    [SerializeField] private TMP_Text titleText;

    [Header("Canvas")]
    [SerializeField] private CanvasGroup playButton;
    [SerializeField] private CanvasGroup settingsButton;
    [SerializeField] private CanvasGroup exitButton;
    [SerializeField] private Image panelAudio;

    [Header("Camaras")]
    [SerializeField] CinemachineVirtualCamera dollyCamera;
    [SerializeField] CinemachineVirtualCamera cameraOptions;
    [SerializeField] private PlayableDirector playableDirector;

    [Header("Timers")]
    [SerializeField] private float timeDolly;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeTime = 0;
    [SerializeField] private float buttonAppearDelay = 0.5f;
    [SerializeField] private float showPanelOptions;

    [Header("Boleanos")]
    private bool isActivePanelOptions;
    private bool isFadingIn = false;
    private bool isInOptionsMenu = false;
    private bool areButtonsShown = false;

    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SfxManager sfxManager;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    // Eventos
    public static event Action OnAnyKeyPress;
    public static event Action OnFadeInComplete;
    public static event Action OnPlayButtonShown;
    public static event Action OnSettingsButtonShown;

    private void OnEnable()
    {
        playableDirector.stopped += OnPlayableDirectorStopped;
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
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
    private void Start()
    {
        audioManager.PlayMenuMusic();
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        panelAudio.gameObject.SetActive(false);
        isActivePanelOptions = false;
        isInOptionsMenu = false;

        pressAnyButton.enabled = true;
        titleText.alpha = 0f;
        dollyCamera.gameObject.SetActive(false);
        cameraOptions.gameObject.SetActive(false);

        playButton.alpha = 0f;
        settingsButton.alpha = 0f;
        exitButton.alpha = 0f;

        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        playableDirector.Stop();
    }
    private void Update()
    {
        if (Input.anyKeyDown && !isInOptionsMenu)
        {
            OnAnyKeyPress?.Invoke();
        }
        if (isFadingIn)
        {
            FadeInTitle();
        }
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); 
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20);
    }
    private void StartDollyTrack()
    {
        pressAnyButton.enabled = false;
        dollyCamera.gameObject.SetActive(true);
        dollyCamera.Priority = 10;
        playableDirector.Play();
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
        if (!areButtonsShown) 
        {
            playButton.gameObject.SetActive(true);
            fadeTime = 0f;
            InvokeRepeating("FadeInPlayButton", 0f, Time.deltaTime);
        }
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
        if (!areButtonsShown) 
        {
            settingsButton.gameObject.SetActive(true);
            fadeTime = 0f;
            InvokeRepeating("FadeInSettingsButton", 0f, Time.deltaTime);
        }
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
        if (!areButtonsShown) 
        {
            exitButton.gameObject.SetActive(true);
            fadeTime = 0f;
            InvokeRepeating("FadeInExitButton", 0f, Time.deltaTime);
        }
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
            areButtonsShown = true;
        }
    }
    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        director.gameObject.SetActive(false);
    }
    public void OnClickPlayButton()
    {
        sfxManager.PlayButtonClickSFX();
        Debug.Log("Profe aun no esta implementado Gameplay");
    }
    public void OnClickOptions()
    {
        sfxManager.PlayButtonClickSFX();
        playableDirector.gameObject.SetActive(false);
        playableDirector.time = 0;
        isInOptionsMenu = true;
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);

        dollyCamera.Priority = 0;
        cameraOptions.gameObject.SetActive(true);
        cameraOptions.Priority = 10;
        dollyCamera.gameObject.SetActive(false);
        Invoke("ShowAudioPanel", showPanelOptions);
    }
    private void ShowAudioPanel()
    {
        isActivePanelOptions = true;
        panelAudio.gameObject.SetActive(true);
    }
    public void CloseOptions()
    {
        sfxManager.PlayButtonClickSFX();
        isInOptionsMenu = false;
        isActivePanelOptions = false;
        panelAudio.gameObject.SetActive(false);

        dollyCamera.Priority = 10;
        cameraOptions.Priority = -10;

        playButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        titleText.gameObject.SetActive(true);

        dollyCamera.gameObject.SetActive(true);
    }
    public void QuitAplication()
    {
        sfxManager.PlayButtonClickSFX();
        Application.Quit();
        Debug.Log("Saliendo del Menu");
    }
}
