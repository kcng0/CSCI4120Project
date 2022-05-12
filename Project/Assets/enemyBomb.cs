using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBomb : MonoBehaviour
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

        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("attack") && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.36f  && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
        {
            if (Vector3.Distance(enemy.transform.position,transform.position) <= 2.0 && lifePoint > 0)
            {
                lifePoint = 0;
                Debug.Log("Explosion Hit!");
            }
        }

        seePlayerCheck();

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
                thisAnim.SetBool("move", true);
                //tempPositon = agent.destination;
                //agent.SetDestination(enemy.transform.position);
                //thisAnim.SetBool("move", false);
                //agent.isStopped = true;
            }
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            agent.speed = 2.5f;
            Vector3 dirToEnemy = transform.position - enemy.transform.position;
            Vector3 newPos = transform.position - dirToEnemy;
            agent.SetDestination(newPos);
            if (Vector3.Distance(newPos,transform.position) <= 1.5)
            {
                agent.isStopped = true;
                thisAnim.SetTrigger("attack");
                Destroy(gameObject, 2.0f);
                //Debug.Log(Vector3.Distance(newPos,transform.position));
            }
            else
            {
                agent.isStopped = false;

            }
            if (!seePlayer)
            {
                agent.speed = 1.0f;
                state = 8;
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
                thisAnim.SetBool("move", false);
                //Debug.Log("stop1");
                yield return new WaitForSeconds(2.0f);
                if (state == 2)
                {
                  state = 0;
                  thisAnim.SetBool("move", true);
                  agent.isStopped = false;
                }
            }
            else if (state == 3)
            {
                agent.isStopped = true;
                thisAnim.SetBool("move", false);
                //Debug.Log("stop2");
                yield return new WaitForSeconds(2.0f);
                 if (state == 3)
                {
                  state = 1;
                  thisAnim.SetBool("move", true);
                  agent.isStopped = false;
                }
            }
            else if (state == 8)
            {
                agent.isStopped = true;
                thisAnim.SetBool("move", false);
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
                  thisAnim.SetBool("move", true);
                  agent.isStopped = false;
                }
            }

            else
            {
                //thisAnim.SetBool("move", true);
                //agent.isStopped = false;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

}
