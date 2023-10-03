using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnd : MonoBehaviour
{
    [SerializeField] GameObject LootZone;
    Combat_controller cc;
    Player_Controller pc;
    private void Start()
    {
        cc = FindObjectOfType<Combat_controller>();
        pc = FindObjectOfType<Player_Controller>();
    }
    private void OnEnable()
    {
        
    }
    void GetLoot()
    {
        if(Random.Range(0,2) > 0)
        {
            for(int i = 0; i < 2; i++)
            {
                pc._inventory.AddToInventory(cc.loot.GetRandomItem());
            }
        }
    }

}
