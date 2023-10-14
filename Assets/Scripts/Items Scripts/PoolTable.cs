using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

[CreateAssetMenu(fileName ="new itempool",menuName = "CreateItemPool")]
public class PoolTable : ScriptableObject
{
    [SerializeField] List<Items> Pool;

    public Items GetRandomFunction(int rarity)
    {
        var temp = Pool.FindAll(x => x is Function && x.ItemRarity == rarity);
        return temp[Random.Range(0, temp.Count)];
    }
    public Items GetRandomVariable(int rarity)
    {
        var temp = Pool.FindAll(x => x is Variable && x.ItemRarity == rarity);
        return temp[Random.Range(0, temp.Count)];
    }
    public Items GetRandomItem(int lowerRarity)
    {
        List<Items> temp = new List<Items>();
        int roll = Random.Range(lowerRarity, 10);
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
        else
        {
            return null;
        }
        
    }

}
