using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : Interactable
{
    public bool open = false;
    public float doorOpenAngle = 0f;
    public float doorCloseAngle = 90f;
    public float smooth = 2f;
    public bool onTrigger1;
    public bool onTrigger2;
    public bool onTrigger3;
    public bool onTrigger4;
    public bool onTrigger5;
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
        if ( ( (onTrigger1 == true && onTrigger2 == true && onTrigger3 == true && onTrigger4 == true) || onTrigger5 == true) && open == false) {
                return "The door is unlocked! Press [E] to open it!";
            } else if (( (onTrigger1 == true && onTrigger2 == true && onTrigger3 == true && onTrigger4 == true) || onTrigger5 == true) && open == true) {
                return "The door is unlocked! Press [E] to close it!";
            }
            else return "The door is locked! Find some triggers to unlock it!";
        
        }

    public override void Interact() {
        if ( (onTrigger1 == true && onTrigger2 == true && onTrigger3 == true && onTrigger4 == true) || onTrigger5 == true) {
            ChangeDoorState();
        }
    }
    // Update is called once per frame
    void Update()
    {
        onTrigger1 = GameObject.Find("trigger1").GetComponent<Trigger>().steppedOn;
        onTrigger2 = GameObject.Find("trigger2").GetComponent<Trigger>().steppedOn;
        onTrigger3 = GameObject.Find("trigger3").GetComponent<Trigger>().steppedOn;
        onTrigger4 = GameObject.Find("trigger4").GetComponent<Trigger>().steppedOn;
        onTrigger5 = GameObject.Find("trigger5").GetComponent<Trigger>().steppedOn;

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
