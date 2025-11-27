using UnityEngine;

public class RotateAroundSelf : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Start is called once before  the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
        
    }
}
