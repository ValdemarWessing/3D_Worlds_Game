using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActivator : MonoBehaviour
{
    [SerializeField] private GameObject GunText;
    [SerializeField] private GameObject SleepText;
    void Start()
    {
        if (GameState.Instance != null) GameState.Instance.OnFlagChanged += HandleFlagChanged;
    }

    void OnDestroy()
    {
        if (GameState.Instance != null) GameState.Instance.OnFlagChanged -= HandleFlagChanged;
    }

    private void HandleFlagChanged(string key, bool value)
    {
        if (key == "GunPickUp" && value)
        {
            StartCoroutine(ShowGunText());
        }
        if (key == "TimeToSleep")
        {
            StartCoroutine(TimeToSleep());
        }
    }

    IEnumerator ShowGunText()
    {
        GunText.SetActive(true);
        yield return new WaitForSeconds(3);
        GunText.SetActive(false);
    }
    IEnumerator TimeToSleep()
    {
        SleepText.SetActive(true);
        yield return new WaitForSeconds(3);
        SleepText.SetActive(false);
    }

}
