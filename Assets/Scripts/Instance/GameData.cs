using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    [SerializeField] GameObject[] player_prefabs;
    [SerializeField] Image[] classButtonsSprites;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] Sprite[] BSprite; 
    GameObject player;
    private void Start()
    {
        for(int i=0; i < player_prefabs.Length; i++)
        {
            classButtonsSprites[i].sprite = player_prefabs[i].GetComponent<Player_Controller>()._playerData.sprite;
        }
        SelectClass(0);
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
        var x = Instantiate(player);
        x.name = "Player";
    }
}
