// ������ �����ϴ� ��ũ��Ʈ
using System.Collections;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject CompletePurchasePopup;
    [SerializeField] private GameObject ShortagePopup;
    [SerializeField] private GameObject SoldoutPanel;

    // Spcae ��Ʈ����Ʈ�� �ν����Ϳ��� ���������� ������ �ݴϴ�.
    [Space(20)]
    [SerializeField] private UnityEngine.UI.Image SwordIcon;
    [SerializeField] private UnityEngine.UI.Text PriceText;
    [SerializeField] private UnityEngine.UI.Text SwordName;

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
        // �ڷ�ƾ�� �������� ���߿� ���ӿ�����Ʈ�� �ı��Ǹ� ������ �߻��ϱ⶧����,
        // ��� �ڷ�ƾ�� �����ϵ��� �մϴ�.
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

    public void ClickNext()
    {
        _currentSwordIndex++;
        if (_currentSwordIndex >= Define.Constant.SWORD_NUMBER)
            _currentSwordIndex = 0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        SwordIcon.sprite = Managers.Resource.GetSprite(Enum.ESpriteKey.Sword_1 + _currentSwordIndex);

        SwordIcon.SetNativeSize();

        PriceText.text = Managers.Save._saveData.GetSwordData(_currentSwordIndex)._price.ToString();

        SoldoutPanel.SetActive(Managers.Save._saveData.GetSwordData(_currentSwordIndex)._isPurchased);

        SwordName.text = Define.Constant.SWORD_NAMES[_currentSwordIndex];
    }

    public void ClickPurchase()
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

                // ������ �ִ� �����͸� ����
                // ���̺� �����ʹ� �Ѱ����� �����ϴ°� ����
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

    // �ڷ�ƾ�� ����ؼ� '������ �����մϴ�' �Ǵ� '���� �Ϸ�' �˾��� ����ٰ� ������� �ϵ��� ��
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
