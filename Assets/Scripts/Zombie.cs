using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Photon.MonoBehaviour
{
    [Header("Zombie Settings")]
    [SerializeField] private GameObject ragDoll;
    public Animator zombieAnimator;

    private bool died;
    public bool isDead
    {
        get { return died; }
        set { died = value; }
    }


    [Header("Skin System")]
    public GameObject[] zombieSkins;
    [SerializeField] private int randomSkin = -1;
    private bool skinLoaded = false;


    [Header("Attack System")]
    [SerializeField] private LayerMask IgnoreLayer;
    private bool attack;

    [Header("NavMesh System")]
    private ZombieNavMesh zombieAI;
    void Start()
    {
        zombieAnimator.GetComponent<Animator>();
        zombieAI = GetComponent<ZombieNavMesh>();
    }
    void Awake()
    {
        photonView.RPC("RandomSkin", PhotonTargets.MasterClient);
    }
     public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(randomSkin);
        }
        else
        {
            randomSkin = (int)stream.ReceiveNext();
        }
    }
    [PunRPC]
    private void TakeHitRPC()
    {
        GetComponent<ZombieStatus>().health -= 30;
    }
    [PunRPC]
    private void RandomSkin()
    {
        randomSkin = Random.Range(0, zombieSkins.Length-1);
       //  photonView.RPC("SetSkinToAll", PhotonTargets.All);
    }
    void AttackPlayer()
    {
        if(zombieAI.target != null)
        {
            float dist = Vector3.Distance(zombieAI.target.transform.position, transform.position);
            if(dist < 3)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position+Vector3.up*1, transform.TransformDirection(Vector3.forward), out hit, 5f, ~IgnoreLayer))
                {
                    if(hit.collider.gameObject.tag == "Player")
                    {
                        HitPlayer(hit.collider.gameObject);
                        attack = true;

                    }
                }
            }
            else
            {
               attack = false;
            }
        }
    }
    private void ChangeAnimation()
    {
        if(attack)
        {
            zombieAnimator.SetBool("Walk", false);
            zombieAnimator.SetBool("Attack", true);
        }
        else
        {
            zombieAnimator.SetBool("Walk", true);
            zombieAnimator.SetBool("Attack", false);
        }
    }
    private void LoadSkin()
    {
        if(!skinLoaded)
        {
            if(randomSkin != -1)
            {
                for(int i = 0; i < zombieSkins.Length; i++)
                {
                    if(zombieSkins[i] != zombieSkins[randomSkin])
                    {
                        zombieSkins[i].SetActive(false);
                    }
                    else
                    {
                        zombieSkins[i].SetActive(true);
                    }
                    if(i == zombieSkins.Length-1)
                    {
                        //skinLoaded = true;
                    }
                }
            }
        }
    }
    void Update()
    {
        
        AttackPlayer(); //Verifica se o player está próximo o suficiente para ataca-lo
        LoadSkin();
        ChangeAnimation();
    }
    void HitPlayer(GameObject player)
    {
        if(!attack)
        {
            StartCoroutine(WaitForAttackAgain());
            if(player.GetComponent<PlayerStats>().health-GetComponent<ZombieStatus>().damage <= 0)
            {
                player.GetComponent<PlayerStats>().Die();
               // player.GetComponent<PlayerStats>().Die();
            }
            else
            {
                player.GetComponent<AudioSource>().PlayOneShot(player.GetComponent<PlayerStats>().painSound, 0.5f);
                player.GetComponent<PlayerStats>().health-= GetComponent<ZombieStatus>().damage;
            }
        }
    }
    IEnumerator WaitForAttackAgain()
    {
        yield return new WaitForSeconds(1.5f);
        attack = false;
    }
    public void Die()
    {
        isDead = true;
        photonView.RPC("DieRPC", PhotonTargets.MasterClient);
    }
    [PunRPC]
    private void DieRPC()
    {
        GameObject rag;
        rag = PhotonNetwork.Instantiate("RagDoll", transform.position, transform.rotation,0);
        rag.GetComponentInChildren<Rigidbody>().AddForce(zombieAI.target.transform.forward * 4000);
        rag.GetComponent<Ragdoll>().ChangeSkin(randomSkin);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
