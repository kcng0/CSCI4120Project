<<<<<<< HEAD
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
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Door") && Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.GetComponent<Door>().ChangeDoorState();
            }
        }
        
    }
}
=======
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
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.CompareTag("Door"))
            {
                hit.collider.GetComponent<Door>().ChangeDoorState();
            }
        }
        
    }
}
>>>>>>> ca5d475d52072cd91c3274f4bd0d14cbb3d9f6e9
