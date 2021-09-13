using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieStatus : Photon.MonoBehaviour
{
    [Header("Zombie Settings")]
    [SerializeField] private GameObject lifeBarUI;
    [SerializeField] private float life;
    public int damage;
    public float health
    {
        get { return life; }
        set { life = value; }
    }
    private ZombieNavMesh zombieAI;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
        }
    }
    void Start()
    {
        zombieAI = GetComponent<ZombieNavMesh>();
        health = 100;
        damage = 15;
    }
    void LifeBarSystem()
    {
        lifeBarUI.GetComponent<Slider>().value = health;
        for(int i = 0; i < zombieAI.allPlayers.Length; i++)
        {
            if(zombieAI.allPlayers[i].GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().LocalPlayer)
            {
                lifeBarUI.transform.LookAt(zombieAI.allPlayers[i].transform.position);
            }
        }
    }
    void Update()
    {
        LifeBarSystem();
    }
}
