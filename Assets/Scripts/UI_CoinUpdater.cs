using UnityEngine;
using Enum;

public class UI_CoinUpdater : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _coinText;
    private void Awake()
    {
        if(Managers.Instance != null)
        {
            Managers.Event.RegistEvent(EEventKey.OnPurcharseSword, this.UIUpdate);
        }

        _coinText.text = DataManager.Instance.nowPlayer.coin.ToString();
    }

    private void OnDestroy()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RemoveEvent(EEventKey.OnPurcharseSword, this.UIUpdate);
        }
    }

    private void UIUpdate(object swordIndex)
    {
        _coinText.text = DataManager.Instance.nowPlayer.coin.ToString();
    }
}
