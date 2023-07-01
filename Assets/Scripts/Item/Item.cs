using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData _data;
    public int _level;
    public Weapon _weapon;
    public Gear _gear;

    Image _icon;
    Text _textLevel;
    Text _textName;
    Text _textDesc;

    private void Awake()
    {
        _icon = GetComponentsInChildren<Image>()[1];
        _icon.sprite = _data._itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        _textLevel = texts[0];
        _textName = texts[1];
        _textDesc = texts[2];
        _textName.text = _data._itemName;
    }

    private void OnEnable()
    {
        _textLevel.text = "Lv." + _level;

        switch(_data._eitemType)
        {
            case ItemData.EItemType.Melee:
            case ItemData.EItemType.Range:
                _textDesc.text = string.Format(_data._itemDesc, _data._damages[_level] * 100, _data._counts[_level]);   // 데미지 표시를 위해 100 곱해줌
                break;

            case ItemData.EItemType.Glove:
            case ItemData.EItemType.Shoe:
                _textDesc.text = string.Format(_data._itemDesc, _data._damages[_level] * 100);
                break;

            default:
                _textDesc.text = string.Format(_data._itemDesc);
                break;
        }

    }

    public void OnClick()
    {
        switch (_data._eitemType)
        {
            case ItemData.EItemType.Melee:
            case ItemData.EItemType.Range:

                if(_level == 0)
                { 
                    GameObject newWeapon = new GameObject();
                    _weapon = newWeapon.AddComponent<Weapon>();
                    _weapon.Init(_data);
                }

                else
                {
                    float nextDamage = _data._baseDamage;
                    int nextCount = 0;

                    nextDamage += _data._baseDamage * _data._damages[_level];
                    nextCount += _data._counts[_level];

                    _weapon.LevelUp(nextDamage, nextCount);
                }
                _level++;
                break;

            case ItemData.EItemType.Glove:
            case ItemData.EItemType.Shoe:

                if (_level == 0)
                {
                    GameObject newGear = new GameObject();
                    _gear = newGear.AddComponent<Gear>();
                    _gear.Init(_data);
                }

                else
                {
                    float nextRate = _data._damages[_level];
                    _gear.Level_UP(nextRate);
                }
                _level++;
                break;

            case ItemData.EItemType.Heal:
                GameManager.Instance._curHp = GameManager.Instance._maxHP;
                break;
        }

        if (_level == _data._damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
