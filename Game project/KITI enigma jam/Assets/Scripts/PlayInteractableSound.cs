using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInteractableSound : Interactable
{
    public AudioSource src;

    public override void OnFocus()
    {
    
    }

    public override void OnInteract()
    {
        src.Play();
    }

    public override void OnLoseFocus()
    {
     
    }
}
