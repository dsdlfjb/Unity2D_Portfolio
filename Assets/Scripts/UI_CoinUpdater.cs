using UnityEngine;
using TMPro;

public class UI_CoinUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Awake()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RegistEvent(Enum.EEventKey.OnPurchaseSword, this.UIUpdate);
        }

        _coinText.text = DataManager.Instance.nowPlayer.coin.ToString();
    }

    private void OnDestroy()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RegistEvent(Enum.EEventKey.OnPurchaseSword, this.UIUpdate);
        }
    }

    void UIUpdate(object swordIndex)
    {
        _coinText.text = DataManager.Instance.nowPlayer.coin.ToString();
    }
}
