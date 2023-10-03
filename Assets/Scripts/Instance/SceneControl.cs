using UnityEngine;
using UnityEngine.SceneManagement;
using Map;

public class SceneControl : MonoBehaviour
{
    public GameObject npc_pre;
    [SerializeField] GameObject Store, Map, Combat;
    public int MaxLvlZone;
    public Node CurrentNode;
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
