using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReload : MonoBehaviour
{
    public int bullets = 25;
    public int bulletsTotal = 60;
    public bool reload;
    [SerializeField] private AudioClip mp5Reload;
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(bulletsTotal > 0 && !reload && bullets != 25)
            StartCoroutine(Reload());
        }
    }
    public IEnumerator Reload()
    {
        reload = true;
        GetComponent<AudioSource>().PlayOneShot(mp5Reload, 0.7f);
        yield return new WaitForSeconds(0.64f);
        if(bulletsTotal >= 25)
        {
            bulletsTotal += bullets;
            bullets = 25;
            bulletsTotal -= 25;
        }
        else
        {
            if(bulletsTotal > 0)
            {
                bullets = bulletsTotal;
                bulletsTotal = 0;
            }
        }
        reload = false;
    }  
}
