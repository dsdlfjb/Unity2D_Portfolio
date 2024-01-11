using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

[System.Serializable]
public class Sword
{
    public string _swordName;
    public string _skinPage;
    public Sprite _swordImage;
    public int _swordPrice;
    public bool _isPurchased;       // 아이템을 구매 했는지 안했는지
    public bool _isEquipped;        // 아이템을 장착 했는지 안했는지
}

[System.Serializable]
public class SkinList
{
    public List<Sword> _swords;
}

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

    [Header("무기 스킨 정보"), SerializeField]
    public List<Sword> _swordSkinList = new List<Sword>();      // 상점 무기 스킨 목록
    public Text _swordSkinText;
    public Text _swordSkinPriceText;
    public Text _skinPage;
    public Image _swordSkinImage;
    public Button _buyButton;

    public Text _idText;
    public TMP_Text _coinText;
    public int nowNum;
    public string path;
    public SkinList _skinList;      // Inspector에서 할당

    int _selectedSkinIndex = 0;

    private void Start()
    {
        // 초기 무기 스킨 표시
        DisplaySkin(_selectedSkinIndex);
        _idText.text = DataManager.Instance.nowPlayer.name + "님";
        _coinText.text = DataManager.Instance.nowPlayer.coin.ToString();
    }

    public void NextSkin()
    {
        _selectedSkinIndex = (_selectedSkinIndex + 1) % _swordSkinList.Count;
        DisplaySkin(_selectedSkinIndex);
    }

    public void PreviousSkin()
    {
        _selectedSkinIndex = (_selectedSkinIndex - 1 + _swordSkinList.Count) % _swordSkinList.Count;
        DisplaySkin(_selectedSkinIndex);
    }

    public void BuySkin(Sword skin)
    {
        Sword selectedItem = _swordSkinList[_selectedSkinIndex];
        
        if (DataManager.Instance.nowPlayer.coin > selectedItem._swordPrice && !selectedItem._isPurchased)
        {
            // 플레이어가 충분한 돈을 가지고 있고 아직 구매하지 않았다면
            DataManager.Instance.nowPlayer.coin -= selectedItem._swordPrice;
            selectedItem._isPurchased = true;
            _buyButton.interactable = false;        // 이미 구매한 아이템은 더 이상 구매할 수 없게 버튼 비활성화
            _skinList._swords.Add(skin);
            // 여기에서 아이템을 플레이어에게 추가하거나 업그레이드하는 코드를 작성하세요.

            //Json 데이터로 저장
            SaveInventoryToJson();
        }
    }

    void DisplaySkin(int index)
    {
        Sword _skin = _swordSkinList[index];
        _swordSkinText.text = _skin._swordName;
        _skinPage.text = _skin._skinPage;
        _swordSkinPriceText.text = _skin._swordPrice.ToString();
        _swordSkinImage.sprite = _skin._swordImage;
        _buyButton.interactable = !_skin._isPurchased;
    }

    // Json 저장 및 로드 관련 함수

    void SaveInventoryToJson()
    {
        string data = JsonUtility.ToJson(_skinList);
        File.WriteAllText(path + nowNum.ToString(), data);
    }
}
