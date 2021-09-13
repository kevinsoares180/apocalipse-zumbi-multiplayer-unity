using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Photon.MonoBehaviour
{


    [SerializeField] private Text globalScoreUI;
    [SerializeField] private GameObject zombie;
    public Transform[] respawnPoints;

    [SerializeField] private GameObject[] zombiesAlive;

    private GameObject[] Player;

    public int globalScore;


    void Start()
    {
        
    }
    void Update()
    {
        globalScoreUI.text = ""+ globalScore;
        Player = GameObject.FindGameObjectsWithTag("Player");
        zombiesAlive = GameObject.FindGameObjectsWithTag("Inimigo");

        if (PhotonNetwork.isMasterClient)
        {
            if(zombiesAlive.Length < 10)
            {
               PhotonNetwork.InstantiateSceneObject("Zumbi", respawnPoints[Random.Range(0, respawnPoints.Length)].position, Quaternion.identity,0,null);
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(globalScore);
        }
        else
        {
            globalScore = (int)stream.ReceiveNext();
        }
    }
    public void AddPoints()
    {
         globalScore += 10;

         if(globalScore >= 1000)
         Invoke("EndTheGame", 1f);
    }
    private void EndTheGame()
    {
        GetComponent<EndGame>().endTheGame();
        for(int i = 0; i < Player.Length; i++)
        {
            if(Player[i].GetComponent<PhotonView>().isMine)
            {
                GetComponent<EndGame>().SetScoreUI(Player[i].GetComponent<PlayerStats>().score);
                PhotonNetwork.Destroy(Player[i].gameObject); 
            }
        }
    }
}
