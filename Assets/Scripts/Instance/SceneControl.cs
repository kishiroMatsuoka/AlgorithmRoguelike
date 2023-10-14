using UnityEngine;
using UnityEngine.SceneManagement;
using Map;

public class SceneControl : MonoBehaviour
{
    [SerializeField] GameObject Store, Map, Combat, PauseMenu;

    public GameObject[] npc_pre;
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
    public void MainMenu()
    {
        Time.timeScale = 1;
        Destroy(GameObject.Find("Player"));
        SceneManager.LoadScene(0);
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
    public void ExitStore()
    {
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
        Map.SetActive(true);
    }
}
