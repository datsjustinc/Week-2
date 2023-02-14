using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private List<Transform> playersTransform;
    private CinemachineVirtualCamera _camera;
    private int _currentPlayerIndex = 0;

    private void Start()
    {
        _camera = GetComponent <CinemachineVirtualCamera>();
        
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            playersTransform.Add(player.transform);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % playersTransform.Count;
            _camera.Follow = playersTransform[_currentPlayerIndex];
        }
    }
}