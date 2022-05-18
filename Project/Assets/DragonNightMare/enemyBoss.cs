using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBoss : MonoBehaviour
{
    public int lifePoint = 30;
    //public int lifeTime = 10;
    public int state;
    public float rotationDamping = 6.0f;
    public Rigidbody prefabFire;
    public float shootForce;
    public Transform shootPosition;
    private Animator thisAnim;
    public float scanRange = 10.0F;
    private bool seePlayer;
    private bool inrange;
    public float viewAngle = 100.0F;
    public LayerMask obstruct;
    private UnityEngine.AI.NavMeshAgent agent; 
    private SphereCollider thisColl;
    public Transform Waypoint1;
    public Transform Waypoint3;

    private bool hit3 = false;
    private bool hit4 = false;
    private bool hit5 = false;
    private bool hit6 = false;

    public HealthStatus healthStatus;
    public int damageValue3;
    public int damageValue4;
    public int damageValue5;

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
        inRangeCheck();
        // Debug.Log(state);

        // check death
        if (lifePoint <= 0)
        {
            // play audio and effect may be?
            Destroy(gameObject);
            return;
        }

        if (state == 0)
        {
            state = 2;
            agent.destination = Waypoint1.position;
        }

        if (hit3 == true && thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack") && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.61f  && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.78f)
        {
            if (Vector3.Distance(enemy.transform.position,transform.position) <= 2.5)
            {
                healthStatus.TakeDamage(damageValue3);
                hit3 = false;
            }
            hit3 = false;
        }
        if (hit4 == true && thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack") && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.48f  && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.51f)
        {
            if (Vector3.Distance(enemy.transform.position,transform.position) <= 2.5)
            {
                healthStatus.TakeDamage(damageValue4);
                hit4 = false;
            }
            hit4 = false;
        }
        if (hit5 == true && thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Horn Attack") && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.46f  && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.61f)
        {
            if (Vector3.Distance(enemy.transform.position,transform.position) <= 2.5)
            {
                healthStatus.TakeDamage(damageValue5);
                hit5 = false;
            }
            hit5 = false;
        }
        if(hit6 == true && thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Scream") && thisAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            Rigidbody instanceFire = Instantiate(prefabFire, new Vector3(0,1,0) * 0.8f + shootPosition.position + shootPosition.forward * 1.0f,  shootPosition.rotation * Quaternion.Euler (-90f, 0f, 0f));
            instanceFire.GetComponent<Rigidbody>().AddForce(shootPosition.forward * shootForce);
            GetComponent<AudioSource>().Play();
            Destroy(instanceFire.gameObject, 2f);
            hit6 = false;
        }
        
        if (!seePlayer)
        {
            state = 0;
            thisAnim.SetBool("SeePlayer", false);
            if (Vector3.Distance(Waypoint1.position,transform.position) <= 0.5)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            
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

    private void inRangeCheck()
    {

        GameObject enemy;
        //Vector3 heading;

        enemy = GameObject.FindGameObjectWithTag("Player");
        float dist = Vector3.Distance(enemy.transform.position, transform.position);

        if(dist < 2.5f)
        {
            thisAnim.SetInteger("attack_random",Random.Range(0, 40));
            thisAnim.SetBool("inrange",true);
            agent.isStopped = true;
            inrange = true;
        }
        else 
        {
            thisAnim.SetBool("inrange",false);
            inrange = false;
            agent.isStopped = false;
        }
    
    }

    IEnumerator attackOrMove()
    { 
        //GameObject enemy;

        while (true) 
        {
            if (state == 2)
            {
                yield return new WaitForSeconds(0.5f);
                agent.isStopped = false;
                if (seePlayer)
                {
                    agent.destination = Waypoint3.position;
                    thisAnim.SetBool("SeePlayer", true);
                    if(inrange)
                    {
                        state = 3;
                    }
                }

            }
            else if(state == 3)
            {
                hit3 = true;
                yield return new WaitForSeconds(1.2f);
                int tmp = thisAnim.GetInteger("attack_random");
                if(tmp <= 10)
                {
                    state = 4;
                }
                else if(tmp > 10 && tmp <=30)
                {
                    state = 5;
                }
                else if(tmp > 30)
                {
                    state = 6;
                }
            }
            else if(state == 4)
            {
                hit4 = true;
                yield return new WaitForSeconds(3.4f);
                state = 2;
            }
            else if(state == 5)
            {
                hit5 = true;
                yield return new WaitForSeconds(2.2f);
                state = 2;
            }
            else if(state == 6)
            {
                hit6 = true;
                yield return new WaitForSeconds(2.8f);
                state = 2;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

}
