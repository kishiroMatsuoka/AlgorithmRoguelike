using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnd : MonoBehaviour
{
    [SerializeField] Transform Slot1, Slot2;
    [SerializeField] GameObject LootZone, Lootprefab;
    Combat_controller cc;
    Player_Controller pc;
    private void Start()
    {
        cc = FindObjectOfType<Combat_controller>();
        pc = FindObjectOfType<Player_Controller>();
    }
    private void OnEnable()
    {
        if(Slot1.gameObject.activeSelf || Slot2.gameObject.activeSelf)
        {
            foreach(Transform x in Slot1) { Destroy(x); }
            foreach(Transform x in Slot2) { Destroy(x); }
        }
        GetLoot();
    }
    void GetLoot()
    {
        Slot1.gameObject.SetActive(true);
        var item1 = cc.loot.GetRandomItem(0);
        var x = Instantiate(Lootprefab, Slot1);
        x.GetComponent<LootPreview>().itemdata = item1;
        pc._inventory.AddToInventory(item1);
        if (Random.Range(0,2) > 0)
        {
            Slot2.gameObject.SetActive(true);
            var item2 = cc.loot.GetRandomItem(0);
            var y = Instantiate(Lootprefab, Slot2);
            y.GetComponent<LootPreview>().itemdata = item2;
            pc._inventory.AddToInventory(item2);

        }
    }

}
