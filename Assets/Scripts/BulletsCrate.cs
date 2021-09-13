using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsCrate : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioClip ammuReload;

    [SerializeField] private GameObject ammuUI;

    private GameObject[] allPlayers;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < allPlayers.Length; i++)
        {
            if(allPlayers[i].GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().LocalPlayer)
            {
                ammuUI.transform.LookAt(allPlayers[i].transform.position);
            }
        }
    }
     private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<GunReload>().bulletsTotal < 60)
            {
                other.gameObject.GetComponent<GunReload>().bulletsTotal = 60;
                other.gameObject.GetComponent<AudioSource>().PlayOneShot(ammuReload, 0.7f);
            }
        }
    }
}
