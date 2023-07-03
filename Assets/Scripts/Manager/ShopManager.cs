using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    #region 싱글턴
    static ShopManager _instance;
    public static ShopManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ShopManager>();
            return _instance;
        }
    }
    #endregion

    [Header("[ 무기 스킨 정보 ]")]
    public Sprite[] _swordSpriteList; // 스킨 종류
    public string[] _swordNameList;   // 스킨 이름
    public int[] _swordCooperList;    // 해금 조건에 필요한 구리코인
    public int[] _swordCoinList;      // 구매 조건에 필요한 일반코인

    public GameObject lockGroup; // 해금하기 전 UI
    public Button buyButtonn;    // 해금하기 전 UI
    public GameObject _coinCompletePanel;
    public GameObject _coinShortagePanel;
    public GameObject _cooperCompletePanel;
    public GameObject _cooperShortagePanel;

    public TMP_Text _coinText;
    public TMP_Text _cooperText;

    private void Update()
    {
        _cooperText.text = DataManager.Instance._cooperScore.ToString();
        _coinText.text = DataManager.Instance._coinScore.ToString();
    }

    // 스킨 해금 버튼을 누르면 호출
    public void ClickOpenSkin(int skinIndex)
    {
        // 구리코인이 부족하다면 함수 탈출
        if (DataManager.Instance._cooperScore < _swordCooperList[skinIndex])
        {
            _cooperShortagePanel.SetActive(true);
            return;
        }

        _cooperCompletePanel.SetActive(true);

        // 구리코인 감소
        DataManager.Instance.RemoveCooper(_swordCooperList[skinIndex]);

        _cooperText.text = DataManager.Instance._cooperScore.ToString();

        // UI 변경 (구매가능한 UI가 보이도록 비활성화)
        lockGroup.SetActive(false);
    }

    // 스킨 구매 버튼을 누르면 호출
    public void ClickBuySkin(int skinIndex)
    {
        // 코인이 부족하다면 함수 탈출
        if (DataManager.Instance._coinScore < _swordCoinList[skinIndex])
        {
            _coinShortagePanel.SetActive(true);
            return;
        }

        _coinCompletePanel.SetActive(true);

        // 코인 감소
        DataManager.Instance.RemoveCoin(_swordCoinList[skinIndex]);

        _coinText.text = DataManager.Instance._coinScore.ToString();

        // 해금한 스킨의 이름 저장
        DataManager.Instance._swordSkinName = _swordNameList[skinIndex];

        // UI 변경 (구매가 완료되면 버튼 또 누를 수 없도록)
        buyButtonn.interactable = false;
    }
}
