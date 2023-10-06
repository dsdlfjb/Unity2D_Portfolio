using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private Transform _contentTransform;

    private void Awake()
    {
        // 정의해뒀던 Define 네임스페이스 안에 있는 static class Constant에서
        // 칼의 갯수가 몇개인지 받아옴
        // 이렇게하면 추후 칼이 늘어나거나 줄어들어도 'Define.Constant.SWORD_NUMBER'만 수정하면 되기 때문에 수정이 용이

        for (int i = 0; i < Define.Constant.SWORD_NUMBER; i++)
        {
            // Instantiate라는 함수는 original이라는 파라미터에 들어간 게임오브젝트를 생성하고 GameObject를 리턴시킴
            // 이렇게하면 런타임중에(플레이 도중에) 스크립트를 사용해서 게임오브젝트를 생성
            // GetComponent로 InventorySlot을 찾고 SetData를 호출
            Instantiate(_inventorySlotPrefab,_contentTransform).GetComponent<InventorySlot>().SetData(i);
        }

        this.gameObject.SetActive(false);
    }
}
