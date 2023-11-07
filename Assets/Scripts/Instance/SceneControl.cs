using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Map;
using TMPro;
using System.Collections;

public class SceneControl : MonoBehaviour
{
    [SerializeField] List<Transform> CombatTransforms;
    [SerializeField] GameObject Store, Map, Combat, PauseMenu, EndScreen, LevelFinish, CombatEnd;
    [SerializeField] TextMeshProUGUI Score,FinalScore;
    public GameObject[] npc_pre;
    public bool BossDead=false;
    public int MaxLvlZone;
    public Node CurrentNode;
    public Transform CNodeObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
    }
    void CheckEndLevel()
    {
        if (BossDead)
        {
            LevelFinish.SetActive(true);
            var pc = FindObjectOfType<Player_Controller>();
            StartCoroutine(MarkGameEnd(pc));
            FinalScore.text = pc.Score.ToString();

        }
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(MarkGameEnd(FindObjectOfType<Player_Controller>()));
        Destroy(GameObject.Find("Player"));
        SceneManager.LoadScene(0);
    }
    public void GameOver(int score)
    {
        ExitCombat();
        StartCoroutine(MarkGameDead(FindObjectOfType<Player_Controller>()));
        EndScreen.SetActive(true);
        Score.text = score.ToString();
    }
    public void AppQuit()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
    public void Lifo()
    {
        CurrentNode.Node_Enemies.Reverse();
    }
    public void EnterStore()
    {
        Map.SetActive(false);
        Store.SetActive(true);
    }
    public void ExitStore(int c, int v)
    {
        StartCoroutine(MarkStore(FindObjectOfType<Player_Controller>(),c,v));
        Map.SetActive(true);
        Store.SetActive(false);
    }
    public void EnterCombat()
    {
        Combat.SetActive(true);
        Map.SetActive(false);
        
    }
    public void ExitCombat()
    {
        Combat.SetActive(false);
        CombatEnd.SetActive(false);
        foreach(Transform ct in CombatTransforms)
        {
            foreach(Transform tc in ct)
            {
                Destroy(tc.gameObject);
            }
        }
        Map.SetActive(true);
        FindObjectOfType<Camera_zoomnode>().Change_Camera(0);
        CheckEndLevel();
    }
    private string URL =
    "https://docs.google.com/forms/u/0/d/e/1FAIpQLSffsKRmzTO-xS7cfq9dw66gCHljU-0mgWbbEQjMofKPAoK4_g/formResponse";
    IEnumerator MarkGameEnd(Player_Controller p)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.432945298", p.PlayerRut);//rut
        form.AddField("entry.313003536", "4");//action
        form.AddField("entry.1871614421", "FinalScore: "+p.Score);//description
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }
    IEnumerator MarkGameDead(Player_Controller p)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.432945298", p.PlayerRut);//rut
        form.AddField("entry.313003536", "3");//action
        form.AddField("entry.1871614421", "FinalScore: " + p.Score+", Muerte en: "+CurrentNode.name);//description
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }
    IEnumerator MarkStore(Player_Controller p, int compras, int ventas)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.432945298", p.PlayerRut);//rut
        form.AddField("entry.313003536", "2");//action
        form.AddField("entry.1871614421", "Compras: " + compras +", Ventas: "+ventas) ;//description
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }
}
