using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBone : Photon.MonoBehaviour
{
    [SerializeField] private Quaternion Pos;

    [SerializeField] private Transform aimBone;

    [SerializeField] private Camera MyCamera;

    void Start()
    {
         MyCamera = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(Pos);

        }
        else
        {
            Pos = (Quaternion)stream.ReceiveNext();
    
        }
    }
    private void LateUpdate()
    {
        if (aimBone != null) 
        {
            if (photonView.isMine)
            {
                Pos = Quaternion.LookRotation(MyCamera.transform.forward, aimBone.up);
                aimBone.rotation = Pos;
            }
            else
            {
                 Pos = Quaternion.LookRotation(MyCamera.transform.forward, aimBone.up);
                aimBone.rotation = Pos;
            }
        }
    }
}
