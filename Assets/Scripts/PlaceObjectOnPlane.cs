using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
public class PlaceObjectOnPlane : MonoBehaviour
{

    private int indexObject = 0;

    private ARPlaneManager planeManager;
    private ARRaycastManager raycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

   // public GameObject placedPrefab;

    public GameObject selectedObject;


    public Text text;
    public Text text2;
    public Camera cam;


    public GameObject choosedObject;
    public bool is_choosed = false;
    public bool is_selected = false;


    public bool moveObject = false;


    public bool rotationObject = false;
    private Quaternion YRotation;


    public Button deselectButton;
    public Button moveButton;
    public Button rotationButton;


  //  public GameObject anchor;
   // private float mass;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

     void  Update()
    {
        text.text = is_selected.ToString();
        if (selectedObject != null)
        {
            text.text += " " + selectedObject.name;
        }
        else
        {
            text.text += " Без объекта";
        }
        if (Input.touchCount > 0)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Touch touch = Input.GetTouch(0);

        if ( !isPointerOverUIObject())
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);//создаем луч (бесконечный вектор) проходящий от камеры через точку (нажатия по экрану)
            RaycastHit rayHit; //структура используемая для информации о столкновении
            if (Physics.Raycast(ray, out rayHit)) //если луч столкнулся с чем то, rayHit содержит данные об этом "чем-то" out - значение изменится
            {
                text.text = "";
                if (is_selected)
                {
                    text.text += "Объект уже выбран:\n";
                    //передвижениe
                    if (moveObject) //объект ещё выбранный и не заграждает другой объект
                    {
                        text.text += "Двигаю объект\n";
                        raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon); //определяем плоскость и точку в пространстве, к которой должен двигаться объект
                        selectedObject.GetComponent<Rigidbody>().mass = 0.1f;
                        selectedObject.GetComponent<Rigidbody>().MovePosition(hits[0].pose.position);
                        return;
                    }
                    else
                    {
                        selectedObject.GetComponent<Rigidbody>().mass = 100000;
                    }
                    //вращение
                    if (rotationObject)
                    {
                        text.text += "Вращаю объект\n";
                        YRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.2f, 0f);
                        selectedObject.transform.rotation = YRotation * selectedObject.transform.rotation;
                    }

                }
                else
                {
                    text.text += "Объект еще не выбран \n";
                    if (touch.phase == TouchPhase.Began)
                    {
                        //выделение объекта
                        if (rayHit.collider.gameObject.name.StartsWith("Object"))
                        {
                            text.text += "Выделяю объект\n";
                                selectedObject = rayHit.collider.gameObject;

                           // selectedObject = rayHit.collider.transform.parent.gameObject;

                            is_selected = true; //объект выбран, другие действия, кроме взаимодействия с ним будут недоступны
                            selectedObject.GetComponent<Rigidbody>().mass = 0.1f;
                            ////////ОБОЗНАЧИТЬ ВЫДЕЛЕНИЕ/////////////
                            //показ управляющих кнопок.
                            ShowOptionObject();
                            text2.text = "Выбран объект " + selectedObject.name;
                            return;
                        }
                        //создать объект 
                        if (is_choosed &&
                            raycastManager.Raycast(touch.position,
                                                   hits,
                                                   TrackableType.PlaneWithinPolygon))
                        {
                            text.text += "Пытаюсь создать объект\n";
                            Pose hitpose = hits[0].pose; 
                            //HACKING  for Wood Table
                            Quaternion rotation = hitpose.rotation; 
                           /* if (choosedObject.tag == "Wood Table")
                            {
                                rotation.y += 90;
                                text2.text += "Это был стол, перевернул" + rotation.x.ToString() + rotation.y.ToString();
                            }*/
                            GameObject clone = Instantiate(choosedObject, hitpose.position, rotation);
                         //   clone.transform.SetParent(anchor.transform);
                            StartCoroutine(CheckForCollision(clone));
                        }
                    }
                }
                
            } 



        }
       
    }


    IEnumerator CheckForCollision(GameObject clone)
    {
        yield return new WaitForSeconds(0.1f);
        if (!clone.GetComponent<CheckTrigger>().is_colided_with_objet)
        {
            //показать управляющие кнопки
            ShowOptionObject();
            clone.name = "Object " + indexObject;
            indexObject++;
            is_selected = true;
            selectedObject = clone;
            selectedObject.GetComponent<Rigidbody>().mass = 0.1f;
            text2.text = "Создан и выбран объект" + selectedObject.name;
        }
        else
        {
             Destroy(clone);
             text2.text += "Встретился с object"; 
             text.text += "Объект создать не удалось \n";
        }
    }

    private void ShowOptionObject()
    {
        deselectButton.gameObject.SetActive(true);
        moveButton.gameObject.SetActive(true);
        rotationButton.gameObject.SetActive(true);
    }

    public bool isPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);


        return results.Count > 0;
    }




}
