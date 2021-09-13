using UnityEngine;
using System.Collections;

public class CreatePlayer : MonoBehaviour 
{
    public Transform respawnPoint;

    void OnJoinedRoom()
    {
        CreatePlayerObject();
    }

    public void CreatePlayerObject()
    {
        PhotonNetwork.Instantiate( "Player", respawnPoint.position, respawnPoint.rotation, 0 );
    }
}
