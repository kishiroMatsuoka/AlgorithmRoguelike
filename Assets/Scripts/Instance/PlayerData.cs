using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerData/CreatePlayerData",fileName ="NewPlayerData")]
public class PlayerData : ScriptableObject
{
    public playerClass p_class;
    public int hp;
    public int def;
    public int maxcost;
    public Inventory inventory;
    [TextArea(10, 15)]
    public string description;
    public enum playerClass
    {
        Warrior,Thief,Mage,Berserker,Tank 
    }
}
