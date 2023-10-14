using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Function> functions = new List<Function>();
    public List<Variable> variables = new List<Variable>();
    public List<Equipment> equipment = new List<Equipment>();
    public List<Consumibles> consumibles = new List<Consumibles>();
    public List<GameObject> used_consumibles = new List<GameObject>();

    public Function GetRandomFunction(int rarity)
    {
        var temp = functions.FindAll(x => x.ItemRarity == rarity);
        if (temp.Count > 0) { return temp[Random.Range(0, temp.Count)]; }
        else { return null; }
        
    }
    public Variable GetRandomVariable(int rarity)
    {
        var temp = variables.FindAll(x => x.ItemRarity == rarity);
        if (temp.Count > 0) { return temp[Random.Range(0, temp.Count)]; }
        else { return null; }

    }
    public Items GetRandomItem()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                if (variables.Count > 0) { return variables[Random.Range(0, variables.Count)]; }
                else { return null; }
            case 1:
                if (functions.Count > 0) { return functions[Random.Range(0, functions.Count)]; }
                else { return null; }
            case 2:
                if (consumibles.Count > 0) { return consumibles[Random.Range(0, consumibles.Count)]; }
                else { return null; }
            default:
                return null;
        }
    }
    public void RemoveFunction(Function f)
    {
        functions.Remove(f);
    }
    public void RemoveVariable(Variable v)
    {
        variables.Remove(v);
    }
    public void RemoveConsumable(Consumibles c)
    {
        consumibles.Remove(c);
    }
    public void AddToInventory(Items target)
    {
        if(target is Function)
        {
            functions.Add((Function)target);
        }
        else if(target is Variable)
        {
            variables.Add((Variable)target);
        }
        else if (target is Equipment)
        {
            equipment.Add((Equipment)target);
        }
    }
    public void RemoveFromInventory(Items target)
    {
        if (target is Function)
        {
            functions.Remove((Function)target);
        }
        else if (target is Variable)
        {
            variables.Remove((Variable)target);
        }
        else if (target is Equipment)
        {
            equipment.Remove((Equipment)target);
        }
    }
}