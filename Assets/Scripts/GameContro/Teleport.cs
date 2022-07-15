using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform playerTele;
    [SerializeField] GameObject player;
    [SerializeField] Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("AIHide"))
        {
            player.SetActive(false);
            playerTele.position = destination.position;
            player.SetActive(true);
        }
    }
}
