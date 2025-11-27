using UnityEngine;
using System.Collections;
public class Target : MonoBehaviour
{
    //public Material newMaterialRef;

    
    public float health = 50f;
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            //GetComponent<Renderer>().material = newMaterialRef;
            StartCoroutine(DieAfterDelay());
        }
        IEnumerator DieAfterDelay()
        {
            yield return new WaitForSeconds(0f);
            Die();
        }
        void Die()
        {
            Destroy(gameObject);
        }
    }
  
    
}
