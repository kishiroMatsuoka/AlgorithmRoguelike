using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
public class Map_Object : MonoBehaviour
{
    [HideInInspector] public List<GameObject> Map_Nodes = new List<GameObject>();
    private GameObject Current_Node;
    [SerializeField] private int Nodes_Count;
    public int Map_lvl;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void AddNode(GameObject target)
    {
        Map_Nodes.Add(target);
        Nodes_Count = Map_Nodes.Count;
    }
    public void SetCurrentNode(GameObject c_target)
    {
        if (Map_Nodes.Contains(c_target))
        {
            Current_Node = c_target;
        }
        else
        {
            Debug.Log("Error nodo no existe en contexto");
        }
    }
    public GameObject getCurrentNode()
    {
        return Current_Node;
    }
}



