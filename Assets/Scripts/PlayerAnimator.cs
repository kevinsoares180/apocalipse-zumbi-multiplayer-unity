using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : Photon.MonoBehaviour
{
  
   private Rigidbody r;
      //For smoothing the Movement of other Characters
    Vector3 latestPos;
    Quaternion latestRot;
   // Vector3 velocity;
    Vector3 angularVelocity;

    public bool valuesReceived;

    private Vector2 m_Input;

    [SerializeField] private Animator animator;
    void Start()
    {
       // animator = GetComponent<Animator>();
       r = GetComponent<Rigidbody>();
    }
     public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
     {
        if (stream.isWriting)
        {
            stream.SendNext(animator.GetBool("Walk"));

        }
        else
        {
            animator.SetBool("Walk", (bool)stream.ReceiveNext());
            valuesReceived = true;
        }
    }
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (photonView.isMine)
        {
            m_Input = new Vector2(horizontal, vertical);
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }
            if(m_Input.sqrMagnitude != 0)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }

    }


}
     
 