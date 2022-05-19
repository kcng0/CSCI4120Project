using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public GameObject background;
    public GameObject menu;
    public GameObject main;
    public GameObject control;
    public GameObject quitMessage;
    //public Button controlButton;
    //public Button menuButton;
    //public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            background.SetActive(!background.activeSelf);
            menu.SetActive(!menu.activeSelf);
            //controlButton.gameObject.SetActive(!controlButton.IsActive());
            //menuButton.gameObject.SetActive(!menuButton.IsActive());
            //exitButton.gameObject.SetActive(!exitButton.IsActive());
            if (menu.activeSelf) {
                main.SetActive(true);
                control.SetActive(false);
                quitMessage.SetActive(false);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                main.SetActive(true);
                control.SetActive(false);
                quitMessage.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
