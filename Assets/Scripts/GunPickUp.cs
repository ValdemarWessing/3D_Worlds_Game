using UnityEngine;

public class GunPickUp : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (GameState.Instance != null)
        {
            // Set the flag indicating the gun has been picked up
            GameState.Instance.SetFlag("GunPickUp", true);
            Destroy(gameObject);
        }
    }
}
