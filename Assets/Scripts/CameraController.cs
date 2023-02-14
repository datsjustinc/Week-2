using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private List<Transform> playersTransform;
    private Transform _currentPlayer;
    private int _currentPlayerIndex = 0;

    private void Start()
    {
        _currentPlayer = null;
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
            _currentPlayer = playersTransform[_currentPlayerIndex];
            _currentPlayer.position = playersTransform[_currentPlayerIndex].position;
        }
        
        transform.position = _currentPlayer.position + Vector3.back * 3 + Vector3.up * 7;
        transform.LookAt(_currentPlayer);
    }
}