using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light _flickerLight;

    [SerializeField] private float minTime = 0.05f;
    [SerializeField] private float maxTime = 2f;

    private void Start()
    {
        _flickerLight = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    private System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            // Toggle light
            _flickerLight.enabled = !_flickerLight.enabled;

            // Wait random time before toggling again
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
