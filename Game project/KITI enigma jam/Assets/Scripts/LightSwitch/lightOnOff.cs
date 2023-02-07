using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightOnOff : MonoBehaviour
{
    public GameObject txtToDisplay;

    public GameObject player;

    private bool playerInZone;

    public GameObject lightOrObject;

    public bool lightIsON;

    // Start is called before the first frame update
    void Start()
    {
        playerInZone = false;
        txtToDisplay.SetActive(false);
        Debug.Log("Your text");
    }

    // Update is called once per frame
    void Update()
    {
        Console.WriteLine("Script is updating");
        if (playerInZone && Input.GetKeyDown(KeyCode.F) && lightIsON == false)
        {
            lightOrObject.SetActive(!lightOrObject.activeSelf);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().Play("LightSwitchAnimation01");
        }
        if (playerInZone && Input.GetKeyDown(KeyCode.F) && lightIsON == true)
        {
            lightOrObject.SetActive(!lightOrObject.activeSelf);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().Play("LightSwitchAnimation02");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            txtToDisplay.SetActive(true);
            playerInZone = true;
            Console.WriteLine("player is inside colloder");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            playerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }
}
