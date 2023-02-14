using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Part of the AI package of scripts.
namespace AI
{
    /// <summary>
    /// This class allows objects to face direction of camera.
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;

        /// <summary>
        /// This function grabs the transform component of the camera.
        /// </summary>
        void Start()
        {
            cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        }

        /// <summary>
        /// This function rotates object to face towards camera.
        /// </summary>
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(-cameraTransform.forward, Vector3.up);
        }
    }
}
