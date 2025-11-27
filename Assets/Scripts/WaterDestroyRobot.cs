using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterDestroyRobot : MonoBehaviour
{
    [SerializeField] private String StartScene;
    [SerializeField] private GameObject Explosion;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Water destroy");
            Destroy(other.gameObject);
            Explosion.SetActive(true);
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit water");
            SceneManager.LoadScene(StartScene);
        }
    }
  
}
