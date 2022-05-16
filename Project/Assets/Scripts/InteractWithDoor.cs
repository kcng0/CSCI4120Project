using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithDoor : MonoBehaviour
{
    public float interactDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Item item = GameObject.Find("PlayerCapsule").GetComponent<InteractItem>().EquipedItem;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Door") && Input.GetKeyDown(KeyCode.E) && item != null && item.itemType == Item.ItemType.key_1)
            {
                hit.collider.GetComponent<Door>().ChangeDoorState();
            }
        }
        
    }
}
