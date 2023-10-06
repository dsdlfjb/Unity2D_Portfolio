using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject _completePurchasePopup;
    [SerializeField] private GameObject _shortagePopup;
    [SerializeField] private GameObject _soldoutPanel;

    // Spcae ��Ʈ����Ʈ�� �ν����Ϳ��� ���������� ������ ��
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
        // �ڷ�ƾ�� �������� ���߿� ���ӿ�����Ʈ�� �ı��Ǹ� ������ �߻��ϱ⶧����,
        // ��� �ڷ�ƾ�� �����ϵ��� �ϴ°� ����
        if (_ePopupDirect != null)
            StopCoroutine(_ePopupDirect);
        _ePopupDirect = null;
    }

    public void ClickPrevious()
    {
        // ����ؼ� �������� �� �� �ֵ��� ������ ����
        _currentSwordIndex--;

        if (_currentSwordIndex < 0)
            // �ε����̱� ������ 1�� ��
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

        // SetNativeSize�� �� �״�� ����Ƽ�� ������� �����ϵ��� ��
        // �ν����Ϳ��� Image ������Ʈ �Ʒ��� Set Native Size ��ư�� ������ ����� ��
        // Native ������� �ؽ��� Ŭ���ϸ� �ν����Ϳ��� ���̴� �ؽ��� ���ÿ��� 'Pizels Per Unit'�� ������ ����
        _swordIcon.SetNativeSize();

        _priceText.text = Managers.Save.saveData.GetSwordData(_currentSwordIndex)._price.ToString();

        // ���� ���⸦ ���������� ����ó��
        _soldoutPanel.SetActive(Managers.Save.saveData.GetSwordData(_currentSwordIndex)._isPurchased);

        // ���� �� �ε����� ���� �̸��� ����
        _swordName.text = Define.Constant.SWORD_NAMES[_currentSwordIndex];
    }

    public void ClickPurchase()
    {
        var swordData = Managers.Save.saveData.GetSwordData(_currentSwordIndex);

        // �������� ���ٸ�
        if (swordData._isPurchased == false)
        {
            if (swordData._price <= DataManager.Instance.nowPlayer.coin)
            {
                DataManager.Instance.nowPlayer.coin -= swordData._price;

                swordData._isPurchased = true;

                Managers.Event.InvokeEvent(Enum.EEventKey.OnPurcharseSword, _currentSwordIndex);
                Managers.Save.SaveData();

                // ������ �ִ� �����͸� ����.
                // ���̺� �����ʹ� �Ѱ����� �����ϴ°� ����
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

    // �ڷ�ƾ�� ����ؼ� '������ �����մϴ�' �Ǵ� '���� �Ϸ�' �˾��� ����ٰ� ������� �ϵ��� ��
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
