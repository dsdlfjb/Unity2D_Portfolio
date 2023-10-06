using UnityEngine;
using System.Collections.Generic;
using Enum;

public class ResourceManager : MonoBehaviour
{
    // Dictionary로 키값에 해당하는 스프라이트를 불러옴
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

    // 외부에서 호출을 할 때에는 이 함수를 통해서
    // 모든 클래스에서 접근이 가능
    // 따라서 ResourceManager 안에서만 리소스를 불러올 수 있어 관리하기 용이
    public Sprite GetSprite(ESpriteKey key)
    {
        // _dicSprite가 해당하는 key를 가지고있는지 확인
        if (_dicSprites.ContainsKey(key))
            return _dicSprites[key];

        else
            return null;
    }
}
