using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Create NPC")]
public class NPC : ScriptableObject
{
    public Sprite npc_sprite;
    public string npc_name;
    public int npc_dmg;
    public int npc_health;
    public int npc_defense;
    public bool npc_usesMagic;
    public enum NPC_Class
    {
        warrior,
        tank,
        mage,
        thief
    }
}
