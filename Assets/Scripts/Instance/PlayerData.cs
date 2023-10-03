using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerData/CreatePlayerData",fileName ="NewPlayerData")]
public class PlayerData : ScriptableObject
{
    public playerClass p_class;
    public int hp;
    public int maxhp;
    public int def;
    public int maxcost;
    public Inventory inventory;

    public enum playerClass
    {
        Warrior,Thief,Mage,Berserker,Test 
    }
}
