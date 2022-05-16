using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool open = false;
    public float doorOpenAngle = 0f;
    public float doorCloseAngle = 90f;
    public float smooth = 2f;
    private Item item;
    // Start is called before the first frame update
    void Start()
    {
        Item item = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
    }
    public void ChangeDoorState()
    {
        if (!open)
        {
            open = true;
        } else
        {
            open = false;
        }
    }

    public override string GetDescription() {
        
        if (open == false && item == null || item.itemType != Item.ItemType.key_1) {
            return "Key is required to open the door!";
        } else {
            if (open == false && item != null && item.itemType == Item.ItemType.key_1) {
                return "Press E to open the door!";
            } else {
                return "Press E to close the door!";
            }
        }
        }

    public override void Interact() {
        ChangeDoorState();
    }
    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            Quaternion targetrotation1 = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetrotation1, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetrotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetrotation2, smooth * Time.deltaTime);
        }
    }
}
