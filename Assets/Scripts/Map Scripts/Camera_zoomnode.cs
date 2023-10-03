using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_zoomnode : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    Camera Cam_main;
    Camera Cam_second;
    private void Start()
    {
        Cam_main = Camera.main;
        Cam_second = GetComponent<Camera>();
    }
    public void Update_Node(Vector3 node_coor)
    {
        Vector3 camera_coor = transform.position;
        node_coor.z = camera_coor.z;
        transform.position = node_coor;
    }
    public void Change_Camera()
    {
        if (Cam_main.enabled)
        {
            Cam_main.enabled = false;
            Cam_second.enabled = true;
            Canvas.SetActive(true);
        }
        else
        {
            Cam_main.enabled = true;
            Cam_second.enabled = true;
            Canvas.SetActive(false);
        }
    }
        
}
