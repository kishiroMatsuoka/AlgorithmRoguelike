using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Camera_zoomnode : MonoBehaviour
{
    [SerializeField] GameObject PreviewPrefab;
    [SerializeField] List<GameObject> NodeUI;
    //Chest
    [SerializeField] GameObject[] ChestNodeObjects;
    //Upgrade
    //[SerializeField] GameObject[] UpgradeNodeObjects;
    [SerializeField] GameObject OptionsPanel, ResultsUpgrade, SlotOld, SlotNew;
    [SerializeField] TextMeshProUGUI StatsOld, StatsNew;
    int stat;
    //RandomEvent
    [SerializeField] GameObject[] RandomEventObjects;
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
        pc = FindObjectOfType<Player_Controller>();
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
                pc._hp = pc._maxhp;
                pc.CheckStatus();
                break;
            case 1:
                pc._hp += (int)(pc._maxhp*0.3f);
                pc.CheckStatus();
                foreach(NPC_Controller n in pc.party)
                {
                    n._health += (int)(n._maxHealth* 0.3f);
                    n.CheckStatus();
                }
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
                RandomEventObjects[0].SetActive(true);//resultscreen
                RandomEventObjects[2].SetActive(true);//title
                RandomEventObjects[2].GetComponent<TextMeshProUGUI>().text = "Oro adquirido";//title
                RandomEventObjects[6].SetActive(true);//goldsprite
                RandomEventObjects[6].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = money + " Oro";
                
                break;
            case 1://hp\
                RandomEventObjects[0].SetActive(true);//resultscreen
                RandomEventObjects[2].SetActive(true);//title
                RandomEventObjects[2].GetComponent<TextMeshProUGUI>().text = "Vida ha sido mmodificada";//title
                RandomEventObjects[3].SetActive(true);//indicador
                RandomEventObjects[4].SetActive(true);//oldtxt
                RandomEventObjects[4].GetComponent<TextMeshProUGUI>().text = statmod + " Hp";//infotext
                RandomEventObjects[5].SetActive(true);//newtxt
                RandomEventObjects[5].GetComponent<TextMeshProUGUI>().text = pc._hp + " Hp";//infotext
                break;
            case 2://random item
                RandomEventObjects[1].SetActive(true);//resultscreen
                var item = Instantiate(PreviewPrefab, RandomEventObjects[1].transform.GetChild(0).transform);
                item.GetComponent<LootPreview>().SetName(x);
                item.name = "NewItem";
                break;
            case 3:
                RandomEventObjects[0].SetActive(true);//resultscreen
                RandomEventObjects[2].SetActive(true);//title
                RandomEventObjects[2].GetComponent<TextMeshProUGUI>().text = "Personaje se ha unido a la party";//title
                RandomEventObjects[9].SetActive(true);//npc sprite+name
                RandomEventObjects[9].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = npcname;//text info
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
        foreach(GameObject g in RandomEventObjects)
        {
            g.SetActive(false);
        }

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
                old.GetComponent<LootPreview>().SetName(x);
                old.name = "OldItem";
                var prev = Instantiate(PreviewPrefab, SlotNew.transform);
                prev.GetComponent<LootPreview>().SetName(function);
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
                oldvar.GetComponent<LootPreview>().SetName (y);
                oldvar.name = "OldItem";
                var prevvar = Instantiate(PreviewPrefab, SlotNew.transform);
                prevvar.GetComponent<LootPreview>().SetName (variable);
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
            ChestNodeObjects[0].SetActive(true);
        }
        else
        {
            ChestNodeObjects[1].SetActive(true);
            var temp = FindObjectOfType<Player_Controller>();
            var item = temp.General.GetRandomItem(2);
            temp._inventory.AddToInventory(item);
            var prev = Instantiate(PreviewPrefab, ChestNodeObjects[1].transform.GetChild(0).transform);
            prev.GetComponent<LootPreview>().SetName(item);
            prev.name = "rewardPreviewchest";
        }
    }
    public void ChestResetUi()
    {
        GameObject x = GameObject.Find("rewardPreviewchest");
        if (x != null) { Destroy(x); }
        foreach(GameObject g in ChestNodeObjects) { g.SetActive(false); }
    }

}
