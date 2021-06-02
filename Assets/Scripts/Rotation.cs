using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rotation : MonoBehaviour
{
    private Button button;
    private PlaceObjectOnPlane placeObjectOnPlaneScript;


    public Button moveButton;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(RotationFunction);

        placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void RotationFunction()
    {
        if (placeObjectOnPlaneScript.rotationObject) //вкл
        {
            placeObjectOnPlaneScript.rotationObject = false;
            GetComponent<Image>().color = Color.white;
        }
        else // выкл
        {
            placeObjectOnPlaneScript.rotationObject = true;
            GetComponent<Image>().color = Color.green;

            placeObjectOnPlaneScript.moveObject = false;//деактивируем движуху
            moveButton.GetComponent<Image>().color = Color.white;
        }
    }
}
