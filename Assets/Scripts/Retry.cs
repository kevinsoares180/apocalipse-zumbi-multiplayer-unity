using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    [SerializeField] private GameObject createPlayerScript;

    public GameObject canvasGamePlay;
    public GameObject canvasRetry;
    [SerializeField] private GameObject lastScoreUI;
    [SerializeField] private GameObject bestScoreUI;

    void Start()
    {
       
    }
    void Update()
    {
        
    }
    public void SetScoreUI(int score, int bestScore)
    {
        lastScoreUI.GetComponent<Text>().text = "" + score; 
        bestScoreUI.GetComponent<Text>().text = "" + bestScore;
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ChangeCanvasToRetry()
    {
        canvasGamePlay.SetActive(false);
        canvasRetry.SetActive(true);
    }
    public void ChangeCanvasToGamePlay()
    {
        canvasGamePlay.SetActive(true);
        canvasRetry.SetActive(false);
    }
    private void DisableDieCamComponents()
    {
        GetComponent<Camera>().enabled = false;
        GetComponent<AudioListener>().enabled = false;
    }
    public void EnableDieCamComponents()
    {
        GetComponent<Camera>().enabled = true;
        GetComponent<AudioListener>().enabled = true;
    }
    public void RetryPlayer()
    {
        createPlayerScript.GetComponent<CreatePlayer>().CreatePlayerObject();
        DisableDieCamComponents();
        ChangeCanvasToGamePlay();
    }
}
