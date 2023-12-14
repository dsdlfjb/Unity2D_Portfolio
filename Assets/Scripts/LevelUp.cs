using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject _levelUpPanel;

    Item[] _items;

    private void Awake()
    {
        _items = GetComponentsInChildren<Item>(true);
    }

    public void Show_LevelUp() 
    {
        Next();
        _levelUpPanel.SetActive(true);
        GameManager.Instance.Stop();
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.LevelUp);
        //AudioManager.Instance.EffectBgm(true);
    }

    public void Hide_LevelUp() 
    { 
        _levelUpPanel.SetActive(false);
        GameManager.Instance.Resume();
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
        //AudioManager.Instance.EffectBgm(false);
    }

    public void Select(int index) { _items[index].OnClick(); } 

    void Next()
    {
        // 1. 모든 아이템 비활성화
        foreach (Item item in _items)
            item.gameObject.SetActive(false);

        // 2. 그 중에서 랜덤 3개 아이템 활성화
        int[] rnd = new int[3];

        while (true)
        {
            rnd[0] = Random.Range(0, _items.Length);
            rnd[1] = Random.Range(0, _items.Length);
            rnd[2] = Random.Range(0, _items.Length);

            if (rnd[0] != rnd[1] && rnd[1] != rnd[2] && rnd[0] != rnd[2])
                break;
        }

        for (int index = 0; index < rnd.Length; index++)
        {
            Item rndItem = _items[rnd[index]];

            // 3. 만렙 아이템의 경우는 소비아이템으로 대체
            if (rndItem._level == rndItem._data._damages.Length)
            {
                _items[4].gameObject.SetActive(true);
            }

            else
            {
                rndItem.gameObject.SetActive(true);
            }
        }
    }
}
