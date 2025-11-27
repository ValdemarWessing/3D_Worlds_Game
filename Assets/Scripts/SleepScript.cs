using UnityEngine;

public class SleepScript : MonoBehaviour
{
    [SerializeField] private GameObject SleepAnimation;
    private bool readyToSleep = false;

    void Update()
    {
        if (readyToSleep && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player in range to sleep");
            if (SleepAnimation != null)
            {
                SleepAnimation.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player can enter boat when near it
        if (other.CompareTag("Player"))
        {
            readyToSleep = true;
        }
    }
}
