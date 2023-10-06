using UnityEngine;
using System.Collections.Generic;
using Enum;

public class ResourceManager : MonoBehaviour
{
    // Dictionary�� Ű���� �ش��ϴ� ��������Ʈ�� �ҷ���
    private Dictionary<ESpriteKey, Sprite> _dicSprites;

    private void Awake()
    {
        _dicSprites = new Dictionary<ESpriteKey, Sprite>();
        this.LoadSpriteFromResources();
    }

    private void LoadSpriteFromResources()
    {
        var sprites = Resources.LoadAll<Sprite>("Swords");

        for (int i = 0; i < sprites.Length; i++)
            _dicSprites.Add((ESpriteKey)i, sprites[i]);
    }

    // �ܺο��� ȣ���� �� ������ �� �Լ��� ���ؼ�
    // ��� Ŭ�������� ������ ����
    // ���� ResourceManager �ȿ����� ���ҽ��� �ҷ��� �� �־� �����ϱ� ����
    public Sprite GetSprite(ESpriteKey key)
    {
        // _dicSprite�� �ش��ϴ� key�� �������ִ��� Ȯ��
        if (_dicSprites.ContainsKey(key))
            return _dicSprites[key];

        else
            return null;
    }
}
