using StarterAssets;
using UnityEngine;

public class GoodbyeLetter : MonoBehaviour, IInteractable
{
    public StarterAssetsInputs inputSource;
    [SerializeField] private GameObject letterText;
    [SerializeField] private GameObject player;
    
    private bool isShowing = false;

    public void Interact()
    {
        // Show the goodbye letter
        if (GameState.Instance == null) return;

        if (!isShowing)
        {
            letterText.SetActive(true);
            GameState.Instance.SetFlag("GoodbyeLetterRead", true);
            isShowing = true;
            player.SetActive(false);
        }
    }

    private void Update()
    {
        if (isShowing && inputSource != null && inputSource.jump)
        {
            letterText.SetActive(false);
            inputSource.jump = false;
            isShowing = false;
            Destroy(gameObject);
            player.SetActive(true);
        }
    }
}