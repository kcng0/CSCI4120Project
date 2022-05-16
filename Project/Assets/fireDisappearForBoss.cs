using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireDisappearForBoss : MonoBehaviour
{
    public HealthStatus healthStatus;
    public int damageValue;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnCollisionEnter(Collision other) 
    {

        if(other.gameObject.tag == "Player"  && hit == false)
            {
                healthStatus.TakeDamage(damageValue);
                hit = true;
            }
    }

}
