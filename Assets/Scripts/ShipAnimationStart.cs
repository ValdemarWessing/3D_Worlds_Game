using System;
using UnityEngine;

public class ShipAnimation : MonoBehaviour, IInteractable
{
    private bool enterShip = false;
    [SerializeField] private GameObject shipAnimation;

    private void Update()
    {
        if (enterShip) return;

    }

    public void Interact()
    {
        shipAnimation.SetActive(true);
        enterShip = true;
    }
}