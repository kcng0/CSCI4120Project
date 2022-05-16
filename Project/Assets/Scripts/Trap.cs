using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Animation anim;
    private HealthStatus healthStatus;

    public int damageValue;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        healthStatus = GameObject.Find("PlayerCapsule").GetComponent<HealthStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            healthStatus.TakeDamage(damageValue);
        }
    }
}
