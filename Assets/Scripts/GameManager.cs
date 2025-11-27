using UnityEngine;

public class GameManager : MonoBehaviour
{
   int fallenPins = 0;
   [SerializeField] GameObject FinishText;
    public void fallenBowlingPin()
    {
        Debug.Log("A bowling pin has fallen!");
        fallenPins++;
    }

    void Update()
    {
        if (fallenPins == 3)
        {
            FinishText.SetActive(true);
        } 
    }
    
}
