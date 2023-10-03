using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public class NodeContainer : MonoBehaviour
{
    public Node Nodeinfo;
    public void SetNode(Node node)
    {
        Nodeinfo = node;
    }
    private void OnMouseDown()
    {
        Debug.Log("node pressed");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 0.6f);
    }
}
