using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiLightSwitch : MonoBehaviour
{
    public Light controlledLight;
    public float maxDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {
            // Get the camera and calculate the direction from it
            Camera camera = Camera.main;
            Vector3 direction = camera.transform.forward;

            // Create a raycast from the camera position in the direction the camera is facing
            Ray ray = new Ray(camera.transform.position, direction);

            // Cast the ray and check if it hits the light switch
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance) && hit.collider.gameObject == gameObject)
            {
                // The player is looking at the light switch, check if they press the left mouse button
                if (Input.GetMouseButtonDown(0))
                {
                    // Toggle the light on or off
                    controlledLight.enabled = !controlledLight.enabled;
                }
            }
        }
    }
}
