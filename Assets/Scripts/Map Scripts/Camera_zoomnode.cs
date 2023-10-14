using UnityEngine;

public class Camera_zoomnode : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject StoreUi, CombatUi, Random, Upgrade;
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
    public void Change_Camera(int ui)
    {
        if (Cam_main.enabled)
        {
            Cam_main.enabled = false;
            Cam_second.enabled = true;
            switch (ui)
            {
                case 0://combat
                    CombatUi.SetActive(true);
                    break;
                case 1://store
                    StoreUi.SetActive(true);
                    break;
                case 2://random
                    Random.SetActive(true);
                    break;
                case 3://upgrade
                    Upgrade.SetActive(true);
                    break;
            }
        }
        else
        {
            Cam_main.enabled = true;
            Cam_second.enabled = true;
            StoreUi.SetActive(false);
            CombatUi.SetActive(false);
            Random.SetActive(false);
            Upgrade.SetActive(false);
        }
    }
        
}
