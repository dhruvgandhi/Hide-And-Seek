// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class RunAway : MonoBehaviour
// {
//    private NavMeshAgent _agent;
//    public GameObject player;
//    public float safeDistance = 5f;

//    void Start()
//    {
//        _agent = GetComponent<NavMeshAgent>();
//    }
//    void Update()
//    {
//        float distance = Vector3.Distance(transform.position,player.transform.position);
//        //Run
//        if(distance<safeDistance)
//        {
//            //player to me dist
//            Vector3 dirToPlayer = transform.position - player.transform.position;
//            Vector3 newPos = transform.position + dirToPlayer;
//            _agent.SetDestination(newPos);
//        }
//    }

// }

using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class RunAway : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        public AudioSource walkAudio;
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
            {
                float distance = Vector3.Distance(transform.position,target.transform.position);
                //Run
                if(distance<5)
                {
                    //player to me dist
                    Vector3 dirToPlayer = transform.position - target.transform.position;
                    Vector3 newPos = transform.position + dirToPlayer;
                    agent.SetDestination(newPos);
                }
            }
                //agent.SetDestination(target.position);

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
    }
}

