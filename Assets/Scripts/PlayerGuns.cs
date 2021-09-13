using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGuns : Photon.MonoBehaviour
{ 

    [SerializeField] private GameObject gameManager;

    [Header("Shot System")]
    public bool shot;
    [SerializeField] private AudioSource myAudio;
    [SerializeField] private AudioSource myOnlineAudio;
    [SerializeField] private GameObject gunFire;
    private Camera myCamera;

    [Header("Guns Info")]
    //MP5
    [SerializeField] private AudioClip mp5Shot;
    [SerializeField] private float mp5FireRate = 0.1f;

    [Header("Inventory System")]
    private Inventory Inventory;
    
    [Header("Reload System")]
    private GunReload reloadSystem;

    //private GameObject lastZombieHit;
    void Start()
    {
        reloadSystem = GetComponent<GunReload>();
        Inventory = GetComponent<Inventory>();
        myAudio = GetComponent<AudioSource>();
        myCamera = gameObject.GetComponentInChildren<Camera>();
    }
    void Update()
    {
    }
    void FixedUpdate()
    {        
        if (photonView.isMine)
        {
            if(Input.GetButton("Fire1"))
            {
                if(!GetComponent<OpenMenuRealtime>().menuVisible)
                Shot();
            }
        }
    }
    void Shot()
    {
        //mp5
        if(Inventory.equiped == Inventory.equipedItem.MP5)
        {
            if(!shot && !reloadSystem.reload)
            {
                if(reloadSystem.bullets > 0)
                {
                    ShotSettings();
                    RaycastHit hit;
                    if (Physics.Raycast(myCamera.transform.position, myCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                    {
                        if(hit.collider.gameObject.tag == "Inimigo")
                        {
                            HitZombie(hit.collider.gameObject);
                        }
                    }
                }
                else
                {
                    if(reloadSystem.bulletsTotal > 0 && !reloadSystem.reload)
                    StartCoroutine(reloadSystem.Reload());
                }
            }
        }

    }
    private void ShotSettings()
    {
        photonView.RPC("ShotRPC", PhotonTargets.Others);
        reloadSystem.bullets--;
        myAudio.PlayOneShot(mp5Shot, 1f);
        gunFire.SetActive(true);
        gunFire.transform.eulerAngles = new Vector3(gunFire.transform.eulerAngles.x, gunFire.transform.eulerAngles.y, Random.Range(0, 180));
        StartCoroutine(ShotAgain());
    }
    [PunRPC]
    private void ShotRPC()
    {
        myOnlineAudio.PlayOneShot(mp5Shot, 1f);
    }
    void HitZombie(GameObject zombie)
    {
        //verificar se neste tiro o zombie irá morrer para executar a pontuação e a morte do zumbi
        if(zombie.GetComponent<ZombieStatus>().health-30f <= 0f && !zombie.GetComponent<Zombie>().isDead)
        {
            photonView.RPC("AddGlobalScore", PhotonTargets.All);
            GetComponent<PlayerStats>().score += 10;
            zombie.GetComponent<Zombie>().Die();
        }
        else
        {

            zombie.GetComponent<Zombie>().photonView.RPC("TakeHitRPC", PhotonTargets.All);
        }
    }
    [PunRPC]
    private void AddGlobalScore()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.GetComponent<GameManager>().AddPoints();
    }
    IEnumerator ShotAgain()
    {
        shot = true;
        yield return new WaitForSeconds(0.1f);
        gunFire.SetActive(false);
        yield return new WaitForSeconds(mp5FireRate-0.1f);
        shot = false;
    }
}