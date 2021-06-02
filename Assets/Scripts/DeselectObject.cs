using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeselectObject : MonoBehaviour
{

    private Button button;
    private PlaceObjectOnPlane placeObjectOnPlaneScript;

    public Button deselectButton;
    public Button rotationButton;
    public Button moveButton;
  void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(DeselectFunction);

        placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();

        gameObject.SetActive(false);
    }
    public void DeselectFunction()
    {
        //  placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();

     //   placeObjectOnPlaneScript.text2.text = "Нажал на " + deselectButton.name + "\n";
        placeObjectOnPlaneScript.moveObject = false;
        moveButton.GetComponent<Image>().color = Color.white;
        moveButton.gameObject.SetActive(false);

        placeObjectOnPlaneScript.rotationObject = false;
        rotationButton.GetComponent<Image>().color = Color.white;
        rotationButton.gameObject.SetActive(false);

  //      placeObjectOnPlaneScript.text.text = "Выберите объект";
   //     placeObjectOnPlaneScript.text2.text = "Объект не выбран";

        if (placeObjectOnPlaneScript.is_selected)
        {
      //      placeObjectOnPlaneScript.text2.text += "Объект был выбран \n";
            placeObjectOnPlaneScript.selectedObject.GetComponent<Rigidbody>().mass = 10000000.0f;
       //     placeObjectOnPlaneScript.text2.text += "Изменил массу \n";

            placeObjectOnPlaneScript.is_selected = false;
            placeObjectOnPlaneScript.is_choosed = false;
      //      placeObjectOnPlaneScript.text2.text += "Удалил метку 'выбрано' \n";
        }
        deselectButton.gameObject.SetActive(false);
       // placeObjectOnPlaneScript.text2.text += "убирал видимость " + gameObject.name + "на " + deselectButton.gameObject.active;


        //   }
    }
}
