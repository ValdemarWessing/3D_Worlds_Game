
using UnityEngine;

namespace DefaultNamespace
{
    public class OrbitSpeed : MonoBehaviour
    {
        [SerializeField] private GameObject objectToOrbit; 
        [SerializeField] private float orbitSpeed;
        [SerializeField] private float waveAmplitude;
        private float _yValue;
        private void Update()
        {
            transform.RotateAround(objectToOrbit.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);

            
        }
    }
}