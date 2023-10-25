using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NPCUi : MonoBehaviour
{
    [SerializeField] Image _greenbar, _redbar;
    [SerializeField] TextMeshProUGUI _hpcounter;
    public bool UsePercentage = false;
    public NPC_Controller controller;
    bool is_start = true;
    int currenthp, maxhp;
    void OnEnable()
    {
        currenthp = controller._health;
        maxhp = controller._maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller._health != currenthp)
        {
            UpdateHealth();
        }
    }
    public void UpdateHealth()
    {
        currenthp = controller._health;
        maxhp = controller._maxHealth;
        float per = (((currenthp * 100.0f) / maxhp) / 100.0f);
        _hpcounter.text = per+" %";
    }
    /*
    public void UpdateHealth()
    {
        currenthp = controller._health;
        maxhp = controller._maxHealth;
        float hppercentage = ((currenthp * 100) / maxhp) / 100.0f;
        if (UsePercentage)
        {
            _hpcounter.text = hppercentage + "%";
        }
        else
        {
            _hpcounter.text = currenthp + " Hp";
        }
        if (is_start)
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
    */
    public void test(int dmg)
    {
        controller._health -= dmg;
        UpdateHealth();
    }
    IEnumerator UpdateRedBar(float target)
    {
        yield return new WaitForSeconds(1f);
        while (_redbar.fillAmount > target)
        {
            _redbar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        if (_redbar.fillAmount < target)
        {
            _redbar.fillAmount = target;
        }
    }
}
