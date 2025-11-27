using System;
using UnityEngine;

public class TentInteract : MonoBehaviour, IInteractable
{
    private bool hasSlept = false;
    [SerializeField] private GameObject tentAnimation;

    private void Update()
    {
        if (hasSlept) return;

    }

    public void Interact()
   {
       tentAnimation.SetActive(true);
       hasSlept = true;
   }
}
