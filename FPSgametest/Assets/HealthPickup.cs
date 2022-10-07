using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RestoreHealthPickup(other);
            Destroy(gameObject);
        }
    }


    void RestoreHealthPickup(Collider player)
    {
        PlayerHealth healAmount = player.GetComponent<PlayerHealth>();
        healAmount.health = healAmount.health + 100f;
    }
}
