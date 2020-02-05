using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public bool isHisTurn;
        public static Transform whoseTurn;
        public Transform startPoint;
        public AudioSource walkAudio;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
            
                
            if(isHisTurn)
            {
                Debug.Log("Its My Turn");
                whoseTurn = transform;
            }
            else
            {
                //Move Randomly
            }
        }

        private void Update()
        {
            if (target != null)
            {
                if(isHisTurn)
                {
                    Debug.Log("I am chasing you");
                    //Chase all
                    agent.destination = GetClosestEnemy(target).position;
                }
                else
                {
                    //Run away from chaser
                    float distance = Vector3.Distance(transform.position,whoseTurn.transform.position);
                    //Run
                    if(distance<5)
                    {
                        Debug.Log(distance);
                        if(distance<=1)
                        {
                            //Now My Turn to catch
                            Debug.Log("Lets Change turn");
                            whoseTurn.GetComponent<AICharacterControl>().isHisTurn = false;
                            transform.position=startPoint.position;
                            
                            isHisTurn = true;
                            whoseTurn = transform;
                            Debug.Log("whoseTurn Changed");
                        }
                        //player to me dist
                        Vector3 dirToPlayer = transform.position - whoseTurn.transform.position;
                        Vector3 newPos = transform.position + dirToPlayer;
                        agent.SetDestination(newPos);
                    }
                    else
                    {
                        float tempX = UnityEngine.Random.Range(-40,40);
                        float tempZ = UnityEngine.Random.Range(-40,40);
                        if(agent.remainingDistance<=0)
                            agent.SetDestination(new Vector3(tempX,0,tempZ));
                    }
                }
            }
            
           

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
                if(!walkAudio.isPlaying)
                    walkAudio.Play();
            }
            else
            {
                character.Move(Vector3.zero, false, false);
                if(walkAudio.isPlaying)
                    walkAudio.Stop();
            }
                
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        Transform GetClosestEnemy(Transform enemies)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (Transform t in enemies)
            {
                
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist && t != transform)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
    }
}
