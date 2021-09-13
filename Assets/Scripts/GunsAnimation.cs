using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsAnimation : MonoBehaviour
{
    private PlayerGuns playerGuns;
    [SerializeField] private Animator myAnimator;
    void Start()
    {
        playerGuns = GetComponent<PlayerGuns>();
    }

    void Update()
    {
        if(playerGuns.shot && !GetComponent<GunReload>().reload)
        {
            myAnimator.SetBool("Idle", false);
            myAnimator.SetBool("Shot", true);
            myAnimator.SetBool("Reload", false);
        }
        else
        {
            myAnimator.SetBool("Idle", true);
            myAnimator.SetBool("Shot", false);
            myAnimator.SetBool("Reload", false);
        }
        if(playerGuns.GetComponent<GunReload>().reload)
        {
            myAnimator.SetBool("Idle", false);
            myAnimator.SetBool("Shot", false);
            myAnimator.SetBool("Reload", true);
        }
    }
}
