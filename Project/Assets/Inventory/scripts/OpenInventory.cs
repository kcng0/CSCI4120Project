using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject player;
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
