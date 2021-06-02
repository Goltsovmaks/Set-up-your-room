using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(-90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
     //   transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
     //   transform.Rotate(new Vector3(90, 45, 0) * Time.deltaTime);
     //   transform.Rotate(new Vector3(90, 45, 0) * Time.deltaTime);
    }
}
