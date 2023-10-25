using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] Image health;//, _redbar;
    [SerializeField] Enemy enemyref;
    [SerializeField] TextMeshProUGUI hpcounter;
    int maxhp;
    private void Awake()
    {
        maxhp = enemyref._enemyhp;
        hpcounter.text = maxhp + " Hp";
        //health.fillAmount = 1f;
    }
    private void Update()
    {
        if(enemyref._enemyhp != maxhp)
        {
            UpdateHealth();
        }
    }
    void UpdateHealth()
    {
        int current = enemyref._enemyhp;
        hpcounter.text = current + " Hp";
    }
    /*
    public void UpdateHealth()
    {
        int current = enemyref._enemyhp;
        hpcounter.text = current + " Hp";
        float hppercentage = ((current * 100) / maxhp) / 100.0f;
        health.fillAmount = hppercentage;
        StartCoroutine(UpdateRedBar(hppercentage));
    }
    
    IEnumerator UpdateRedBar(float target)
    {
        yield return new WaitForSeconds(1f);
        while (health.fillAmount >= target)
        {
            health.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        if (health.fillAmount < target)
        {
            health.fillAmount = target;
        }
    }
    */
}
