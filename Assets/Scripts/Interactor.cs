using StarterAssets;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public GameObject InteractText;
    public Transform interacterSource;
    public float interacterRange;
    
    IInteractable objectInView;
    public StarterAssetsInputs inputSource;
    

    // Update is called once per frame
    void Update()
    {
        bool interactPressed = inputSource.interact;
        inputSource.interact = false;

        // Visualize the raycast in the scene view for debugging
        Debug.DrawRay(interacterSource.position, interacterSource.forward * interacterRange, Color.red);
        
          Ray r = new Ray(interacterSource.position, interacterSource.forward);
          
          // Cast the ray
          if (Physics.Raycast(r, out RaycastHit hitinfo, interacterRange))
          {
              // Check if the hit object has an IInteractable component
              if (hitinfo.collider.TryGetComponent(out IInteractable interactObj))
              {
                  if (objectInView != interactObj)
                  {
                      objectInView = interactObj;
                      InteractText.SetActive(true);
                  }
                  if (interactPressed)
                  {
                      interactObj.Interact();
                      inputSource.interact = false;
                      InteractText.SetActive(false);
                  }

                  return;

              }
          }
          
          clearObjectInView();
          
    }
    // Clear the object in view when nothing is hit
    void clearObjectInView()
    {
        if (objectInView != null)
        {
            objectInView = null;
            InteractText.SetActive(false);
        }
    }
}
