using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderUpdate : MonoBehaviour
{
    Transform NextNode;
    LineRenderer lr;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    public void SetReference(Transform EndPoint)
    {
        NextNode = EndPoint;
        lr.SetPosition(1, NextNode.position);
    }
    public void UpdateLine()
    {
        lr.SetPosition(1, NextNode.position);
    }
}
