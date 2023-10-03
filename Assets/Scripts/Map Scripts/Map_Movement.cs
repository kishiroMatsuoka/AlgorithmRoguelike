using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public class Map_Movement : MonoBehaviour
{
    Camera_zoomnode cam_comp;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam_comp = GameObject.FindGameObjectWithTag("2nd Cam").GetComponent<Camera_zoomnode>();
    }
    
    private void OnMouseDown()
    {
        /*
        print(transform.localPosition);
        print(transform.position);
        Vector3 cuurent_coor = transform.position;
        player.transform.position = cuurent_coor;
        cam_comp.Update_Node(cuurent_coor);
        cam_comp.Change_Camera();
        */
        Node noderef = gameObject.GetComponent<NodeContainer>().Nodeinfo;
        SceneControl sc = GameObject.Find("EventSystem").GetComponent<SceneControl>();
        bool yarnus = true;
        foreach(Node_Conection nc in sc.CurrentNode.Node_Conections)
        {
            if(nc.Conection_Child == noderef)
            {
                NodeType query = gameObject.GetComponent<NodeContainer>().Nodeinfo.N_Type;
                if (query == NodeType.Store)
                {
                    GameObject.Find("EventSystem").GetComponent<SceneControl>().CurrentNode =
                        gameObject.GetComponent<NodeContainer>().Nodeinfo;
                    sc.EnterStore();
                }
                else if (query == NodeType.Combat || query == NodeType.Boss)
                {
                    GameObject.Find("EventSystem").GetComponent<SceneControl>().CurrentNode =
                        gameObject.GetComponent<NodeContainer>().Nodeinfo;
                    sc.EnterCombat();
                }
                else
                {
                    Debug.Log("Valid Node");
                    GameObject.Find("EventSystem").GetComponent<SceneControl>().CurrentNode =
                        gameObject.GetComponent<NodeContainer>().Nodeinfo;
                    yarnus = false;
                }
                break;
            }
        }
        if (yarnus)
        {
            Debug.Log("Node invalid");
        }
    }
}
