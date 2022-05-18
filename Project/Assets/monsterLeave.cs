using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterLeave : MonoBehaviour
{
    public GameObject monster;
    private bool played = false;
    private bool selfExplode = false;
    private bool isBoss = false;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (monster.tag == "Dragon")
          isBoss = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (monster != null)
        {
            gameObject.transform.position = monster.transform.position;
            if (monster.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                selfExplode = true;
            }

        }
        else
        {
            if (played == false)
            { 
                if (!selfExplode)
                {
                  gameObject.GetComponent<ParticleSystem>().Play();
                  gameObject.GetComponent<AudioSource>().Play();
                }
                played = true;
                Destroy(gameObject, 1.0f);
                if (isBoss)
                {
                    gameManager.GameClear();
                }
            }
        }
        
    }
}
