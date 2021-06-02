using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseObject : MonoBehaviour
{
    public PlaceObjectOnPlane placeObjectOnPlaneScript;
    public GameObject button;

    public GameObject assignedObject;
    public GameObject selectItemPanel;
    public GameObject workPanel;

    public void SetParams(GameObject new_button, GameObject new_assignedObject, 
                          GameObject new_selectItemPanel, GameObject new_workPanel)
    {
        Debug.Log("Устанавливаю параметры ");
        button = new_button;
        Debug.Log("Кнопка скоппирована");
        assignedObject = new_assignedObject;
        Debug.Log("объект присвоен");
        selectItemPanel = new_selectItemPanel;
        Debug.Log("Есть панель выбора объекта");
        workPanel = new_workPanel;
        Debug.Log("Есть главная панель");
    }

    void Start()
    {
        Debug.Log("функция start кнопки");
        placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();
       // button = GetComponent<Button>();
        button.GetComponent<Button>().onClick.AddListener(Choose);
    }

    // Update is called once per frame
    void Choose()
    {
        Debug.Log("Навешиваю листенера");
        workPanel.SetActive(true);
        placeObjectOnPlaneScript.choosedObject = assignedObject;
        placeObjectOnPlaneScript.is_choosed = true;
    selectItemPanel.SetActive(false);
    }
}
