using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Photon.MonoBehaviour
{

    [Header("Guns Objects")]
    public GameObject gunMP5;

    public GameObject gunMP5OnlineView;

    
    public enum equipedItem : byte
    {
        None, MP5
    }
    public equipedItem equiped;

    void Start()
    {
        SetEquipedItem(Inventory.equipedItem.MP5);
    }
    void Update()
    {
        
    }
    public void SetEquipedItem(equipedItem equipItem)
    {
        equiped = equipItem;
        //verificar a arma equipada atualmente para ativa-lá e desativar as outras caso tenha
        if(equiped == Inventory.equipedItem.MP5)
        {
            if (photonView.isMine)
            {
                gunMP5.gameObject.SetActive(true);
                gunMP5OnlineView.gameObject.SetActive(false);
            }
            else
            {
                gunMP5.gameObject.SetActive(false);
                gunMP5OnlineView.gameObject.SetActive(true);
            }
        }
        else
        {
            gunMP5.gameObject.SetActive(false);
            gunMP5OnlineView.gameObject.SetActive(false);
        }
    }
}
