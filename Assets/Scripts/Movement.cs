using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _nav;

        private void Start()
        {
            _nav = gameObject.GetComponent<NavMeshAgent>();
            _nav.speed = 3;
        }

        private void Update()
        {
            _nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
        }
    }
  
 }
