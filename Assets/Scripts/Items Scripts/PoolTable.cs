using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

[CreateAssetMenu(fileName ="new itempool",menuName = "CreateItemPool")]
public class PoolTable : ScriptableObject
{
    [SerializeField] List<Function> PoolFunctions = new List<Function>();
    [SerializeField] List<Variable> PoolVariables = new List<Variable>();
    [SerializeField] List<Consumibles> PoolConsumibles = new List<Consumibles>();
    public Function GetRandomFunction(int rarity)
    {
        var temp = PoolFunctions.FindAll(x => x.ItemRarity <= rarity);
        if (temp.Count > 0)
        {
            return temp[Random.Range(0, temp.Count)];
        }
        else
        {
            return null;
        }
    }
    public Variable GetRandomVariable(int rarity)
    {
        var temp = PoolVariables.FindAll(x => x.ItemRarity <= rarity);
        if (temp.Count > 0)
        {
            return temp[Random.Range(0, temp.Count)];
        }
        else
        {
            return null;
        }
    }
    public Consumibles GetRandomConsumibles(int rarity)
    {
        var temp = PoolConsumibles.FindAll(x => x.ItemRarity <= rarity);
        if (temp.Count > 0)
        {
            return temp[Random.Range(0, temp.Count)];
        }
        else
        {
            return null;
        }
    }
    public Items GetRandomItem(int lowerRarity)
    {
        return Random.Range(0, 3) switch
        {
            0 => GetRandomFunction(lowerRarity),
            1 => GetRandomVariable(lowerRarity),
            2 => GetRandomConsumibles(lowerRarity),
            _ => null,
        };
    }

}
