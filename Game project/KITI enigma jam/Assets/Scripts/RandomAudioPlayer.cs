using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private float timeSinceLastPlay = 0f;
    private float playInterval = 15f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timeSinceLastPlay += Time.deltaTime;

        if (timeSinceLastPlay >= playInterval)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
            timeSinceLastPlay = 0f;
        }
    }
}
