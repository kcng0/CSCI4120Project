using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStone : MonoBehaviour
{
    public int lifePoint = 30;
    //public int lifeTime = 10;
    private int state;
    public float rotationDamping = 6.0f;
    public Rigidbody prefabFire;
    public float shootForce;
    public Transform shootPosition;
    private Animator thisAnim;
    public float scanRange = 10.0F;
    private bool seePlayer;
    public float viewAngle = 100.0F;
    public LayerMask obstruct;
    private UnityEngine.AI.NavMeshAgent agent; 
    public Transform Waypoint1;
    public Transform Waypoint2;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = Waypoint1.position;
        thisAnim = GetComponent<Animator>();
        StartCoroutine("attackOrMove");       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy;

        enemy = GameObject.FindGameObjectWithTag("Player");

        seePlayerCheck();

        if (state == 0)
        {
            
            if (Vector3.Distance(Waypoint1.position,transform.position) <= 0.5)
            {
                agent.destination = Waypoint2.position;
                state = 2;
            }
              
            else if (Vector3.Distance(Waypoint2.position,transform.position) <= 0.5)
            {
                agent.destination = Waypoint1.position;
                state = 2;
            }  

        }
        
        if (state != 1) 
        {
            if (seePlayer)
            {
                state = 1;
                agent.isStopped = true;
                thisAnim.SetBool("attack", true);
                Debug.Log("player within range");
            }
        }
        else 
        {
            Quaternion rotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            if (!seePlayer)
            {
                state = 0;
                thisAnim.SetBool("attack", false);
                agent.isStopped = false;
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

    IEnumerator attackOrMove()
    { 
        //GameObject enemy;

        while (true) 
        {
            if (state == 2)
            {
                agent.isStopped = true;
                yield return new WaitForSeconds(0.5f);
                state = 0;
                agent.isStopped = false;
            }

            if (!thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Anim_Attack")) 
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                //enemy = GameObject.FindWithTag("Player");
                //transform.LookAt(enemy.transform.position);
                Rigidbody instanceFire = Instantiate(prefabFire, new Vector3(0,1,0) * 0.7f + shootPosition.position + shootPosition.forward * 1.0f,  shootPosition.rotation * Quaternion.Euler (-90f, 0f, 0f));
                instanceFire.GetComponent<Rigidbody>().AddForce(shootPosition.forward * shootForce);
                GetComponent<AudioSource>().Play();
                Destroy(instanceFire.gameObject, 2f);
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
