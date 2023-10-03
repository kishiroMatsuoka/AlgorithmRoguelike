using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public List<NPC_Controller> npcs = new List<NPC_Controller>();
    [SerializeField] List<NPCUi> npcuis = new List<NPCUi>();
    [SerializeField] List<Image> npcuimages = new List<Image>();
    [SerializeField] TextMeshProUGUI Defense, Money;
    Player_Controller pc;
    int staticDef;

    private void OnEnable()
    {
        pc = GameObject.Find("Player").GetComponent<Player_Controller>();
        staticDef = pc._def;
        Defense.text = pc._def.ToString();
        Money.text = pc.money.ToString();
        int counter = 0;
        foreach(NPC_Controller x in npcs)
        {
            npcuis[counter].transform.parent.gameObject.SetActive(true);
            npcuis[counter].controller = x;
            npcuimages[counter].sprite = x._npcSprite;
            counter++;
        }
    }
    
    public void UpdateDef(int def)
    {
        if(def < staticDef)
        {
            Defense.color = Color.green;
        }
        else if(def > staticDef)
        {
            Defense.color = Color.red;
        }
        else
        {
            Defense.color = Color.white;
        }
        Defense.text = def.ToString();
    }
}
