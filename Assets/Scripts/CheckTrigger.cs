using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    public bool is_colided_with_objet;
    // Start is called before the first frame update
    void Start()
    {
        is_colided_with_objet = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        PlaceObjectOnPlane placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();

        placeObjectOnPlaneScript.text2.text += "\n Засек столкновения c " + collision.transform.gameObject.name + "\n";
        if (collision.transform.gameObject.name.StartsWith("Object"))
        {
            is_colided_with_objet = true;
            /*PlaceObjectOnPlane placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();
            placeObjectOnPlaneScript.is_choosed = false;
            placeObjectOnPlaneScript.is_selected = false;
            Destroy(placeObjectOnPlaneScript.selectedObject);
            placeObjectOnPlaneScript.text2.text += "Встретился с object";*/
        }
        else
        {
            is_colided_with_objet = false;
        }
    }

  /*  void OnTriggerStay(Collider other)
    {
        PlaceObjectOnPlane placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();
        placeObjectOnPlaneScript.text2.text += "\n Засек столкновения c " + other.gameObject.name +"\n";
        if (other.gameObject.name.StartsWith("Object"))
        {
            //PlaceObjectOnPlane placeObjectOnPlaneScript = FindObjectOfType<PlaceObjectOnPlane>();
            placeObjectOnPlaneScript.is_choosed = false;
            placeObjectOnPlaneScript.is_selected = false;
            Destroy(this);
            placeObjectOnPlaneScript.text2.text += "Встретился с object";
        }
        {
            this.GetComponent<Collider>().isTrigger = false;
         //   this.GetComponent<Rigidbody>().isKinematic = true;
          //  this.GetComponent<Rigidbody>().detectCollisions = false;
            placeObjectOnPlaneScript.text2.text += "Отрубил из триггеред \n";
        }
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Collider>().isTrigger = false;
     //   this.GetComponent<Rigidbody>().isKinematic = true;
     //   this.GetComponent<Rigidbody>().detectCollisions = false;
    }*/
}
