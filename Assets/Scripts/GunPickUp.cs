using UnityEngine;

public class GunPickUp : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (GameState.Instance != null)
        {
            GameState.Instance.SetFlag("GunPickUp", true);
            Destroy(gameObject);
        }
    }
}
