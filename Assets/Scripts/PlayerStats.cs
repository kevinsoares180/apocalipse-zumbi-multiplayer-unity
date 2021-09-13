using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Photon.MonoBehaviour
{
    //[SerializeField] private Text lifeUI;
    
    [Header("Player Status")]

    [SerializeField] private float life;

    public float health
    {
        get { return life; }
        set { life = value; }
    }
    public int score;
    private GunReload reloadSystem;
    public GameObject DieCam;

    public AudioClip painSound;
    [Header("Canvas UI")]

    [SerializeField] private GameObject lifeBarUI;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject pingUI;
    [SerializeField] private GameObject fpsUI;
    
    [SerializeField] private GameObject bulletsUI;


    [Header("Get FPS/Ping")]
    
    private float ping;

    private float frameCount = 0f;
    private float dt = 0.0f;
    private float fps = 0.0f;
    private float updateRate = 4.0f;  // 4 updates per sec.
    public void Die()
    {
        if (photonView.isMine)
        {
            DieCam = GameObject.FindGameObjectWithTag("DieCam");
            DieCam.GetComponent<Retry>().EnableDieCamComponents();
            DieCam.GetComponent<Retry>().ChangeCanvasToRetry();
            DieCam.GetComponent<Retry>().SetScoreUI(score, PlayerPrefs.GetInt("Score"));
            DieCam.GetComponent<Retry>().UnlockCursor();
            
            
            PhotonNetwork.Destroy(this.gameObject); 
        }
       // photonView.RPC("DieRPC", PhotonTargets.MasterClient);
    }
    void Start()
    {
        DieCam = GameObject.FindGameObjectWithTag("DieCam");
        DieCam.GetComponent<Retry>().ChangeCanvasToGamePlay();
        reloadSystem = GetComponent<GunReload>();
        FindCanvasStatus();
        SetCanvasText();
        health = 100f;
    }
    private void FindCanvasStatus()
    {
        lifeBarUI = GameObject.Find("CanvasGamePlay/LifeBar");
        scoreUI = GameObject.Find("CanvasGamePlay/ScorePoints");
        pingUI = GameObject.Find("CanvasGamePlay/Ping");
        fpsUI = GameObject.Find("CanvasGamePlay/FPS");
        DieCam = GameObject.FindGameObjectWithTag("DieCam");
        bulletsUI = GameObject.Find("CanvasGamePlay/Bullets");
    }
    private void BulletsUIUpdate()
    {
        if(reloadSystem.bullets > 9)
        {
            if(reloadSystem.bulletsTotal > 9)
            {
                 bulletsUI.GetComponent<Text>().text = "" + reloadSystem.bullets + "/" + reloadSystem.bulletsTotal;
            }
            else
            {
                 bulletsUI.GetComponent<Text>().text = "" + reloadSystem.bullets + "/0" + reloadSystem.bulletsTotal;
            }
        }
        else
        {
            if(reloadSystem.bulletsTotal > 9)
            {
                 bulletsUI.GetComponent<Text>().text = "0" + reloadSystem.bullets + "/" + reloadSystem.bulletsTotal;
            }
            else
            {
                 bulletsUI.GetComponent<Text>().text = "0" + reloadSystem.bullets + "/0" + reloadSystem.bulletsTotal;
            }
        }
    }
    void Update()
    {
        SetCanvasText();
        GetFPS();
        BulletsUIUpdate();
        SaveScore();
    }
    private void SetCanvasText()
    {
        ping = PhotonNetwork.GetPing();
        pingUI.GetComponent<Text>().text = "PING: "+ ping;
        fpsUI.GetComponent<Text>().text = "FPS: "+ (int)fps;
        scoreUI.GetComponent<Text>().text = ""+ score;
        lifeBarUI.GetComponent<Slider>().value = health;
    }
    private void GetFPS()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0/updateRate)
        {
            fps = frameCount / dt ;
            frameCount = 0f;
            dt -= 1.0f/updateRate;
        }
    }
    private void SaveScore()
    {
       if(PlayerPrefs.HasKey("Score"))
       {
           if(score > PlayerPrefs.GetInt("Score"))
           {
                PlayerPrefs.SetInt("Score", score);
           }
       }
       else
       {
           PlayerPrefs.SetInt("Score", score);
       }
    }
}
