using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    #region 싱글턴
    public static ShopManager Instance;

    void Awake()
    {
        // 인스턴스가 비어있다면
        if (Instance == null)
        {
            // 자기자신으로 채우고, 씬 이동시에도 삭제되지 않도록
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // 인스턴스가 비어있지 않지만 자기자신과 다르다면 = 새로 생긴 객체
        else if (Instance != this)
        {
            // 자기자신 삭제
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("[ 무기 스킨 정보 ]")]
    public Sprite[] _swordSpriteList; // 스킨 종류
    public Sprite[] _shadowSwordSpriteList; // 스킨 그림자 종류
    public string[] _swordNameList;   // 스킨 이름
    public string[] _swordPageList;    // 스킨 페이지
    public int[] _swordCooperList;    // 해금 조건에 필요한 구리코인
    public int[] _swordCoinList;      // 구매 조건에 필요한 일반코인
    
    public GameObject lockGroup; // 해금하기 전 UI
    public Button buyButtonn;    // 해금하기 전 UI
    public GameObject _coinCompletePanel;
    public GameObject _coinShortagePanel;
    public GameObject _cooperCompletePanel;
    public GameObject _cooperShortagePanel;

    public Text _nickNameText;
    public Text _lockGroupPage;
    public Text _unlockGroupPage;
    public TMP_Text _coinText;
    public TMP_Text _cooperText;

    int _clickCount;
    int _minCount;
    int _maxCount;

    private void Start()
    {
        _nickNameText.text = "ID : " + DataManager.Instance.nowPlayer.name;
        GameObject.Find("Image - Sword").GetComponent<Image>().sprite = _swordSpriteList[0];
        GameObject.Find("Image - ShadowSword").GetComponent<Image>().sprite = _shadowSwordSpriteList[0];
        GameObject.Find("Text - SwordName").GetComponent<Text>().text = _swordNameList[0];
        GameObject.Find("Text - Need Coin").GetComponent<Text>().text = _swordCoinList[0].ToString();
        GameObject.Find("Text - Need Cooper").GetComponent<Text>().text = _swordCooperList[0].ToString();
        GameObject.Find("Text - Lock Skin Page").GetComponent<Text>().text = _swordPageList[_clickCount].ToString();
        GameObject.Find("Text - UnLock Skin Page").GetComponent<Text>().text = _swordPageList[_clickCount].ToString();
        GameObject.Find("Text - SwordSkin").GetComponent<Text>().text = _swordCoinList[_clickCount].ToString();
        _maxCount = _swordSpriteList.Length - 1;
        _minCount = 0;
    }

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

    public void Next_Button()
    {
        _clickCount++;

        if (_clickCount > _maxCount) _clickCount = 0;

        if (GameObject.Find("Image - ShadowSword").GetComponent<Image>().sprite == null) return;

        GameObject.Find("Image - Sword").GetComponent<Image>().sprite = _swordSpriteList[_clickCount];
       // GameObject.Find("Image - ShadowSword").GetComponent<Image>().sprite = _shadowSwordSpriteList[_clickCount];
        GameObject.Find("Text - SwordName").GetComponent<Text>().text = _swordNameList[_clickCount];
        GameObject.Find("Text - Need Coin").GetComponent<Text>().text = _swordCoinList[_clickCount].ToString();
        GameObject.Find("Text - Need Cooper").GetComponent<Text>().text = _swordCooperList[_clickCount].ToString();
        GameObject.Find("Text - Lock Skin Page").GetComponent<Text>().text = _swordPageList[_clickCount].ToString();
        GameObject.Find("Text - UnLock Skin Page").GetComponent<Text>().text = _swordPageList[_clickCount].ToString();
    }

    public void Previous_Button()
    {
        _clickCount--;

        if (_clickCount < _minCount) _clickCount = 11;

        if (GameObject.Find("Image - ShadowSword").GetComponent<Image>().sprite == null) return;
        
        GameObject.Find("Image - Sword").GetComponent<Image>().sprite = _swordSpriteList[_clickCount];
        //GameObject.Find("Image - ShadowSword").GetComponent<Image>().sprite = _shadowSwordSpriteList[_clickCount];
        GameObject.Find("Text - SwordName").GetComponent<Text>().text = _swordNameList[_clickCount];
        GameObject.Find("Text - Need Coin").GetComponent<Text>().text = _swordCoinList[_clickCount].ToString();
        GameObject.Find("Text - Need Cooper").GetComponent<Text>().text = _swordCooperList[_clickCount].ToString();
        GameObject.Find("Text - Lock Skin Page").GetComponent<Text>().text = _swordPageList[_clickCount].ToString();
        GameObject.Find("Text - UnLock Skin Page").GetComponent<Text>().text = _swordPageList[_clickCount].ToString();
    }
}
