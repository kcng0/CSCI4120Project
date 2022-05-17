using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemContoller : MonoBehaviour
{
    public Item item;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }
    
    public void AddItem(Item newItem)
    {
        item = newItem;
        
    }

    public void UseItem() {
        InteractItem interactItem = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>();
        switch (item.itemType)
        {
            case Item.ItemType.dagger:
                Debug.Log("Use dagger");
                if (interactItem.EquipedItem != item) {
                    interactItem.EquipedItem = item;
                    GameObject.Find("PlayerCapsule").GetComponent<HealthStatus>().attack = 100;
                }
                else
                    interactItem.EquipedItem = null;
                break;
            case Item.ItemType.potion:
                Debug.Log("Use portion");
                HealthStatus healthStatus = GameObject.Find("PlayerCapsule").GetComponent<HealthStatus>();
                healthStatus.currentHealth += 100;
                if (healthStatus.currentHealth > healthStatus.maxHealth)
                {
                    // GetComponent<AudioSource>().Play();
                    healthStatus.currentHealth = healthStatus.maxHealth;
                }
                GameObject.Find("Health bar").GetComponent<HealthBar>().SetHealth(healthStatus.currentHealth);
                RemoveItem();
                break;

            case Item.ItemType.magicBook:
                Debug.Log("Use book");
                if (interactItem.EquipedItem != item)
                    interactItem.EquipedItem = item;
                else
                    interactItem.EquipedItem = null;
                break;

            case Item.ItemType.key_1:
                Debug.Log("Use key 1");
                if (interactItem.EquipedItem != item)
                    interactItem.EquipedItem = item;
                else
                    interactItem.EquipedItem = null;
                break;

            case Item.ItemType.key_2:
                Debug.Log("Use key 2");
                if (interactItem.EquipedItem != item)
                    interactItem.EquipedItem = item;
                else
                    interactItem.EquipedItem = null;
                break;

            case Item.ItemType.key_3:
                Debug.Log("Use key 3");
                if (interactItem.EquipedItem != item)
                    interactItem.EquipedItem = item;
                else
                    interactItem.EquipedItem = null;
                break;

            case Item.ItemType.key_4:
                Debug.Log("Use key 4");
                if (interactItem.EquipedItem != item)
                    interactItem.EquipedItem = item;
                else
                    interactItem.EquipedItem = null;
                break;

            case Item.ItemType.key_5:
                Debug.Log("Use key 5");
                if (interactItem.EquipedItem != item)
                    interactItem.EquipedItem = item;
                else
                    interactItem.EquipedItem = null;
                break;

            case Item.ItemType.key_6:
                Debug.Log("Use key 6");
                if (interactItem.EquipedItem != item)
                    interactItem.EquipedItem = item;
                else
                    interactItem.EquipedItem = null;
                break;
        
            case Item.ItemType.syringe:
                Debug.Log("Use syringe");
                if (interactItem.EquipedItem != item) {
                    interactItem.EquipedItem = item;
                    GameObject.Find("PlayerCapsule").GetComponent<HealthStatus>().attack = 10;
                }
                else
                    interactItem.EquipedItem = null;
                break;

            case Item.ItemType.knife:
                Debug.Log("Use knife");
                if (interactItem.EquipedItem != item) {
                    interactItem.EquipedItem = item;
                    GameObject.Find("PlayerCapsule").GetComponent<HealthStatus>().attack = 50;
                }
                else
                    interactItem.EquipedItem = null;
                break;
        }

        
    }
}
