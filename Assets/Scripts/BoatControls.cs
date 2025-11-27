using System;
using System.Collections;
using StarterAssets;
using UnityEngine;

public class BoatControls : MonoBehaviour, IInteractable
{
    [Header("Boat Movement")]
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    [Header("Boat Motion Effects")]
    public float bobbingHeight = 0.2f;
    public float bobbingSpeed = 2f;

    [Header("Boat State")]
    public bool enterBoat = false;
    
    [SerializeField] GameObject missingKeyText;

    private Rigidbody rb;
    private float startY;
    public GameObject player;
    
    public GameObject ShipCamera;
    public GameObject PlayerCamera;
    
    [SerializeField] GameObject ExitText;
    
    public AudioSource boatSound;
    
    private bool hadConversation = false;
    
    public StarterAssetsInputs inputSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startY = transform.position.y;
        if (GameState.Instance != null) GameState.Instance.OnFlagChanged += HandleFlagChanged;
    }
    
    private void HandleFlagChanged(string key, bool value)
    {
        if (key == "HadConversationWith_Leela" && value == true)
        {
            hadConversation = true;
        }
       
    }
    

    public void Interact()
    {
        // Enter the boat
        if (!enterBoat && hadConversation)
        {
            enterBoat = true;
            boatSound.Play();
            if (player != null)
            {
                player.SetActive(false);
                ShipCamera.SetActive(true);
                PlayerCamera.SetActive(false);
            }
            Debug.Log("Player has entered the boat");
        }
        else if (!hadConversation)
        {
            StartCoroutine(ShowMissingKeyRoutine());
        }
    }
    private IEnumerator ShowMissingKeyRoutine()
    {
        if (missingKeyText == null) yield break;
        missingKeyText.SetActive(true);
        yield return new WaitForSeconds(2f);
        missingKeyText.SetActive(false);
    }


    void FixedUpdate()
    {
        // Only allow control while inside the boat
        if (enterBoat)
        {
            float translation = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;

            Vector3 movement = transform.forward * translation;
            rb.AddForce(movement);

            Vector3 torque = Vector3.up * rotation;
            rb.AddTorque(torque, ForceMode.Force);
        }

        // Bobbing motion
        float newY = startY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        Vector3 pos = transform.position;
        pos.y = newY;
        transform.position = pos;

        // Gentle wobble (roll + pitch)
        float roll = Mathf.Sin(Time.time * 1.5f) * 2f;
        float pitch = Mathf.Sin(Time.time * 1.3f) * 1f;
        Quaternion targetRotation = Quaternion.Euler(pitch, transform.eulerAngles.y, roll);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.5f);
    }

    // --- Trigger logic ---

    private void OnTriggerStay(Collider other)
    {
        
        // Only allow exiting at valid zones while inside boat
        if (enterBoat && other.CompareTag("GroundPosition") && inputSource != null && inputSource.back)
        {
            Debug.Log("Exiting boat at allowed zone");
            // Transform child = other.transform.Find("ExitPointName");
            Transform child = other.transform.GetChild(0); //first child is exit point
            inputSource.back = false;

            if (child != null && player != null)
            {
                Vector3 exitPos = child.position;
                // use exitPos (e.g. place player)
                player.transform.position = exitPos;
                player.SetActive(true);
                enterBoat = false;
                ShipCamera.SetActive(false);
                PlayerCamera.SetActive(true);
                ExitText.SetActive(false);
            }
            else
            {
                // fallback: use collider closest point
                Vector3 exitPos = other.ClosestPoint(transform.position);
                player.transform.position = exitPos;
                player.SetActive(true);
                enterBoat = false;
                ShipCamera.SetActive(false);
                PlayerCamera.SetActive(true);
            }
            boatSound.Stop();
         
        }
        if (enterBoat && other.CompareTag("GroundPosition"))
        {
            ExitText.SetActive(true);
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GroundPosition"))
        {
            ExitText.SetActive(false);
        }
    }
    
}
