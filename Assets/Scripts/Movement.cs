using UnityEngine;
using UnityEngine.AI;

// Part of the AI package of scripts.
namespace AI
{
	public class Movement : MonoBehaviour
	{

		private Transform _body;
		private Animator _anim;
		private Transform _camera;
		private NavMeshAgent _nav;

		// variables for movement direction
		private string _currentDirection = "Right";
		private float _timerDirection = 0;
		private float _timerTotal = 1;
		
		// variables for movement period
		private float _moveTimer = 0;
		private float _moveTimerTotal = 0;
		
		// player dialogue prefab
		[SerializeField] private GameObject dialogue;

		// Use this for initialization
		private void Start()
		{
			_body = gameObject.GetComponent<Transform>();
			_anim = gameObject.GetComponent<Animator>();
			_camera = Camera.main.transform;
			_nav = gameObject.GetComponent<NavMeshAgent>();
			ResetMoveTimer();

			dialogue.SetActive(false);
		}

		/// <summary>
		/// This function generates new movement allotted time and speed.
		/// </summary>
		private void ResetMoveTimer()
		{
			_moveTimer = 0;
			_moveTimerTotal = Random.Range(2f, 6f);
			_nav.speed = Random.Range(0, 5);
		}

		/// <summary>
		/// This function generates the AI movement.
		/// </summary>
		private void Update()
		{
			// checks if movement period expired
			if (_moveTimer > _moveTimerTotal)
			{
				// generate new movement time and speed
				ResetMoveTimer();
				_nav.isStopped = false;
				
				// generate new navmesh movement range
				_nav.SetDestination(transform.position + new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20)));
			}
			else
			{
				// if movement period has not expired, keep increasing the time
				_moveTimer += Time.deltaTime;
			}

			// player idle state
			if (_nav.velocity.magnitude < 0.1f)
			{
				_anim.SetBool("isWalking", false);
			}
			// player moving state
			else
			{
				_anim.SetBool("isWalking", true);
				_anim.SetFloat("Velocity", Mathf.Clamp(_nav.velocity.magnitude, _nav.speed, _nav.speed));
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

		/// <summary>
		/// This function opens player dialogue animation when players collide.
		/// </summary>
		/// <param name="other"></param>
		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				// activate
				Debug.Log("Collide with player.");
				_nav.isStopped = false;
				_nav.speed = 0;
				dialogue.SetActive(true);
			}
		}

		/// <summary>
		/// This function closes the player dialogue animation when moving away.
		/// </summary>
		/// <param name="other"></param>
		private void OnCollisionExit(Collision other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				// deactivate
				Debug.Log("Leave player.");
				dialogue.SetActive(false);
			}
		}
	}
}