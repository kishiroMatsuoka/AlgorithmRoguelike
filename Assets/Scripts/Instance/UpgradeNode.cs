using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeNode : MonoBehaviour
{
    [SerializeField] GameObject PreviewPrefab, OptionsPanel, Results, SlotOld, SlotNew;
    [SerializeField] TextMeshProUGUI StatsOld, StatsNew;
    Player_Controller pc;
    int stat;
    void OnEnable()
    {
        pc = FindObjectOfType<Player_Controller>();
    }
    public void Effect(int effect)
    {
        OptionsPanel.SetActive(false);
        switch (effect)
        {
            case 0: //hp
                stat = pc._maxhp;
                pc._maxhp += pc._maxhp / 10;
                ShowResult(0);
                break;
            case 1: //def
                stat = pc._def;
                pc._def += pc._def / 10;
                ShowResult(1);
                break;
            case 2: //random Upg
                var x = pc._inventory.GetRandomFunction(Random.Range(0, 10));
                while (true)
                {
                    if(x != null) { break; }
                    else { x = pc._inventory.GetRandomFunction(Random.Range(0, 10)); }
                }
                var function = pc.General.GetRandomFunction(x.ItemRarity+1);
                var old = Instantiate(PreviewPrefab, SlotOld.transform);
                old.GetComponent<LootPreview>().itemdata = x;
                old.name = "OldItem";
                var prev = Instantiate(PreviewPrefab, SlotNew.transform);
                prev.GetComponent<LootPreview>().itemdata = function;
                prev.name = "NewItem";
                pc._inventory.RemoveFunction(x);
                pc._inventory.AddToInventory(function);
                ShowResult(2);
                break;
            case 3: //random up var
                var y = pc._inventory.GetRandomVariable(Random.Range(0, 10));
                while (true)
                {
                    if (y != null) { break; }
                    else { y = pc._inventory.GetRandomVariable(Random.Range(0, 10)); }
                }
                var variable = pc.General.GetRandomVariable(y.ItemRarity + 1);
                var oldvar = Instantiate(PreviewPrefab, SlotOld.transform);
                oldvar.GetComponent<LootPreview>().itemdata = y;
                oldvar.name = "OldItem";
                var prevvar = Instantiate(PreviewPrefab, SlotNew.transform);
                prevvar.GetComponent<LootPreview>().itemdata = variable;
                prevvar.name = "NewItem";
                pc._inventory.RemoveVariable(y);
                pc._inventory.AddToInventory(variable);
                ShowResult(2);
                break;
        }
    }
    void ShowResult(int result)
    {
        Results.SetActive(true);
        switch (result)
        {
            case 0: //hp
                StatsOld.gameObject.SetActive(true);
                StatsNew.gameObject.SetActive(true);
                StatsOld.text = stat + " Hp";
                StatsNew.text = pc._maxhp + " Hp";
                break;
            case 1: //def
                StatsOld.gameObject.SetActive(true);
                StatsNew.gameObject.SetActive(true);
                StatsOld.text = stat + " Defense";
                StatsNew.text = pc._def + " Defense";
                break;
            case 2: //random Upg
                SlotNew.SetActive(true);
                SlotOld.SetActive(true);
                break;
        }
    }
    public void GoBack()
    {
        try
        {
            Destroy(GameObject.Find("NewItem"));
            Destroy(GameObject.Find("OldItem"));
        }
        catch { }
        Results.SetActive(false);
        OptionsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
