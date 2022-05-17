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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Item EquipedItem = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
        if (Input.GetMouseButtonDown(0) && EquipedItem != null && EquipedItem.itemType == Item.ItemType.dagger)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            if (Physics.Raycast(Player.transform.position, Player.transform.forward, out RaycastHit hit, hitRange))
            {

                GetComponent<AudioSource>().Play();
                switch (hit.collider.tag)
                {
                    case "Dragon":
                        hit.collider.GetComponent<enemyBoss>().lifePoint -= 10;
                        // hit sound
                        break;
                    case "Statue":
                        hit.collider.GetComponent<enemyPot>().lifePoint -= 10;
                        // hit sound
                        break;
                    case "Golem":
                        hit.collider.gameObject.transform.parent.GetComponent<enemyGolem>().lifePoint -= 10;
                        // hit sound
                        break;
                    case "Bomber":
                        hit.collider.gameObject.transform.parent.GetComponent<enemyBomb>().lifePoint -= 10;
                        // hit sound
                        break;
                    case "FireElement":
                        hit.collider.GetComponent<enemyStone>().lifePoint -= 10;
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
    }

    // The restart button is endowed the RestartGame function in the inspector
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
