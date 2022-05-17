using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Interactable
{
public Item Item;
public Item key;
    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Item = null;
    }
    
    public override string GetDescription() {
        Item item = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
        string keyName = key.itemName;
        if (item != null && item == key && Item != null)
            return "Press [E] to power the magic book";
        else if (Item == null)
            return "The fire is gone!";
        else return "Seems like you can use the fire for energy!";
    }

    public override void Interact() {
        Item item = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
        if (item != null && item == key && Item != null)
        {
            Pickup();
        }
    }
}
