using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    private bool hasSpawned = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger area and the script hasn't spawned an object yet
        if (other.CompareTag("Player") && !hasSpawned)
        {
            // Spawn the object at the specified position and rotation
            Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            // Set the hasSpawned flag to true
            hasSpawned = true;

            // Disable the script so it won't spawn another object
            enabled = false;
        }
    }
}
