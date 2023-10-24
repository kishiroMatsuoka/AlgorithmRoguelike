using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public class Map_Movement : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr,unk;
    Camera_zoomnode cam_comp;
    MapUi mu;
    SceneControl sc;
    Node noderef;
    [SerializeField] bool BossNode = false;
    private void Start()
    {
        cam_comp = GameObject.FindGameObjectWithTag("2nd Cam").GetComponent<Camera_zoomnode>();
        sc = GameObject.Find("EventSystem").GetComponent<SceneControl>();
        mu = FindObjectOfType<MapUi>();
        noderef = GetComponent<NodeContainer>().Nodeinfo;
    }
    private void Update()
    {
        noderef = GetComponent<NodeContainer>().Nodeinfo;
        if (sr.enabled == false && noderef.Layer == (sc.CurrentNode.Layer+1))
        {
            foreach(Node_Conection nc in sc.CurrentNode.Node_Conections)
            {
                if(nc.Conection_Child == noderef)
                {
                    sr.enabled = true;
                    if (!BossNode)
                    {
                        unk.enabled = false;
                    }
                    break;
                }
            }
        }
        else
        {
            if(sr.enabled == true && noderef.Layer  <= (sc.CurrentNode.Layer)) { sr.enabled = false; }
        }
    }
    private void OnMouseDown()
    {
        foreach(Node_Conection nc in sc.CurrentNode.Node_Conections)
        {
            if(nc.Conection_Child == noderef)
            {
                sc.CurrentNode = noderef;
                sc.CNodeObject = gameObject.transform;
                mu.UpdateArrow(transform.position);
                Debug.Log("transform.postition:" + transform.position);
                switch (noderef.N_Type)
                {

                    case NodeType.Combat:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(0);
                        break;
                    case NodeType.Elite:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(0);
                        break;
                    case NodeType.Boss:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(0);
                        break;
                    case NodeType.Store:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(1);
                        break;
                    case NodeType.Random:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(2);
                        break;
                    case NodeType.Upgrade:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(3);
                        break;
                    case NodeType.Heal:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(4);
                        break;
                    case NodeType.Chest:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(5);
                        break;
                    case NodeType.Blessing:
                        cam_comp.Update_Node(transform.position);
                        cam_comp.Change_Camera(6);
                        break;
                }
                break;
            }
        }
    }
}
