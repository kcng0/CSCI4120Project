using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : Interactable
{
    public Item Item;
    public Item key;
    public GameObject chestGuard;
    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Item = null;
    }
    
    public override string GetDescription() {
        Item item = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
        string keyName = key.itemName;
        if (item != null && item == key && Item != null && chestGuard == null)
            return "Press [E] to open the box";
        else if (Item == null)
            return "This box is empty!";
        else if (chestGuard != null)
            return "Chest guard isn't defeated!";
        else return "You need " + keyName + " to open this box!";
    }

    public override void Interact() {
        Item item = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
        if (item != null && item == key && Item != null && chestGuard == null)
        {
            Pickup();
        }
    }
}
