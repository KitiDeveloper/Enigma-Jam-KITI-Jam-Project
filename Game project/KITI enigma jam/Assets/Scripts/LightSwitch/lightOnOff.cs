using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightOnOff : MonoBehaviour
{
    public GameObject txtToDisplay;

    private bool playerInZone;

    public GameObject lightOrObject;

    // Start is called before the first frame update
    void Start()
    {
        playerInZone = false;
        txtToDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            lightOrObject.SetActive(!lightOrObject.activeSelf);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().Play("switch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            txtToDisplay.SetActive(true);
            playerInZone = true;
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
