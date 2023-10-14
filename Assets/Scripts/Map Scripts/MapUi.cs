using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUi : MonoBehaviour
{
    [SerializeField] GameObject Node_Arrow;
    [SerializeField] GameObject[] Ui_info;
    [SerializeField] GameObject[] InfoInventory;
    [SerializeField] GameObject[] SlotsParty;
    [SerializeField] GameObject[] EventUI;
    Player_Controller pc;

    private void Start()
    {
        pc = FindObjectOfType<Player_Controller>();
        UpdateParty();
    }
    void UpdateParty()
    {
        int count = 1;
        //SlotsParty[0].GetComponent<Image>().sprite = 
        InfoInventory[0].GetComponent<TextMeshProUGUI>().text = pc.money.ToString();
        InfoInventory[1].GetComponent<TextMeshProUGUI>().text = pc._hp.ToString();
        foreach (NPC_Controller npc in pc.party)
        {
            
            SlotsParty[count].GetComponent<Image>().sprite = npc._npcSprite;
            SlotsParty[count].transform.GetChild(0).GetComponent<NPCUi>().controller = npc;
            SlotsParty[count].SetActive(true);
            count++;
        }
    } 
    public void UpdateInfoInventory()
    {
        Ui_info[0].SetActive(true);
        Ui_info[1].GetComponent<TextMeshProUGUI>().text =
            "Functions: " + pc._inventory.functions.Count +
            "\nVariables: " + pc._inventory.variables.Count +
            "\nConsumibles: " + pc._inventory.consumibles.Count;
    }
    public void UpdateArrow(Vector3 nodePos)
    {
        Node_Arrow.SetActive(true);
        nodePos.y += 0.85f;
        Node_Arrow.transform.position = nodePos;
    }
    public void ShowEventUi(int ui)
    {
        switch (ui)
        {
            case 0://heal
                EventUI[0].SetActive(true);
                break;
            case 1://chest a.k.a loot
                EventUI[1].SetActive(true);
                break;
            case 2://blessing wip
                EventUI[2].SetActive(true);
                break;
        }
    }
}
