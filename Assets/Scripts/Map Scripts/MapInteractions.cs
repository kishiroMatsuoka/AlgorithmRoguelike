using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using TMPro;
using System;

public class MapInteractions : MonoBehaviour
{
    [SerializeField] GameObject ScanResult;
    [SerializeField] TextMeshProUGUI ScanResultTxt;
    [HideInInspector] public List<Node> nodes;
    SceneControl sc;
    string travel;
    public int difficulty=0;
    void Start()
    {
        sc = FindObjectOfType<SceneControl>();
    }
    public void RenameSimple(int layer)
    {
        print("Rename Called: int layer= "+layer);
        int letter = 66;
        for(int x =1; x <= layer; x++)
        {
            print("For Cycle: x ="+x);
            var t = nodes.FindAll(n => n.Layer == x);
            print("Nodes finded: " + t.Count);
            foreach(Node n in t)
            {
                print(Convert.ToChar(letter).ToString());
                n.name = Convert.ToChar(letter).ToString();
                letter++;
            }
        }
        var a = FindObjectsOfType<NodeContainer>();
        foreach(NodeContainer nc in a)
        {
            print("UpdateName Called");
            nc.UpdateName();
        }
    }
    public void Scan(int s)
    {
        switch (s)
        {
            case 0://preorder
                //+ "-L: " + sc.CurrentNode.Layer
                travel = sc.CurrentNode.name;
                PreOrden(sc.CurrentNode);
                break;
            case 1://inorder

                break;
            case 2://postorden

                break;
        }
        //ScanChilds(sc.CurrentNode);
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
                travel += "," + nc.Conection_Child.name;
                PreOrden(nc.Conection_Child);
            }
        }
        ScanResultTxt.text = travel;
        ScanResult.SetActive(true);
    }
    void InOrden(Node node)
    {
        foreach (Node_Conection nc in node.Node_Conections)
        {
            PreOrden(nc.Conection_Child);
            travel += "," + nc.Conection_Child + "-L: " + nc.Conection_Child.Layer;
        }
    }
    void PostOrden(Node node)
    {
        foreach (Node_Conection nc in node.Node_Conections)
        {
            travel += "," + nc.Conection_Child + "-L: " + nc.Conection_Child.Layer;
            PreOrden(nc.Conection_Child);
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
