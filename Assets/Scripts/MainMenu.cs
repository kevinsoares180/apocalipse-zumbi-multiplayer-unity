using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Photon.MonoBehaviour
{
    [SerializeField]private GameObject canvasMainMenu;
    [SerializeField]private GameObject canvasOptions;
    [SerializeField]private GameObject canvasLanguage;
    [SerializeField]private GameObject canvasGraphics;
    [SerializeField]private GameObject openMenuRealTime;
    void Start()
    {
       // GoToMenu();
    }
    void Update()
    {
        
    }
    private void GraphicsCanvas(bool variable)
    {
        canvasGraphics.SetActive(variable);
    }
    private void LanguageCanvas(bool variable)
    {
        canvasLanguage.SetActive(variable);
    }
    private void OptionsCanvas(bool variable)
    {
        canvasOptions.SetActive(variable);
    }
    private void MainMenuCanvas(bool variable)
    {
        canvasMainMenu.SetActive(variable);
    }
    public void GoToMenu()
    {
        MainMenuCanvas(true);
        GraphicsCanvas(false);
        LanguageCanvas(false);
        OptionsCanvas(false);
    }
    public void GoToOptions()
    {
        MainMenuCanvas(false);
        GraphicsCanvas(false);
        LanguageCanvas(false);
        OptionsCanvas(true);
    }
    public void GoToLanguage()
    {
        MainMenuCanvas(false);
        GraphicsCanvas(false);
        LanguageCanvas(true);
        OptionsCanvas(false);
    }
    public void GoToGraphics()
    {
        MainMenuCanvas(false);
        GraphicsCanvas(true);
        LanguageCanvas(false);
        OptionsCanvas(false);
    }
    public void HideAll()
    {
        MainMenuCanvas(false);
        GraphicsCanvas(false);
        LanguageCanvas(false);
        OptionsCanvas(false);
    }
    public void BackToMainMenu()
    {
        PhotonNetwork.Disconnect();
      //  PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel("FirstMap");
       //SceneManager.LoadScene("FirstMap");
    }

    public void SetQualityToLow()
    {
        QualitySettings.SetQualityLevel(0, true);
    }
    public void SetQualityToMedium()
    {
        QualitySettings.SetQualityLevel(1, true);
    }
    public void SetQualityToHigh()
    {
        QualitySettings.SetQualityLevel(2, true);
    }
}
