using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

[CreateAssetMenu(fileName = "map configuration", menuName = "Map/New Configuration")]
public class Map_Configuration : ScriptableObject
{
    public List<Node> nodes;
    public int Map_MaxLevel;
    public int Map_Layers;
    public int Map_Bosses;//
    public int Map_Elites;
    public int Map_Complexity;//max branches of map
    public int Map_Stores;//max number of stores
}