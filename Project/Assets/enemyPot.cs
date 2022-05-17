using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPot : MonoBehaviour
{
    public int lifePoint = 30;
    public int state = 2;
    public Vector3 tempPositon;
    public float rotationDamping = 6.0f;
    private Animator thisAnim;
    public float scanRange = 10.0F;
    public bool seePlayer;
    public float viewAngle = 120.0F;
    public LayerMask obstruct;
    private UnityEngine.AI.NavMeshAgent agent; 
    public Transform Waypoint1;
    public Transform Waypoint2;
    private bool hit1 = false;
    private bool hit2 = false;
    
    public HealthStatus healthStatus;
    public int damageValue;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = Waypoint2.position;
        tempPositon = Waypoint2.position;
        thisAnim = GetComponent<Animator>();
        StartCoroutine("MoveOrNot");        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy;

        enemy = GameObject.FindGameObjectWithTag("Player");

        seePlayerCheck();

        // check death
        if (lifePoint <= 0)
        {
            // play audio and effect may be?
            Destroy(gameObject);
            state = 8;
            return;
        }

        if (state == 0)
        {
            tempPositon = agent.destination;
            if (Vector3.Distance(Waypoint2.position,transform.position) <= 0.5)
            {
                agent.destination = Waypoint1.position;
                state = 3;
            }
            
        }
        if (state == 1)
        {
            tempPositon = agent.destination;
            if (Vector3.Distance(Waypoint1.position,transform.position) <= 0.5)
            {
                agent.destination = Waypoint2.position;
                state = 2;
            }            
        }

        if (state != 7)
        {
            if (seePlayer)
            {
                state = 7;
            }
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            Vector3 dirToEnemy = transform.position - enemy.transform.position;
            Vector3 newPos = transform.position - dirToEnemy;
            agent.SetDestination(newPos);
            if (Vector3.Distance(newPos,transform.position) <= 2.5f)
            {
                agent.isStopped = true;
                thisAnim.SetBool("attack", true);
                //Debug.Log("Attack");
            }
            else
            {
                thisAnim.SetBool("attack", false);
                hit1 = false;
                agent.isStopped = false;
            }
            if (!seePlayer)
            {
                //agent.speed = 1.0f;
                thisAnim.SetBool("attack", false);
                hit1 = false;
                state = 8;
            }
            if (hit1 && hit2)
            {
                hit1 = false;
                hit2 = false;
            }
        }   

        
    }

    //check see the player or not
    private void seePlayerCheck()
    {
        GameObject enemy;
        Vector3 heading;

        enemy = GameObject.FindGameObjectWithTag("Player");
        heading = enemy.transform.position - transform.position;

        if (heading.sqrMagnitude <= scanRange * scanRange)
        {
            Vector3 directionToEnemy = heading.normalized;
            
            if (Vector3.Angle(transform.forward, directionToEnemy) < viewAngle / 2)
            {
                if (Physics.Raycast(transform.position, directionToEnemy, Vector3.Distance(enemy.transform.position,transform.position), obstruct))
                  seePlayer = false;
                else
                  seePlayer = true;

            }
            else
              seePlayer = false;

        }
        else if (seePlayer)
          seePlayer = false;
    
    }

    IEnumerator MoveOrNot()
    { 
        //GameObject enemy;

        while (true) 
        {
            if (state == 2)
            {
                agent.isStopped = true;
                //Debug.Log("stop1");
                yield return new WaitForSeconds(1.0f);
                if (state == 2)
                {
                  state = 0;
                  agent.isStopped = false;
                }
            }
            else if (state == 3)
            {
                agent.isStopped = true;
                //Debug.Log("stop2");
                yield return new WaitForSeconds(1.0f);
                 if (state == 3)
                {
                  state = 1;
                  agent.isStopped = false;
                }
            }
            else if (state == 8)
            {
                agent.isStopped = true;
                //Debug.Log("stop2");
                yield return new WaitForSeconds(2.0f);
                 if (state == 8)
                {
                  agent.destination = tempPositon;
                  if (Vector3.Distance(tempPositon,Waypoint1.position) <= 0.5)
                  {
                      state = 1;
                  }
                  if (Vector3.Distance(tempPositon,Waypoint2.position) <= 0.5)
                  {
                      state = 0;
                  }
                  agent.isStopped = false;
                }
            }

            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("nobu_attack") && hit1 == false && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.12f && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.88f)
        {
            //Debug.Log("Small Hit!");
            if(collision.gameObject.tag == "Player")
            {
                healthStatus.TakeDamage(damageValue);
                hit1 = true;    
            }
        }
        else if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("nobu_big_attack") && hit2 == false)
        {
            Debug.Log("Big Hit!");
            hit2 = true;         
        }

    }
}
