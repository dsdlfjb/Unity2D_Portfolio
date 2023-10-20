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

        AudioManager.Instance.PlaySFX(AudioManager.ESfx.LevelUp);
        AudioManager.Instance.EffectBGM(true);
    }

    public void Hide_LevelUp() 
    { 
        _levelUpPanel.SetActive(false);
        GameManager.Instance.Resume();

        AudioManager.Instance.PlaySFX(AudioManager.ESfx.Select);
        AudioManager.Instance.EffectBGM(false);
    }

    public void Select(int index) { _items[index].OnClick(); } 

    void Next()
    {
        // 1. ��� ������ ��Ȱ��ȭ
        foreach (Item item in _items)
            item.gameObject.SetActive(false);

        // 2. �� �߿��� ���� 3�� ������ Ȱ��ȭ
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

            // 3. ���� �������� ���� �Һ���������� ��ü
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
