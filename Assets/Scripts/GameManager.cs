using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image panelAudio;
    private bool isActivePanelOptions;

    public void Start()
    {
        panelAudio.gameObject.SetActive(false);
        isActivePanelOptions = false;
    }
    public void OnClickOptions()
    {
        isActivePanelOptions = true;
        panelAudio.gameObject.SetActive(true);
    }  
    public void CloseOptions()
    {
        isActivePanelOptions = false;
        panelAudio.gameObject.SetActive(false);
    }
    public void QuitAplication()
    {
        Application.Quit();
        Debug.Log("Saliendo del Menu");
    }
}
