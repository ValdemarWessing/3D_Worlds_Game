using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _timeToDestroy = 2f; // how long flashlight must hit
    [SerializeField] private ParticleSystem _deathParticles;
    private bool _lightHit = false;
    private bool _particlesSpawned = false; 
    private float _hitTimer = 0f;
    void Update()
    {
        if (_lightHit)
        {
            _hitTimer += Time.deltaTime;
            if (_hitTimer >= _timeToDestroy)
            {
                if (!_particlesSpawned && _deathParticles != null)
                {
                    Instantiate(_deathParticles, transform.position, Quaternion.identity);
                    _particlesSpawned = true;
                    Destroy(gameObject); // destroy enemy
                }
            }
        }
        else
        {
            _hitTimer = 0f;
            _particlesSpawned = false;  // reset if no longer lit

            transform.position = Vector3.MoveTowards(
                transform.position,
                _player.transform.position,
                _speed * Time.deltaTime
            );
        }
    }

    // Call this from flashlight when raycast hits this enemy
    public void SetLightHit(bool isHit)
    {
        _lightHit = isHit;
    }
    
    
}
