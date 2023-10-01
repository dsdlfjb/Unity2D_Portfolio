using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enum;

public class ResourceManager : MonoBehaviour
{
    //Dictionary로 키값에 해당하는 스프라이트를 불러옴
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
        // _dicSprite가 해당하는 key를 가지고 있는지 확인
        if (_dicSprites.ContainsKey(key))
            // 해당 키를 이미 가지고 있다면 리턴
            return _dicSprites[key];

        // 가지고있지 않다면 null을 리턴
        else return null;
    }
}
