using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] NPC _npcData;
    public Sprite _npcSprite;
    public string _name;
    public int _dmg, _health, _maxHealth, _defense;
    public bool _magic, _isAlive=true;
    void Awake()
    {
        _npcSprite = _npcData.npc_sprite;
        _name = _npcData.npc_name;
        _dmg = _npcData.npc_dmg;
        _health = _npcData.npc_health;
        _maxHealth = _health;
        _defense = _npcData.npc_defense;
        _magic = _npcData.npc_usesMagic;
        DontDestroyOnLoad(gameObject);
    }
    public void HpModifier(int effect, bool positive)
    {
        if (positive)
        {
            _health += (int)System.Math.Round((_maxHealth * effect / 100f));
            if (_health > _maxHealth) { _health = _maxHealth; }
        }
        else
        {
            int tempdef = _defense + FindObjectOfType<Combat_controller>().e_def;
            _health -= (effect - tempdef);
            if (_health <= 0)
            {
                _isAlive = false;
            }
        }
    }

}
