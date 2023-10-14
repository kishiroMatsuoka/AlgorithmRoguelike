using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomEvent : MonoBehaviour
{
    [SerializeField] GameObject PreviewPrefab, SlotPreview;
    [SerializeField] GameObject TextResults, ItemResults, Arrow;
    [SerializeField] TextMeshProUGUI TextTitle, StatsNew,StatsOld,GoldGained;
    Player_Controller pc;
    void OnEnable()
    {
        pc = FindObjectOfType<Player_Controller>();
        GenerateRandomEvent();
    }

    // Update is called once per frame
    void GenerateRandomEvent()
    {
        int ran = Random.Range(0, 5);
        SceneControl sc = FindObjectOfType<SceneControl>();
        switch (ran)
        {
            case 0://get random gold
                money = Random.Range(25, 251);
                pc.money += money;
                //message
                break;
            case 1: //heal by chance
                float rf = Random.Range(0f, 100f);
                statmod = pc._hp;
                if(rf > 95f)
                {
                    pc._hp = pc._maxhp;
                    foreach(NPC_Controller nc in pc.party)
                    {
                        nc._health = nc._maxHealth;
                    }
                }
                else if(rf > 80f)
                {
                    pc._hp += pc._maxhp/2;
                    pc.CheckStatus();
                    foreach (NPC_Controller nc in pc.party)
                    {
                        nc._health += nc._maxHealth/2;
                        nc.CheckStatus();
                    }
                }
                else if(rf > 30f)
                {
                    pc._hp += pc._maxhp / 4;
                    pc.CheckStatus();
                    foreach (NPC_Controller nc in pc.party)
                    {
                        nc._health += nc._maxHealth / 4;
                        nc.CheckStatus();
                    }
                }
                else if(rf > 5f)
                {
                    pc._hp += pc._maxhp / 10;
                    pc.CheckStatus();
                    foreach (NPC_Controller nc in pc.party)
                    {
                        nc._health += nc._maxHealth / 10;
                        nc.CheckStatus();
                    }
                }
                else
                {
                    pc._hp -= pc._maxhp / 10;
                    pc.CheckStatus();
                    foreach (NPC_Controller nc in pc.party)
                    {
                        nc._health -= nc._maxHealth / 10;
                        nc.CheckStatus();
                    }
                }
                break;
            case 2://Get Random item
                x = pc.General.GetRandomItem(Random.Range(0, 10));
                while (true)
                {
                    if (x != null) { break; }
                    else { x = pc.General.GetRandomFunction(Random.Range(0, 10)); }
                }
                break;
            case 3://Get Random Companion
                var d = sc.npc_pre[Random.Range(0, sc.npc_pre.Length)];
                npcname = d.GetComponent<NPC_Controller>()._name;
                Instantiate(d);
                pc.party.Add(d.GetComponent<NPC_Controller>());
                break;
        }
        ShowEvent(ran);
    }
    int statmod,money;
    ItemSystem.Items x;
    string npcname;
    void ShowEvent(int e)
    {
        switch (e)
        {
            case 0://money
                TextResults.SetActive(true);
                GoldGained.gameObject.SetActive(true);
                TextTitle.text = "Oro adquirido";
                GoldGained.text = money + " Oro";
                break;
            case 1://hp
                TextResults.SetActive(true);
                Arrow.SetActive(true);
                StatsOld.gameObject.SetActive(true);
                StatsNew.gameObject.SetActive(true);
                TextTitle.text = "Vida ha sido mmodificada";
                StatsOld.text = statmod + " Hp";
                StatsNew.text = pc._hp+" Hp";
                break;
            case 2://random item
                ItemResults.SetActive(true);
                var item = Instantiate(PreviewPrefab, SlotPreview.transform);
                item.GetComponent<LootPreview>().itemdata = x;
                item.name = "NewItem";
                break;
            case 3:
                TextResults.SetActive(true);
                GoldGained.gameObject.SetActive(true);
                TextTitle.text = "Personaje se ha unido a la party";
                GoldGained.text = npcname;
                break;
        }
    }
    public void GoBack()
    {
        try
        {
            Destroy(GameObject.Find("NewItem"));
        }
        catch { }
        Arrow.SetActive(false);
        GoldGained.gameObject.SetActive(false);
        StatsNew.gameObject.SetActive(false);
        StatsOld.gameObject.SetActive(false);
        ItemResults.SetActive(false);
        TextResults.SetActive(false);
        gameObject.SetActive(false);

    }
}
