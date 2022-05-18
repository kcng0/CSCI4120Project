using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool steppedOn = false;
    private int collideNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        collideNum += 1;
        if (collideNum > 0)
            steppedOn = true;

    }

    private void OnTriggerExit(Collider other)
    {
        collideNum--;
        if (collideNum == 0)
            steppedOn = false;
        
    }
}
