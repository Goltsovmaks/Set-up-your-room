using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    private Button movementButton;
    private PlaceObjectOnPlane placeObjectOnPlaneScript;


    public Button rotationButton;

    void Start()
    {
        movementButton = GetComponent<Button>();
        movementButton.onClick.AddListener(MoveFunct);

        placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();
        movementButton.gameObject.SetActive(false);

    }
    void MoveFunct()
    {

        if (placeObjectOnPlaneScript.moveObject) //выкл
        {
            placeObjectOnPlaneScript.moveObject = false;
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            placeObjectOnPlaneScript.moveObject = true;//вкл
            GetComponent<Image>().color = Color.green;

            placeObjectOnPlaneScript.rotationObject = false;//деактивируем вращение
            rotationButton.GetComponent<Image>().color = Color.white;

        }
    }


 
}
