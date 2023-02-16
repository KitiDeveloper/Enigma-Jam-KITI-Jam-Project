using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerGhostAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 3.0f;

    private bool isHidden = true;
    private float fadeSpeed = 2.0f;

    void Update()
    {
        // If the Stalker Ghost is currently hidden, check if the player is looking at it.
        if (isHidden)
        {
            Vector3 ghostPos = transform.position;
            Vector3 playerPos = player.transform.position;
            Vector3 directionToPlayer = (playerPos - ghostPos).normalized;
            float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

            if (angleToPlayer < 15.0f)
            {
                // Player is looking at the Stalker Ghost, so make it visible and start fading it in.
                isHidden = false;
                StartCoroutine(FadeIn());
            }
        }
        // If the Stalker Ghost is currently visible, check if the player is no longer looking at it.
        else
        {
            Vector3 ghostPos = transform.position;
            Vector3 playerPos = player.transform.position;
            Vector3 directionToPlayer = (playerPos - ghostPos).normalized;
            float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

            if (angleToPlayer > 30.0f)
            {
                // Player is no longer looking at the Stalker Ghost, so start fading it out and then destroy it.
                StartCoroutine(FadeOutAndDestroy());
            }
        }
    }

    IEnumerator FadeIn()
    {
        // Set the alpha of the Stalker Ghost's material to 0 to start.
        Material ghostMaterial = GetComponent<MeshRenderer>().material;
        Color ghostColor = ghostMaterial.color;
        ghostColor.a = 0.0f;
        ghostMaterial.color = ghostColor;

        // Gradually increase the alpha of the Stalker Ghost's material over time.
        while (ghostMaterial.color.a < 1.0f)
        {
            ghostColor.a += Time.deltaTime * fadeSpeed;
            ghostMaterial.color = ghostColor;
            yield return null;
        }
    }

    IEnumerator FadeOutAndDestroy()
    {
        // Gradually decrease the alpha of the Stalker Ghost's material over time.
        Material ghostMaterial = GetComponent<MeshRenderer>().material;
        Color ghostColor = ghostMaterial.color;

        while (ghostMaterial.color.a > 0.0f)
        {
            ghostColor.a -= Time.deltaTime * fadeSpeed;
            ghostMaterial.color = ghostColor;
            yield return null;
        }

        // Destroy the Stalker Ghost game object once it has fully faded out.
        Destroy(gameObject);
    }
}
