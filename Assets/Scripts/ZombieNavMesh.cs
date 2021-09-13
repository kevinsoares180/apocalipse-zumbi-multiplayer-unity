using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieNavMesh : Photon.MonoBehaviour
{
    [Header("NavMesh Settings")]
    public GameObject[] allPlayers;
    public GameObject target;
    NavMeshAgent agent;
    Vector3 destination;

    private GameObject gameManager;

    private Vector3 realPosition;
    private Quaternion realRotation;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        agent.updatePosition = true;
       // photonView.RPC("SetCorrectPos", PhotonTargets.All, transform.position);

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting) 
        {
             stream.SendNext(transform.position);
             stream.SendNext(transform.rotation);
  
         }
         else 
         {
             realPosition = (Vector3)stream.ReceiveNext();
             realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
        }
        if (!PhotonNetwork.isMasterClient)
        {
            agent.enabled = false;
            //this.enabled = false;
        }
        else
        {
            agent.enabled = true;
        }
        SetDestination();
    }
    void SetDestination()
    {
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        target = GetClosestPlayer(allPlayers);
        if (PhotonNetwork.isMasterClient)
        {
            if(target != null)
            {
                destination = target.transform.position;
            }
            else
            {
                destination =  gameManager.GetComponent<GameManager>().respawnPoints[0].transform.position;
            }
            agent.destination = destination;
        }
    }
    GameObject GetClosestPlayer(GameObject[] players)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in players)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
    
}
