// 상점을 관리하는 스크립트
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject _completePurchasePopup;
    [SerializeField] private GameObject _shortagePopup;
    [SerializeField] private GameObject _soldoutPanel;

    // Space 어트리뷰트는 인스펙터에서 변수끼리의 간격을 주기위해 사용
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
        // 코루틴이 실행중인 도중에 게임오브젝트가 파괴되면 에러가 발생하기 때문에 모든 코루틴을 중지하도록 함
        if (_ePopupDirect != null)
            StopCoroutine(_ePopupDirect);
        _ePopupDirect = null;
    }

    public void PreviousButton()
    {
        _currentSwordIndex--;

        if (_currentSwordIndex < 0)
            _currentSwordIndex = Define.Constant.SWORD_NUMBER - 1;

        UpdateUI();
    }

    public void NextButton()
    {
        _currentSwordIndex++;

        if (_currentSwordIndex >= Define.Constant.SWORD_NUMBER)
            _currentSwordIndex = 0;

        UpdateUI();
    }

    void UpdateUI()
    {
        _swordIcon.sprite = Managers.Resource.GetSprite(Enum.ESpriteKey.Sword_1 + _currentSwordIndex);

        // 인스펙터에서 Image 컴포넌트 아래에 Set Native Size 버튼과 동일한 기능
        _swordIcon.SetNativeSize();

        _priceText.text = Managers.Save._saveData.GetSwordData(_currentSwordIndex)._price.ToString();

        _soldoutPanel.SetActive(Managers.Save._saveData.GetSwordData(_currentSwordIndex)._isPurchased);

        _swordName.text = Define.Constant.SWORD_NAMES[_currentSwordIndex];
    }

    public void PurchaseButton()
    {
        var swordData = Managers.Save._saveData.GetSwordData(_currentSwordIndex);

        if (swordData._isPurchased == false)
        {
            if (swordData._price <= DataManager.Instance.nowPlayer.coin)
            {
                DataManager.Instance.nowPlayer.coin -= swordData._price;

                swordData._isPurchased = true;
                Managers.Event.InvokeEvent(Enum.EEventKey.OnPurchaseSword, _currentSwordIndex);
                Managers.Save.Save();

                DataManager.Instance.Save();

                if (_ePopupDirect != null)
                    StopCoroutine(_ePopupDirect);
                _ePopupDirect = this.Coroutine_ShowPopup(0);
                StartCoroutine(_ePopupDirect);

                UpdateUI();
            }

            else
            {
                if (_ePopupDirect != null)
                    StopCoroutine(_ePopupDirect);
                _ePopupDirect = this.Coroutine_ShowPopup(1);
                StartCoroutine(_ePopupDirect);
            }
        }
    }

    // 코루틴을 사용해서 "코인이 부족합니다" 또는 "구매 완료" 팝업을 띄웠다가 사라지게 하도록 함
    private IEnumerator Coroutine_ShowPopup(int index)
    {
        if (index == 0)
            _completePurchasePopup.SetActive(true);

        else if (index == 1)
            _shortagePopup.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (index == 0)
            _completePurchasePopup.SetActive(false);

        else if (index == 1)
            _shortagePopup.SetActive(false);
    }
}
