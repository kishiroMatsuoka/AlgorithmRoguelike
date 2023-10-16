using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnd : MonoBehaviour
{
    [SerializeField] Transform Slot1, Slot2;
    [SerializeField] GameObject LootZone, Lootprefab;
    [SerializeField] Combat_controller cc;
    Player_Controller pc;
    private void Start()
    {
        pc = FindObjectOfType<Player_Controller>();
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        pc.Score += cc.combat_score;
        if(Slot1.gameObject.activeSelf || Slot2.gameObject.activeSelf)
        {
            foreach(Transform x in Slot1) { Destroy(x); }
            foreach(Transform x in Slot2) { Destroy(x); }
        }
        GetLoot();
    }
    public void ReturnToMap()
    {
        Time.timeScale = 1;
        FindObjectOfType<SceneControl>().ExitCombat();
    }
    void GetLoot()
    {
        Slot1.gameObject.SetActive(true);
        var item1 = pc.Combat.GetRandomItem(0);
        var x = Instantiate(Lootprefab, Slot1);
        x.GetComponent<LootPreview>().itemdata = item1;
        pc._inventory.AddToInventory(item1);
        if (Random.value > .7f)
        {
            Slot2.gameObject.SetActive(true);
            var item2 = pc.Combat.GetRandomItem(0);
            var y = Instantiate(Lootprefab, Slot2);
            y.GetComponent<LootPreview>().itemdata = item2;
            pc._inventory.AddToInventory(item2);

        }
    }

}
