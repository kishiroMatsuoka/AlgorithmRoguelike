using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDUI : MonoBehaviour
{
    [SerializeField] Image _greenbar, _redbar;
    [SerializeField] TextMeshProUGUI _hpcounter;
    Player_Controller _combatcontroller;
    bool is_start = true;
    int currenthp, maxhp;
    void OnEnable()
    {
        _combatcontroller = GameObject.Find("Player").GetComponent<Player_Controller>();
        currenthp = _combatcontroller._hp;
    }
    private void Update()
    {
        if(_combatcontroller._hp != currenthp)
        {
            UpdateHealth();
        }
    }
    public void UpdateHealth()
    {
        currenthp = _combatcontroller._hp;
        maxhp = _combatcontroller._maxhp;
        _hpcounter.text = currenthp+" Hp";
        float hppercentage = ((currenthp * 100)/maxhp)/100.0f;
        if(is_start)
        {
            _greenbar.fillAmount = hppercentage;
            _redbar.fillAmount = _greenbar.fillAmount;
            is_start = false;
        }
        else
        {
            _greenbar.fillAmount = hppercentage;
            StopAllCoroutines();
            StartCoroutine(UpdateRedBar(hppercentage));
        }
    }
    public void test(int dmg)
    {
        _combatcontroller._hp -= dmg;
        UpdateHealth();
    }
    IEnumerator UpdateRedBar(float target)
    {
        yield return new WaitForSeconds(1f);
        while(_redbar.fillAmount > target)
        {
            _redbar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        if(_redbar.fillAmount< target)
        {
            _redbar.fillAmount = target;
        }
    }
}
