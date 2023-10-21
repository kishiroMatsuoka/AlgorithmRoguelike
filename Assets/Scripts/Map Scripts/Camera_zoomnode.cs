using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Camera_zoomnode : MonoBehaviour
{
    [SerializeField] GameObject PreviewPrefab;
    [SerializeField] List<GameObject> NodeUI;
    //Chest
    [SerializeField] GameObject RewardChest, PuzleChest;
    [SerializeField] Transform SlotChest;
    
    //Upgrade
    [SerializeField] GameObject OptionsPanel, ResultsUpgrade, SlotOld, SlotNew;
    [SerializeField] TextMeshProUGUI StatsOld, StatsNew;
    int stat;
    //RandomEvent
    [SerializeField] GameObject SlotPreview;
    [SerializeField] GameObject TextResults, ItemResults, Arrow;
    [SerializeField] TextMeshProUGUI TextTitle, RStatsNew, RStatsOld, GoldGained;
    int statmod, money;
    ItemSystem.Items x;
    string npcname;
    //
    Camera Cam_main;
    Camera Cam_second;
    Player_Controller pc;
    private void Start()
    {
        Cam_main = Camera.main;
        Cam_second = GetComponent<Camera>();
    }
    public void Update_Node(Vector3 node_coor)
    {
        Vector3 camera_coor = transform.position;
        node_coor.z = camera_coor.z;
        transform.position = node_coor;
    }
    public void Change_Camera(int ui)
    {
        if (Cam_main.enabled)
        {
            Cam_main.enabled = false;
            Cam_second.enabled = true;
            NodeUI[ui].SetActive(true);
            //0:combat
            //1:store
            //2:random
            //3:Upgrade
            //4:Heal
            //5:Chest
            //6:Blessing
            switch (ui)
            {
                case 2://random
                    GenerateRandomEvent();
                    break;
                case 5://chest
                    ChestResult(2);
                    break;
                default:
                    //nothing
                    break;
            }
        }
        else
        {
            Cam_main.enabled = true;
            Cam_second.enabled = false;
            foreach(GameObject x in NodeUI) { x.SetActive(false); }
        }
    }
    //Heal Functions
    public void HealOption(int option)
    {
        switch (option)
        {
            case 0:

                break;
            case 1:

                break;
        }
    }
    //Random Event
    public void GenerateRandomEvent()
    {
        int ran = Random.Range(0, 4);
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
                if (rf > 95f)
                {
                    pc._hp = pc._maxhp;
                    foreach (NPC_Controller nc in pc.party)
                    {
                        nc._health = nc._maxHealth;
                    }
                }
                else if (rf > 80f)
                {
                    pc._hp += pc._maxhp / 2;
                    pc.CheckStatus();
                    foreach (NPC_Controller nc in pc.party)
                    {
                        nc._health += nc._maxHealth / 2;
                        nc.CheckStatus();
                    }
                }
                else if (rf > 30f)
                {
                    pc._hp += pc._maxhp / 4;
                    pc.CheckStatus();
                    foreach (NPC_Controller nc in pc.party)
                    {
                        nc._health += nc._maxHealth / 4;
                        nc.CheckStatus();
                    }
                }
                else if (rf > 5f)
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
                RStatsOld.gameObject.SetActive(true);
                RStatsNew.gameObject.SetActive(true);
                TextTitle.text = "Vida ha sido mmodificada";
                RStatsOld.text = statmod + " Hp";
                RStatsNew.text = pc._hp + " Hp";
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
                FindObjectOfType<MapUi>().UpdateParty();
                break;
        }
    }
    public void RandomGoBack()
    {
        try
        {
            Destroy(GameObject.Find("NewItem"));
        }
        catch { }
        Arrow.SetActive(false);
        GoldGained.gameObject.SetActive(false);
        RStatsNew.gameObject.SetActive(false);
        RStatsOld.gameObject.SetActive(false);
        ItemResults.SetActive(false);
        TextResults.SetActive(false);
        gameObject.SetActive(false);

    }
    //Upgrade Functions
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
                    if (x != null) { break; }
                    else { x = pc._inventory.GetRandomFunction(Random.Range(0, 10)); }
                }
                var function = pc.General.GetRandomFunction(x.ItemRarity + 1);
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
        ResultsUpgrade.SetActive(true);
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
    public void UpgradeGoBack()
    {
        try
        {
            Destroy(GameObject.Find("NewItem"));
            Destroy(GameObject.Find("OldItem"));
        }
        catch { }
        ResultsUpgrade.SetActive(false);
        OptionsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    //ChestNode Functions
    public void ChestResult(int x)
    {
        if (x == 1)
        {
            PuzleChest.SetActive(true);
        }
        else
        {
            RewardChest.SetActive(true);
            var temp = FindObjectOfType<Player_Controller>();
            var item = temp.General.GetRandomItem(2);
            temp._inventory.AddToInventory(item);
            var prev = Instantiate(PreviewPrefab, SlotChest);
            prev.GetComponent<LootPreview>().itemdata = item;
            prev.name = "rewardPreview";
        }
    }
    public void ChestResetUi()
    {
        GameObject x = GameObject.Find("rewardPreview");
        if (x != null) { Destroy(x); }
        RewardChest.SetActive(false);
        PuzleChest.SetActive(false);
        gameObject.SetActive(false);
    }

}
