using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool open = false;
    public float doorOpenAngle = 0f;
    public float doorCloseAngle = 90f;
    public float smooth = 2f;
    public Item item;
    public Item key;
    // Start is called before the first frame update
    void Start()
    {
        
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
        string keyName = key.itemName;
        if (open == false && item != null && item == key) {
                return "Press E to open the door!";
            } else if (open == true && item != null && item == key) {
                return "Press E to close the door!";
            }
            else return "You need " + keyName + " to open this door!";
        
        }

    public override void Interact() {
        ChangeDoorState();
    }
    // Update is called once per frame
    void Update()
    {
        item = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
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
