using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject _completePurchasePopup;
    [SerializeField] private GameObject _shortagePopup;
    [SerializeField] private GameObject _soldoutPanel;

    // Spcae 어트리뷰트는 인스펙터에서 변수끼리의 간격을 줌
    [Space(20)] 
    [SerializeField] private Image _swordIcon;
    [SerializeField] private Text _priceText;
    [SerializeField] private Text _swordName;

    private IEnumerator _ePopupDirect;
    private int _currentSwordIndex;

    private void Awake()
    {
        _currentSwordIndex = 0;
        UpdateUI();
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // 코루틴이 실행중인 도중에 게임오브젝트가 파괴되면 에러가 발생하기때문에,
        // 모든 코루틴을 중지하도록 하는게 좋음
        if (_ePopupDirect != null)
            StopCoroutine(_ePopupDirect);
        _ePopupDirect = null;
    }

    public void ClickPrevious()
    {
        // 계속해서 다음것을 볼 수 있도록 루프를 돌림
        _currentSwordIndex--;

        if (_currentSwordIndex < 0)
            // 인덱스이기 때문에 1을 뺌
            _currentSwordIndex = Define.Constant.SWORD_NUMBER - 1;

        UpdateUI();
    }

    public void ClickNext()
    {
        _currentSwordIndex++;
        if (_currentSwordIndex >= Define.Constant.SWORD_NUMBER)
            _currentSwordIndex = 0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _swordIcon.sprite = Managers.Resouce.GetSprite(Enum.ESpriteKey.Sword_1 + _currentSwordIndex);

        // SetNativeSize는 말 그대로 네이티브 사이즈로 세팅하도록 함
        // 인스펙터에서 Image 컴포넌트 아래에 Set Native Size 버튼과 동일한 기능을 함
        // Native 사이즈는 텍스쳐 클릭하면 인스펙터에서 보이는 텍스쳐 세팅에서 'Pizels Per Unit'에 영향을 받음
        _swordIcon.SetNativeSize();

        _priceText.text = Managers.Save.saveData.GetSwordData(_currentSwordIndex)._price.ToString();

        // 현재 무기를 구매했으면 매진처리
        _soldoutPanel.SetActive(Managers.Save.saveData.GetSwordData(_currentSwordIndex)._isPurchased);

        // 현재 검 인덱스에 따라 이름을 변경
        _swordName.text = Define.Constant.SWORD_NAMES[_currentSwordIndex];
    }

    public void ClickPurchase()
    {
        var swordData = Managers.Save.saveData.GetSwordData(_currentSwordIndex);

        // 구입한적 없다면
        if (swordData._isPurchased == false)
        {
            if (swordData._price <= DataManager.Instance.nowPlayer.coin)
            {
                DataManager.Instance.nowPlayer.coin -= swordData._price;

                swordData._isPurchased = true;

                Managers.Event.InvokeEvent(Enum.EEventKey.OnPurcharseSword, _currentSwordIndex);
                Managers.Save.SaveData();

                // 코인이 있는 데이터를 저장.
                // 세이브 데이터는 한곳에서 관리하는게 좋음
                DataManager.Instance.Save();

                if (_ePopupDirect != null)
                    StopCoroutine(_ePopupDirect);
                _ePopupDirect = this.coroShowPopup(0);
                StartCoroutine(_ePopupDirect);

                UpdateUI();
            }
            else
            {
                if (_ePopupDirect != null)
                    StopCoroutine(_ePopupDirect);
                _ePopupDirect = this.coroShowPopup(1);
                StartCoroutine(_ePopupDirect);
            }
        }
    }

    // 코루틴을 사용해서 '코인이 부족합니다' 또는 '구매 완료' 팝업을 띄웠다가 사라지게 하도록 함
    private System.Collections.IEnumerator coroShowPopup(int index)
    {
        if (index == 0)
            _completePurchasePopup.SetActive(true);
        else if (index == 1)
            _shortagePopup.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (index == 0)
            _completePurchasePopup.SetActive(false);
        else if (index == 1)
            _shortagePopup.SetActive(false);
    }
}
