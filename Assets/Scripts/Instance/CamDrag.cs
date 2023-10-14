using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDrag : MonoBehaviour
{
    [SerializeField] float wheelSpeed = 10f;
    private Vector3 origin, difference;
    Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (cam.enabled)
        {
            float wheel = Input.mouseScrollDelta.y;
            //print(wheel);
            if (wheel > 0)//zoom in
            {
                if (cam.orthographicSize > 3.92f)
                {
                    cam.orthographicSize -= Time.deltaTime * wheelSpeed;
                }
                else if (cam.orthographicSize < 3.92f) { cam.orthographicSize = 3.92f; }
                
            }
            else if(wheel < 0) {//zoom out
                if (cam.orthographicSize < 6f)
                {
                    cam.orthographicSize += Time.deltaTime * wheelSpeed;
                }
                else if (cam.orthographicSize >= 6f) { cam.orthographicSize = 6f; }
            }
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
}
