using UnityEngine;

public class BowlingDetector : MonoBehaviour
{
    public GameObject gameManager;
    private bool _hasFallen = false;
 void Update()
 {
     //check if the object is closer to be orthogonal to the ground
     if (Vector3.Dot(transform.up, Vector3.up) <= 0f && _hasFallen == false)
     {
         Debug.Log("FallenDot");
            _hasFallen = true;
            
         gameManager.GetComponent<GameManager>().fallenBowlingPin();
     }
 }
 
}
