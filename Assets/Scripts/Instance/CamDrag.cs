using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDrag : MonoBehaviour
{
    private Vector3 origin, difference;
    Camera cam;
    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            origin = cam.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButton(1))
        {
            difference = origin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
        }
        
    }
}
