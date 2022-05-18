using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject control;
    public GameObject player;
    public Button menuButton;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {


            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
            control.SetActive(!control.activeSelf);
            menuButton.gameObject.SetActive(!menuButton.IsActive());
            exitButton.gameObject.SetActive(!exitButton.IsActive());
            if (inventoryMenu.activeSelf) {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
