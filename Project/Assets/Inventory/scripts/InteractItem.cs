using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    public Item EquipedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("item") && Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.GetComponent<ItemPickup>().Pickup();
            }
        }
    }
}
