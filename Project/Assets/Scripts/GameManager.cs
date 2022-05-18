using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public bool isGameActive = true;
    public Button restartButton;
    public float hitRange;
    public GameObject gameOverImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Item EquipedItem = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
        if (EquipedItem == null || ( EquipedItem != null && (EquipedItem.itemType != Item.ItemType.dagger && EquipedItem.itemType != Item.ItemType.syringe && EquipedItem.itemType != Item.ItemType.knife)) )
            {
                GameObject.Find("PlayerCapsule").GetComponent<HealthStatus>().attack = 0;
            }

        if (Input.GetMouseButtonDown(0) && EquipedItem != null && (EquipedItem.itemType == Item.ItemType.dagger || EquipedItem.itemType == Item.ItemType.syringe || EquipedItem.itemType == Item.ItemType.knife))
        {

            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            if (Physics.Raycast(Player.transform.position, Player.transform.forward, out RaycastHit hit, hitRange))
            {
                int attack = GameObject.Find("PlayerCapsule").GetComponent<HealthStatus>().attack;
                GetComponent<AudioSource>().Play();
                switch (hit.collider.tag)
                {
                    case "Dragon":
                        hit.collider.GetComponent<enemyBoss>().lifePoint -= attack;
                        // hit sound
                        break;
                    case "Statue":
                        hit.collider.GetComponent<enemyPot>().lifePoint -= attack;
                        // hit sound
                        break;
                    case "Golem":
                        hit.collider.gameObject.transform.parent.GetComponent<enemyGolem>().lifePoint -= attack;
                        // hit sound
                        break;
                    case "Bomber":
                        hit.collider.gameObject.transform.parent.GetComponent<enemyBomb>().lifePoint -= attack;
                        // hit sound
                        break;
                    case "FireElement":
                        hit.collider.GetComponent<enemyStone>().lifePoint -= attack;
                        //Debug.Log(hit.collider.gameObject.transform.parent);
                        // hit sound
                        break;
                    default:
                        Debug.Log(hit.collider.gameObject.transform.parent);
                        break;
                }
            }
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameOverImage.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    // The restart button is endowed the RestartGame function in the inspector
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
