using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    //System Access
    public PoolTable Store, Combat, General;
    [HideInInspector] public List<NPC_Controller> party = new List<NPC_Controller>();
    public Inventory _inventory;
    public int _hp, _maxhp, _def, _maxcost, money = 100;
    public int Score = 0;
    //internal Access
    [SerializeField] PlayerData _playerData;
    bool EducationMode = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        

        _hp = _playerData.hp;
        _maxhp = _hp;
        _def = _playerData.def;
        _maxcost = _playerData.maxcost;
        _inventory = Instantiate(_playerData.inventory);
        print("Player data Loaded");
    }
    public void CheckStatus()
    {
        if (_hp > _maxhp) { _hp = _maxhp; }
        else if (_hp <= 0) { //dedad
                             }
    }
    public void PartyAttack(List<Enemy> en)
    {
        foreach(NPC_Controller m in party)
        {
            if (m._isAlive)
            {
                en[Random.Range(0, en.Count)].ChangeHp(m._dmg, m._magic);
            }
        }
    }
    public void PartyRecieveDmg(int dmg, bool aoe)
    {
        if (party.Count > 0) {
            if (aoe)
            {
                HpModifier(dmg, false);
                foreach(NPC_Controller n in party){n.HpModifier(dmg, false);}
            }
            else
            {
                if(Random.Range(0,2) > 0){party[Random.Range(0, party.Count)].HpModifier(dmg, false);}
                else{HpModifier(dmg, false);}
                
            }
        }
        else { HpModifier(dmg, false); }
    }
    //data modifiers
    
    public void HpModifier(int effect, bool positive)
    {
        if (positive)
        {
            _hp += (int)System.Math.Round((_maxhp * effect / 100f));
            if (_hp > _maxhp) { _hp = _maxhp; }
        }
        else
        {
            int tempdef = _def + FindObjectOfType<Combat_controller>().e_def;
            _hp -= (effect - tempdef);
            if (_hp <= 0)
            {
                //player dead
            }
        }
    }
    public void E_Mode(){EducationMode = true;}
    public bool EModeS(){return EducationMode;}
    public string GetDescription(){return _playerData.description;}
}
