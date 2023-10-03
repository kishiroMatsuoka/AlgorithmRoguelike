using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

public class PoolTable : ScriptableObject
{
    [SerializeField] List<Items> Pool;

    public Items GetRandomItem()
    {
        List<Items> temp = new List<Items>();
        int roll = Random.Range(0, 10);
        foreach(Items i in Pool)
        {
            if (i.ItemRarity <= roll)
            {
                temp.Add(i);
            }
        }
        if(temp.Count > 0)
        {
            return temp[Random.Range(0,temp.Count)];
        }
        return null;
    }

}
