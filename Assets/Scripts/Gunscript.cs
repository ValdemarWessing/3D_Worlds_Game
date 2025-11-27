using StarterAssets;
using UnityEngine;

public class Gunscript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource shootSound;
    
    public Material newMaterialRef;
    [SerializeField] GameObject gunModel;

    
    private float nextFire = 0.1f;
    public bool hasGun = false;
    private bool gunsActive = false;
    
    public StarterAssetsInputs inputSource;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

    void Start()
    {
        GameState.Instance.OnFlagChanged += HandleFlagChanged;
    }
    void OnDestroy()
    {
        if (GameState.Instance != null) GameState.Instance.OnFlagChanged -= HandleFlagChanged;
    }
    
    private void HandleFlagChanged(string key, bool value)
    {
        if (key == "GunPickUp" && true)
        {
            hasGun = true;
            Debug.Log("Gun picked up!");
        }
    }
   
    void Update()
    {
        if (hasGun && !gunsActive && inputSource != null && inputSource.back)
        {
            gunModel.SetActive(true);
            gunsActive = true;
            inputSource.shoot = false;
            inputSource.back = false;
        }
        else if (gunsActive && inputSource != null && inputSource.back)
        {
            gunModel.SetActive(false);
            gunsActive = false;
            inputSource.shoot = false;
            inputSource.back = false;
        }
        
        if (inputSource.shoot && Time.time >= nextFire && hasGun && gunsActive)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
            inputSource.shoot = false;
        }
      
    }

    void Shoot()
    {
        muzzleFlash.Play();
        shootSound.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 50f);
            }
            
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f);
          
        }
    }
}
