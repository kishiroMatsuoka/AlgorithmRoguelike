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
}