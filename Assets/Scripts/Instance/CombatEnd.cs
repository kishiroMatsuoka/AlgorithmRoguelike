using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnd : MonoBehaviour
{
    [SerializeField] Transform Slot1, Slot2;
    [SerializeField] GameObject LootZone, Lootprefab;
    [SerializeField] Combat_controller cc;
    Player_Controller pc;
    [SerializeField] TMPro.TextMeshProUGUI CombatPoints;
    private void OnEnable()
    {
        pc = FindObjectOfType<Player_Controller>();
        Time.timeScale = 0;
        pc.Score += cc.combat_score;
        CombatPoints.text = 
            cc.combat_score + " Points\n" +
            cc.turn_counter+" Turnos\n"+
            cc.totaldmg +" Daño total recivido"
            ;
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
        if(holder1 != null) { Destroy(holder1);holder1 = null; Slot1.gameObject.SetActive(false); }
        if(holder2 != null) { Destroy(holder2);holder2 = null; Slot2.gameObject.SetActive(false); }
        FindObjectOfType<SceneControl>().ExitCombat();
    }
    GameObject holder1=null, holder2=null;
    void GetLoot()
    {
        Slot1.gameObject.SetActive(true);
        var item1 = pc.Combat.GetRandomItem(0);
        var x = Instantiate(Lootprefab, Slot1);
        x.transform.localPosition = Vector3.zero;
        x.GetComponent<LootPreview>().SetName(item1);
        holder1 = x;
        pc._inventory.AddToInventory(item1);
        if (Random.value > .7f)
        {
            Slot2.gameObject.SetActive(true);
            var item2 = pc.Combat.GetRandomItem(0);
            var y = Instantiate(Lootprefab, Slot2);
            y.transform.localPosition = Vector3.zero;
            y.GetComponent<LootPreview>().SetName(item2);
            holder2 = y;
            pc._inventory.AddToInventory(item2);

        }
    }

}
