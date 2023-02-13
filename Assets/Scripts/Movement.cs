using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
	public class Movement : MonoBehaviour
	{

		private Transform _body;
		private Animator _anim;
		private Transform _camera;
		private NavMeshAgent _nav;

		private string _currentDirection = "Right";
		private float _timerDirection = 0;
		private float _timerTotal = 1;


		private float _moveTimer = 0;
		private float _moveTimerTotal = 0;

		// Use this for initialization
		void Start()
		{
			_anim = GetComponent<Animator>();
			_camera = Camera.main.transform;
			_nav = GetComponent<NavMeshAgent>();
			ResetMoveTimer();
		}

		void ResetMoveTimer()
		{
			_moveTimer = 0;
			_moveTimerTotal = Random.Range(1.1f, 3.3f);
		}

		// Update is called once per frame
		void Update()
		{
			
			if (_moveTimer > _moveTimerTotal)
			{
				ResetMoveTimer();
				_nav.isStopped = false;
				
				_nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
				_nav.speed = 3;
			}
			else
			{
				_moveTimer += Time.deltaTime;
			}

			if (_nav.velocity.magnitude < 0.1f)
			{
				_anim.SetBool("isWalking", false);
			}
			else
			{
				_anim.SetBool("isWalking", true);
				_anim.SetFloat("Velocity", _nav.velocity.magnitude);
			}

			//Direction Change Detection
			if (_timerDirection > _timerTotal)
			{
				_timerDirection = 0;
				_timerTotal = Random.Range(0.2f, 1.23f);
				float currentRelativeDirection = Vector3.Dot(_nav.velocity, _camera.right);
				if (currentRelativeDirection > 0 && _currentDirection == "Right")
				{
					_currentDirection = "Left";
					var localS = _body.localScale;
					localS.x *= -1;
					_body.localScale = localS;
				}
				else if (currentRelativeDirection < 0 && _currentDirection == "Left")
				{
					_currentDirection = "Right";
					var localS = _body.localScale;
					localS.x *= -1;
					_body.localScale = localS;
				}
			}
			else
			{
				_timerDirection += Time.deltaTime;
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.CompareTag("Booth_Food"))
			{
			}

		}
	}
}