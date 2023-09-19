// 라이트닝 스킬아이템 구현
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningItem : MonoBehaviour, IFieldDropItem
{
    public void UseItem()
    {
        // 번개 아이템을 먹었을 때의 동작을 구현합니다.
        // 주변 적들에게 번개 효과를 적용하고 적들을 처리합니다.
        // 예: 번개 효과 적용 및 적 처리 로직을 구현하세요.
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject, 5f);
    }
}
