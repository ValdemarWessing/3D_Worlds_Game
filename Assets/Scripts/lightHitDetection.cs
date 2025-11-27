using UnityEngine;

public class lightHitDetection : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _range = 20f;
    [SerializeField] private Light _flashlight; 
    private Color _originalColor;

    void Start()
    {
        if (_flashlight != null)
            _originalColor = _flashlight.color;
    }
    void Update()
    {
        foreach (EnemyMovement enemy in FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None))
        {
            enemy.SetLightHit(false);
        }
        
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _range))
        {
            EnemyMovement enemy = hit.collider.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.SetLightHit(true);

                if (_flashlight != null)
                    _flashlight.color = Color.red;  // 🔴 change color while hitting enemy
            }
            else
            {
                if (_flashlight != null)
                    _flashlight.color = _originalColor; // reset if not enemy
            }
        }
        else
        {
            if (_flashlight != null)
                _flashlight.color = _originalColor; // reset if hitting nothing
        }
    }
}