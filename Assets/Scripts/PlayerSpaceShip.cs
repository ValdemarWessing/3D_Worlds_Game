using StarterAssets;
using UnityEngine;

public class PlayerSpaceShip : MonoBehaviour, IInteractable
{
    private bool enterShip = true;
    public GameObject player;
    public GameObject ShipCamera;

    
    Rigidbody spaceShipRb; 
    
    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInput;

    [SerializeField] private float speedMult = 1;
    [SerializeField] private float speedMultAngle = 0.5f;
    [SerializeField] private float speedRollMultAngle = 0.05f;
    [SerializeField] private Light spaceShipLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        spaceShipRb = GetComponent<Rigidbody>();
    }
    
    
    // Update is called once per frame
    public StarterAssetsInputs input;

    void Update()
    {
        Debug.Log($"move:{input.move} look:{input.look} roll:{input.roll}");
    
        verticalMove   = input.move.y;
        horizontalMove = input.move.x;

        mouseInputX = input.look.x;
        mouseInputY = input.look.y;

        rollInput = input.roll;   // You must add this to your input actions
    }
    public void Interact()
    {
        Debug.Log("Interact");
        if (!enterShip)
        {
            enterShip = true;
            if (player != null)
            {
                player.SetActive(false);
                ShipCamera.SetActive(true);
            }
        }
    }
    private void FixedUpdate()
    {
        if (enterShip)
        {
            spaceShipLight.enabled = true;
            spaceShipRb.AddForce(spaceShipRb.transform.TransformDirection(Vector3.forward) * verticalMove * speedMult, ForceMode.VelocityChange);
            spaceShipRb.AddForce(spaceShipRb.transform.TransformDirection(Vector3.right) * horizontalMove * speedMult, ForceMode.VelocityChange);
            spaceShipRb.AddTorque(spaceShipRb.transform.right * speedRollMultAngle * mouseInputY, ForceMode.VelocityChange);
            spaceShipRb.AddTorque(spaceShipRb.transform.up * speedRollMultAngle * mouseInputX, ForceMode.VelocityChange);
            spaceShipRb.AddTorque(spaceShipRb.transform.forward * speedRollMultAngle * rollInput, ForceMode.VelocityChange);   
        }
    }
    
    
    
}
