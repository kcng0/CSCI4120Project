using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public GameObject platform_1;
    public GameObject platform_2;
    public GameObject platform_3;
    public GameObject platform_4;
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
        if (other.CompareTag("Player")) {
            platform_1.SetActive(true);
            platform_2.SetActive(true);
            platform_3.SetActive(true);
            platform_4.SetActive(true);
        }

    }
}
