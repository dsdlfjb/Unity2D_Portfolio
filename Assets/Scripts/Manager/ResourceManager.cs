using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enum;

public class ResourceManager : MonoBehaviour
{
    //Dictionary�� Ű���� �ش��ϴ� ��������Ʈ�� �ҷ���
    private Dictionary<ESpriteKey, Sprite> _dicSprites;

    private void Awake()
    {
        _dicSprites = new Dictionary<ESpriteKey, Sprite>();
        this.LoadSpriteFromResources();
    }

    void LoadSpriteFromResources()
    {
        var sprites = Resources.LoadAll<Sprite>("Swords");

        for (int i = 0; i < sprites.Length; i++)
            _dicSprites.Add((ESpriteKey)i, sprites[i]);
    }

    public Sprite GetSprite(ESpriteKey key)
    {
        // _dicSprite�� �ش��ϴ� key�� ������ �ִ��� Ȯ��
        if (_dicSprites.ContainsKey(key))
            // �ش� Ű�� �̹� ������ �ִٸ� ����
            return _dicSprites[key];

        // ���������� �ʴٸ� null�� ����
        else return null;
    }
}