using System;
using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;


namespace Map
{
    public enum ConectionType
    {
        normal,
        lane_change,
        elevator
    }
    public enum NodeType
    {
        Spawn,Normal,Combat,Elite,Heal,Upgrade,Store,Chest,Boss,Random,Blessing
    }
    [System.Serializable]
    public class Node_Conection
    {
        public Node Conection_Father, Conection_Child;
        public ConectionType Conection_Type;
        
    }

    [System.Serializable]
    public class Node
    {
        public string name;
        public GameObject icon;
        public NodeType N_Type;
        public int Lvl;
        public List<GameObject> Node_Enemies = new List<GameObject>();
        public List<Node_Conection> Node_Conections = new List<Node_Conection>();
        public int Layer;
        public void Add_Enemies(GameObject x)
        {
            Node_Enemies.Add(x);
        }
        public void Lvl_calc()
        {
            int temp = 0;
            if (N_Type == NodeType.Combat || N_Type == NodeType.Boss)
            {
                int counter = 0;
                foreach (GameObject x in Node_Enemies)
                {
                    temp =+ x.GetComponent<Enemy>()._enemylvl;
                    counter++;
                }
                temp /= counter;
            }
            Lvl = temp;
        }
        public void AddConection(Node child,Map_Configuration mc)
        {
            Node_Conection x = new ()
            {
                Conection_Child = child,
                Conection_Father = this
            };
            try
            {
                x.Conection_Type = ConectionType.normal;
                Node_Conections.Add(x);
            }
            catch (NullReferenceException e)
            {
                Debug.Log(e);
            }
        }
        
    }
    
}
