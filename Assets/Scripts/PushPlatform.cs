
using UnityEngine;

public class PushPlatform : MonoBehaviour
{
 
    void OnCollisionEnter (Collision collision) {

        if (collision.gameObject.CompareTag("Player") ) {
            
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

    }

}
