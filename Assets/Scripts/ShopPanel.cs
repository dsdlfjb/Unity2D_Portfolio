// ������ �����ϴ� ��ũ��Ʈ
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject _completePurchasePopup;
    [SerializeField] private GameObject _shortagePopup;
    [SerializeField] private GameObject _soldoutPanel;

    // Space ��Ʈ����Ʈ�� �ν����Ϳ��� ���������� ������ �ֱ����� ���
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
        // �ڷ�ƾ�� �������� ���߿� ���ӿ�����Ʈ�� �ı��Ǹ� ������ �߻��ϱ� ������ ��� �ڷ�ƾ�� �����ϵ��� ��
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

        // �ν����Ϳ��� Image ������Ʈ �Ʒ��� Set Native Size ��ư�� ������ ���
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

    // �ڷ�ƾ�� ����ؼ� "������ �����մϴ�" �Ǵ� "���� �Ϸ�" �˾��� ����ٰ� ������� �ϵ��� ��
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
