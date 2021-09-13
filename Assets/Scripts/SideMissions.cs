using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SideMissions : Photon.MonoBehaviour
{
    [SerializeField] private Transform[] respawnPoints;
    private GameObject map;
    private GameObject medKit;
    private GameObject walkTalkie;

    [SerializeField] private GameObject[] missionUI;

    public bool[] missionComplete; //0 = map | 1 = medkit | 2 = walktalkie |
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            for(int i = 0; i < missionComplete.Length; i++)
            {
                stream.SendNext(missionComplete[i]);
            }
        }
        else
        {
            for(int i = 0; i < missionComplete.Length; i++)
            {
                missionComplete[i] = (bool)stream.ReceiveNext();
            }
        }
    }

    void Start()
    {
        Invoke("RespawnMissionObjects", 5.0f);
    }
    private void RespawnMissionObjects()
    {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.InstantiateSceneObject("Map", respawnPoints[0].position, Quaternion.identity,0, null);
                PhotonNetwork.InstantiateSceneObject("WalkTalkie", respawnPoints[1].position, Quaternion.identity,0, null);
                PhotonNetwork.InstantiateSceneObject("MedKit", respawnPoints[2].position, Quaternion.identity,0, null);
                Debug.Log("Spawn");
            }
    }
    void Update()
    {
        for(int i = 0; i < missionComplete.Length; i++)
        {
            if(missionComplete[i])
            {
                missionUI[i].GetComponent<Text>().color = Color.green;
            }
        }
    }
}
