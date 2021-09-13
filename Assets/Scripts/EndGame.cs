using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject canvasEndGame;
    [SerializeField] private GameObject canvasGamePlay;
    [SerializeField] private GameObject canvasRetry;
    [SerializeField] private GameObject sideMissions;

    [SerializeField] private GameObject[] sideMissionUI;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject noScoreUI;

    [SerializeField] private GameObject startCam;
    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private AudioClip endgameMusic;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void ChangeCanvasToEndGame()
    {
        Destroy(canvasRetry.gameObject);
        Destroy(canvasGamePlay.gameObject);
        canvasEndGame.SetActive(true);
    }
    public void SetScoreUI(int score)
    {
        if(score == 0)
        {
            scoreUI.SetActive(false);
            noScoreUI.SetActive(true);
        }
        else
        {
            scoreUI.SetActive(true);
            noScoreUI.SetActive(false);
            scoreUI.GetComponent<Text>().text = "" + score; 
        }
       
    }
    private void CheckMissionCompleted()
    {
        for(int i = 0; i < sideMissions.GetComponent<SideMissions>().missionComplete.Length; i++)
        {
            if(sideMissions.GetComponent<SideMissions>().missionComplete[i])
            {
                sideMissionUI[i].GetComponent<Text>().color = Color.green;
            }
            else
            {
                sideMissionUI[i].GetComponent<Text>().color = Color.red;
            }
        }
    }
    public void endTheGame()
    {
        backgroundMusic.clip = endgameMusic;
        backgroundMusic.Play();
        startCam.GetComponent<AudioListener>().enabled = true;
        UnlockCursor();
        CheckMissionCompleted();
        ChangeCanvasToEndGame();
        PhotonNetwork.Disconnect();
    }
}
