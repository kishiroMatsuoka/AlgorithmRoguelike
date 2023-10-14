using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Map;
public class MapInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    SceneControl sc;
    Camera cam;
    Node n = null;
    private void Start()
    {
        sc = FindObjectOfType<SceneControl>();
        cam = Camera.main;
    }
    void Update()
    {
        if (cam.enabled)
        {
            if(sc.CurrentNode != n)
            {
                List<Node> nc = new List<Node>();
                foreach(Node_Conection x in sc.CurrentNode.Node_Conections) { nc.Add(x.Conection_Child); }
                txt.text = 
                    "-Node: "+sc.CurrentNode.name+
                    "\n-Hijos/Ramas:";
                foreach(Node no in nc) { 
                    txt.text += 
                        "\n" + no.name; }
                txt.text += 
                    "\n-Layer/Profundidad: " + sc.CurrentNode.Layer+
                    "\n-Dificultad/Valor: "+sc.CurrentNode.Lvl;
                n = sc.CurrentNode;
            }
            //transform.position = cam.WorldToScreenPoint(sc.CNodeObject.position);
        }
    }
}
