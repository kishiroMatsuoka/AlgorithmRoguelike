using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using TMPro;

public class NodeContainer : MonoBehaviour
{
    [SerializeField] TextMeshPro Name;
    public Node Nodeinfo;
    public void SetNode(Node node)
    {
        Nodeinfo = node;
        Name.text = Nodeinfo.name;
    }
    public void UpdateName()
    {
        Name.text = Nodeinfo.name;
    }
}
