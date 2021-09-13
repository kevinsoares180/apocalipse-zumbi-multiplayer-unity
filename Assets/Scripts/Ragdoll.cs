using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : Photon.MonoBehaviour
{
    public int RagDollZombieSkin;
    public GameObject[] RagDollSkins;
    void Start()
    {
        
        StartCoroutine(DestroyRagDoll());
    }
    void Update()
    {
        RagDollSkins[RagDollZombieSkin].SetActive(true);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(RagDollZombieSkin);
        }
        else
        {
            RagDollZombieSkin = (int)stream.ReceiveNext();
        }
    }
    public void ChangeSkin(int SkinID)
    {
        photonView.RPC("ChangeSkinRPC", PhotonTargets.All, SkinID);
    }
    [PunRPC]
    private void ChangeSkinRPC(int SkinID)
    {
        RagDollZombieSkin = SkinID;
    }
    IEnumerator DestroyRagDoll()
    {
        yield return new WaitForSeconds(3f);
        photonView.RPC("DestroyRPC", PhotonTargets.MasterClient);
    }
    [PunRPC]
    private void DestroyRPC()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
}
