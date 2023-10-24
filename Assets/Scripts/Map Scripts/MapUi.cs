using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUi : MonoBehaviour
{
    [SerializeField] Image playerSprite;
    [SerializeField] GameObject Node_Arrow;
    [SerializeField] GameObject[] Ui_info;
    [SerializeField] GameObject[] InfoInventory;
    [SerializeField] GameObject[] SlotsParty;
    Player_Controller pc;

    private void Start()
    {
        pc = FindObjectOfType<Player_Controller>();
        playerSprite.sprite = pc.SP;
        UpdateParty();
    }
    private void Update()
    {
        InfoInventory[0].GetComponent<TextMeshProUGUI>().text = pc.money.ToString();
        InfoInventory[1].GetComponent<TextMeshProUGUI>().text = pc._hp.ToString();
    }
    public void UpdateParty()
    {
        int count = 0;
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
}
