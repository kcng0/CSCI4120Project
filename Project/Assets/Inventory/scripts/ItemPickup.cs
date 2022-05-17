using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item Item;
    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
    
    public override string GetDescription() {
        return "Press [E] to pick up " + Item.itemName;
    }

    public override void Interact() {
        Pickup();
    }
    // private void OnMouseDown()
    // {
    //     Pickup();
    // }
}
