using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class OpenMenuRealtime : MonoBehaviour
{
    public bool menuVisible;
    [SerializeField] private GameObject mainMenuScript;
    void Start()
    {
        mainMenuScript = GameObject.Find("RealTimeMenuManager");
        MenuStatus(false);
    }
    void Update()
    {
        if(GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>() != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(!menuVisible)
                {
                    MenuStatus(true);
                }
                else
                {
                    MenuStatus(false);
                }
            }
        }
    }
    public void MenuStatus(bool value)
    {
        if(value)
        {
            mainMenuScript.GetComponent<MainMenu>().GoToMenu();
            SetCursorLock(false);
            SetSensitivity(0f);
            menuVisible = true;
        }
        else
        {
            mainMenuScript.GetComponent<MainMenu>().HideAll();
            SetCursorLock(true);
            SetSensitivity(2f);
            menuVisible = false;
        }
    }
    public void SetSensitivity(float sensitivity)
    {
        this.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.XSensitivity = sensitivity;
        this.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.YSensitivity = sensitivity;
    }
    public void SetCursorLock(bool value)
    {
        if(GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>() != null)
        {
            this.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.SetCursorLock(value);
        }
    }
    
}
