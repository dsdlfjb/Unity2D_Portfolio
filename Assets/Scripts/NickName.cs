using UnityEngine;
using TMPro;

public class NickName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nickNameText;

    private void Awake()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RegistEvent(Enum.EEventKey.OnPlayerName, this.UIUpdate);
        }

        _nickNameText.text = DataManager.Instance.nowPlayer.name;
    }

    private void OnDestroy()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RegistEvent(Enum.EEventKey.OnPlayerName, this.UIUpdate);
        }
    }

    void UIUpdate(object swordIndex)
    {
        _nickNameText.text = "ID : " + DataManager.Instance.nowPlayer.name;
    }
}
