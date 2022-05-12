using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open = false;
    public float doorOpenAngle = 0f;
    public float doorCloseAngle = 90f;
    public float smooth = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ChangeDoorState()
    {
        if (!open)
        {
            open = true;
        }
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
