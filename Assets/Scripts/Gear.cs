using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.EItemType _eType;
    public float _rate;

    public void Init(ItemData data)
    {
        name = "Gear " + data._itemId;
        transform.parent = GameManager.Instance._player.transform;
        transform.localPosition = Vector3.zero;

        _eType = data._eitemType;
        _rate = data._damages[0];
        ApplyGear();
    }

    public void Level_UP(float rate)
    {
        this._rate = rate;
        ApplyGear();
    }


    void ApplyGear()
    {
        switch (_eType)
        {
            case ItemData.EItemType.Glove:
                Rate_UP();
                break;

            case ItemData.EItemType.Shoe:
                Speed_UP();
                break;
        }
    }

    void Rate_UP()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch(weapon._id)
            {
                case 0:
                    weapon._speed = 150 + (150 * _rate);
                    break;

                case 1:
                    weapon._speed = 0.5f * (1f - _rate);
                    break;
            }
        }
    }

    void Speed_UP()
    {
        float speed = 3;
        GameManager.Instance._player._moveSpeed = speed + speed * _rate;
    }
}
