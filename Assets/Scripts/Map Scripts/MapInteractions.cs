using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using TMPro;
using System;

public class MapInteractions : MonoBehaviour
{
    [SerializeField] GameObject ScanResult;
    [SerializeField] UnityEngine.UI.Button[] Scanners;
    [SerializeField] TextMeshProUGUI ScanResultTxt;
    [HideInInspector] public List<Node> nodes;
    SceneControl sc;
    Player_Controller pc;
    string travel;
    public int difficulty=0;
    void Start()
    {
        sc = FindObjectOfType<SceneControl>();
        pc = FindObjectOfType<Player_Controller>();
    }
    private void Update()
    {
        if(pc.Scans > 0)
        {
            foreach(var b in Scanners)
            {
                b.enabled = false;
            }
        }
        else
        {
            foreach (var b in Scanners)
            {
                b.enabled = true;
            }
        }
    }
    public void Scan(int s)
    {
        if(pc.Scans > 0)
        {
            pc.Scans--;
            travel = "";
            switch (s)
            {
                case 0://preorder
                       //+ "-L: " + sc.CurrentNode.Layer
                    travel = sc.CurrentNode.name;
                    PreOrden(sc.CurrentNode);
                    break;
                case 1://inorder
                    InOrden(sc.CurrentNode);
                    travel = travel.Remove(0, 1);
                    recorrido.Clear();
                    break;
                case 2://postorden
                    PostOrden(sc.CurrentNode);
                    travel = travel.Remove(0, 1);
                    recorrido.Clear();
                    break;
            }

            ScanResultTxt.text = travel;
            ScanResult.SetActive(true);
        }
    }
    public void NodosActuales()
    {
        ScanResultTxt.text = "";
        foreach (Node x in nodes)
        {
            ScanResultTxt.text += x.name+", ";
        }
        ScanResult.SetActive(true);
    }
    void PreOrden(Node node)
    {
        foreach (Node_Conection nc in node.Node_Conections)
        {
            if(nc.Conection_Child.N_Type != NodeType.Boss)
            {
                //+ "-L: " + nc.Conection_Child.Layer
                if(nc.Conection_Child.N_Type != NodeType.Normal)
                {
                    travel += "," + nc.Conection_Child.name+"["+ nc.Conection_Child.N_Type+"]";
                }
                else
                {
                    travel += "," + nc.Conection_Child.name;
                }
                PreOrden(nc.Conection_Child);
            }
        }
        
    }
    private List<string> recorrido= new List<string>();
    void InOrden(Node node)
    {
        if (node.Node_Conections[0].Conection_Child.N_Type !=NodeType.Boss)
        {
            foreach (Node_Conection nc in node.Node_Conections.FindAll(x => x.Conection_Type == ConectionType.normal))
            {
                InOrden(nc.Conection_Child);
                if (!recorrido.Contains(node.name))
                {
                    travel += "," + node.name + "[" + node.N_Type + "]";
                    recorrido.Add(node.name);
                }
            }
        }
        else
        {
            travel += ","+node.name;
            recorrido.Add(node.name);
        }
        
    }
    void PostOrden(Node node)
    {
        if (node.Node_Conections.FindAll(x => x.Conection_Child.N_Type == NodeType.Boss).Count == 0)
        {
            foreach (Node_Conection nc in node.Node_Conections.FindAll(x => x.Conection_Type == ConectionType.normal))
            {
                PostOrden(nc.Conection_Child);
            }
            if (!recorrido.Contains(node.name))
            {
                travel += "," + node.name + "[" + node.N_Type + "]";
                recorrido.Add(node.name);
            }
        }
        else
        {
            if (!recorrido.Contains(node.name))
            {
                travel += "," + node.name + "[" + node.N_Type + "]";
                recorrido.Add(node.name);
            }
        }
    }
    void ScanChilds(Node target)
    {
        foreach(Node_Conection x in target.Node_Conections)
        {
            if(x.Conection_Type == ConectionType.normal)
            {
                difficulty += x.Conection_Child.Lvl;
                ScanChilds(x.Conection_Child);
            }
        }
        ScanResult.SetActive(true);
        ScanResultTxt.text = difficulty + " Total Strenght";
    }
}
