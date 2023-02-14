using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

// Part of the AI package of scripts.
namespace AI
{
    /// <summary>
    /// This class controls camera switches from NPC to NPC.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        // variables keep track of NPC's
        [SerializeField] private GameObject[] players;
        [SerializeField] private List<Transform> playersTransform;
        private CinemachineVirtualCamera _camera;
        private int _currentPlayerIndex = 0;

        /// <summary>
        /// this function keeps track of all the NPC's from the start.
        /// </summary>
        private void Start()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();

            // grabs all NPC players
            players = GameObject.FindGameObjectsWithTag("Player");

            // grabs all transforms of NPC players
            foreach (GameObject player in players)
            {
                playersTransform.Add(player.transform);
            }
        }

        /// <summary>
        /// This function creates the camera switch to another NPC from list.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _currentPlayerIndex = (_currentPlayerIndex + 1) % playersTransform.Count;
                _camera.Follow = playersTransform[_currentPlayerIndex];
            }
        }
    }
}