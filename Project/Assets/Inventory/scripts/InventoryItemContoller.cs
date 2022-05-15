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
        print("Hi");
        switch (item.itemType)
        {
            case Item.ItemType.dagger:
                Debug.Log("Use dagger");
                break;
            case Item.ItemType.portion:
                Debug.Log("Use portion");
                break;
        }

        RemoveItem();
    }
}
