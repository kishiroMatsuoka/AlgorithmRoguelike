using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameData : MonoBehaviour
{
    [SerializeField] GameObject[] player_prefabs;
    [SerializeField] Image[] classButtonsSprites;
    [SerializeField] TextMeshProUGUI Description, RutInput;
    [SerializeField] Sprite[] BSprite;
    [SerializeField] Button BeginButton;
    GameObject player;
    
    private void Start()
    {
        for(int i=0; i < player_prefabs.Length; i++)
        {
            classButtonsSprites[i].sprite = player_prefabs[i].GetComponent<Player_Controller>()._playerData.sprite;
        }
        SelectClass(0);
    }
    private void Update()
    {
        if (RutInput.gameObject.activeSelf)
        {
            if(RutInput.text.Length < 8)
            {
                BeginButton.enabled = false;
            }
            else
            {
                BeginButton.enabled = true;
            }
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeButtonSprites(Transform button)
    {
        button.GetComponent<Image>().sprite = BSprite[1];
        foreach (Transform t in button.parent)
        {
            if(t != button)
            {
                t.GetComponent<Image>().sprite = BSprite[0];
            }
        }
    }
    public void ToggleActive(GameObject target)
    {
        if(target.activeSelf == true)
        {
            target.SetActive(false);
        }
        else
        {
            target.SetActive(true);
        }
    }
    public void Exit( )
    {
        Application.Quit();
    }
    public void SelectClass(int pc_class)
    {
        player = player_prefabs[pc_class];
        Description.text = player.GetComponent<Player_Controller>().GetDescription();
    }
    public void CreatePlayer()
    {
        string playerrut = RutInput.text;
        var x = Instantiate(player);
        x.name = "Player";
        x.GetComponent<Player_Controller>().PlayerRut = playerrut;
        StartCoroutine(MarkStart(playerrut));
    }
    private string URL =
        "https://docs.google.com/forms/u/0/d/e/1FAIpQLSffsKRmzTO-xS7cfq9dw66gCHljU-0mgWbbEQjMofKPAoK4_g/formResponse";
    IEnumerator MarkStart(string rut)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.432945298", rut);//rut
        form.AddField("entry.313003536", "0");//action
        form.AddField("entry.1871614421", "Inicia Partida");//description
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }
}
