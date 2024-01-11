// 상점을 관리하는 스크립트
using System.Collections;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject CompletePurchasePopup;
    [SerializeField] private GameObject ShortagePopup;
    [SerializeField] private GameObject SoldoutPanel;
    [SerializeField] private GameObject SkillSoldoutPanel;

    // Spcae 어트리뷰트는 인스펙터에서 변수끼리의 간격을 줌
    [Space(20)]
    [SerializeField] private UnityEngine.UI.Image SwordIcon;
    [SerializeField] private UnityEngine.UI.Text PriceText;
    [SerializeField] private UnityEngine.UI.Text SwordName;

    [SerializeField] private UnityEngine.UI.Text SkillPriceText;
    [SerializeField] private UnityEngine.UI.Text SkillLevel;

    private IEnumerator _ePopupDirect;
    private int _currentSwordIndex;
    private int _currentSkillIndex;
    private int maxSkillLevel = 6;

    private bool _purchaseSword = false;
    private bool _purchaseSkill = false;

    private void Awake()
    {
        _currentSwordIndex = 0;
        _currentSkillIndex = 0;

        for (int i = DataManager.Instance.nowPlayer.upgradeSkill; 0 <= i-1; i--)
        {
            Managers.Save._saveData.GetUpgradeSkillData(i)._isPurchased = true;
            Managers.Save.Save();               
        }
        _currentSkillIndex = DataManager.Instance.nowPlayer.upgradeSkill + 1;
        
        Debug.Log("스킬 업그레이드 : " + (DataManager.Instance.nowPlayer.upgradeSkill + 1) + _currentSkillIndex);
        //UpdateUI();

        //this.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void OnDestroy()
    {
        // 코루틴이 실행중인 도중에 게임오브젝트가 파괴되면 에러가 발생하기때문에,
        // 모든 코루틴을 중지하도록 함
        if (_ePopupDirect != null)
            StopCoroutine(_ePopupDirect);

        _ePopupDirect = null;
    }

    public void ClickPrevious()
    {
        _currentSwordIndex--;

        if (_currentSwordIndex < 0)
            _currentSwordIndex = Define.Constant.SWORD_NUMBER - 1;

        UpdateUI();
    }

    public void ClickPreviousSkill()
    {
        //_currentSkillIndex--;

        //if (_currentSkillIndex < 0)
        //    _currentSkillIndex = 4;

        //UpdateUI();
    }

    public void ClickNext()
    {
        _currentSwordIndex++;

        if (_currentSwordIndex >= Define.Constant.SWORD_NUMBER)
            _currentSwordIndex = 0;

        UpdateUI();
    }

    public void ClickNextSkill()
    {
        //_currentSkillIndex++;

        //if (_currentSkillIndex >= 5)
        //    _currentSkillIndex = 0;

        //UpdateUI();
    }

    private void UpdateUI()
    {
        SwordIcon.sprite = Managers.Resource.GetSprite(Enum.ESpriteKey.Sword_1 + _currentSwordIndex);
        SwordIcon.SetNativeSize();

        PriceText.text = Managers.Save._saveData.GetSwordData(_currentSwordIndex)._price.ToString();

        if (_currentSkillIndex == maxSkillLevel)
        {
            SkillPriceText.text = Managers.Save._saveData.GetUpgradeSkillData(_currentSkillIndex - 2)._price.ToString();
        }

        else
        {
            SkillPriceText.text = Managers.Save._saveData.GetUpgradeSkillData(_currentSkillIndex - 1)._price.ToString();
        }

        SoldoutPanel.SetActive(Managers.Save._saveData.GetSwordData(_currentSwordIndex)._isPurchased);

        if (_currentSkillIndex == maxSkillLevel)
        {
            SkillSoldoutPanel.SetActive(true);
        }

        else
        {
            SkillSoldoutPanel.SetActive(false);
        }

        SwordName.text = Define.Constant.SWORD_NAMES[_currentSwordIndex];

        if (_currentSkillIndex == maxSkillLevel)
        {
            SkillLevel.text = "번개 lv" + (_currentSkillIndex - 1);
        }

        else
        {
            SkillLevel.text = "번개 lv" + (_currentSkillIndex);
        }
    }
    
    // 구매 버튼 클릭 함수
    public void ClickPurchase()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);

        var swordData = Managers.Save._saveData.GetSwordData(_currentSwordIndex);
        _purchaseSword = true;

        if (swordData._isPurchased == false)
        {
            if (swordData._price <= DataManager.Instance.nowPlayer.coin)
            {
                DataManager.Instance.nowPlayer.coin -= swordData._price;
                swordData._isPurchased = true;
                Managers.Event.InvokeEvent(Enum.EEventKey.OnPurchaseSword, _currentSwordIndex);
                Managers.Save.Save();

                // 코인이 있는 데이터를 저장
                DataManager.Instance.Save();

                if (_ePopupDirect != null)
                    StopCoroutine(_ePopupDirect);
                _ePopupDirect = this.Co_ShowPopup(0);
                StartCoroutine(_ePopupDirect);
                UpdateUI();
            }

            else
            {
                if (_ePopupDirect != null)
                    StopCoroutine(_ePopupDirect);

                _ePopupDirect = this.Co_ShowPopup(1);
                StartCoroutine(_ePopupDirect);
            }
        }
    }
    
    // 업그레이드 버튼 클릭 함수
    public void ClickUpgrade()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);

        var skillData = Managers.Save._saveData.GetUpgradeSkillData(_currentSkillIndex-1);
        _purchaseSkill = true;

        if (skillData._price <= DataManager.Instance.nowPlayer.coin)
        {
            DataManager.Instance.nowPlayer.coin -= skillData._price;
            skillData._isPurchased = true;
            Managers.Event.InvokeEvent(Enum.EEventKey.OnPurchaseSword, _currentSwordIndex);
            Managers.Save.Save();

            DataManager.Instance.nowPlayer.upgradeSkill = _currentSkillIndex;
            DataManager.Instance.Save();
            _currentSkillIndex++;

            if (_ePopupDirect != null)
                StopCoroutine(_ePopupDirect);
            _ePopupDirect = this.Co_ShowPopup(0);
            StartCoroutine(_ePopupDirect);
            UpdateUI();
        }

        else
        {
            if (_ePopupDirect != null)
                StopCoroutine(_ePopupDirect);

            _ePopupDirect = this.Co_ShowPopup(1);
            StartCoroutine(_ePopupDirect);
        }
    }

    // 코루틴을 사용해서 '코인이 부족합니다' 또는 '구매 완료' 팝업을 띄웠다가 사라지게 하도록 함
    private IEnumerator Co_ShowPopup(int index)
    {
        if (index == 0)
            CompletePurchasePopup.SetActive(true);

        else if (index == 1)
            ShortagePopup.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (index == 0)
            CompletePurchasePopup.SetActive(false);

        else if (index == 1)
            ShortagePopup.SetActive(false);
    }
}
