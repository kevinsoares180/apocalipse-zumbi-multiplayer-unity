using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSideMission : Photon.MonoBehaviour
{
    [SerializeField] private int objectID;
    [SerializeField] private GameObject sideMissions;
    void Start()
    {
        sideMissions = GameObject.FindGameObjectWithTag("SideMissionsManager");
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            
        }
        else
        {

        }
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
             photonView.RPC("completeSideMission", PhotonTargets.MasterClient);
        }
    }
    [PunRPC]
    private void completeSideMission()
    {
        sideMissions.GetComponent<SideMissions>().missionComplete[objectID] = true;
        PhotonNetwork.Destroy(this.gameObject);
    }
}
