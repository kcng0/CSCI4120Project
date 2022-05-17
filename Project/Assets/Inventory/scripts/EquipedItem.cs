using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItem : MonoBehaviour
{
    public TMPro.TextMeshProUGUI equipText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Item EquipedItem = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
        if (EquipedItem != null)
        {
            equipText.text = "Equipped: " + EquipedItem.itemName;
        }
        else
        {
            equipText.text = "";
        }
    }
}
