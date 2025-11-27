using System;
using UnityEngine;

public class TimeToSleepTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameState.Instance.SetFlag("TimeToSleep", true);
        }
    }
}
