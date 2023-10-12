using UnityEngine;

// 이 스크립트는 싱글톤매니저들을 관리하는 스크립트
// 외부 클래스에서 한번이라도 이 클래스를 호출하면 그 즉시 DonDestroy를 통해서 Inspector에 생성
// "Managers"라는 이름의 게임오브젝트가 생성되고, 관리되는 매니저는 해당 게임오브젝트의 자식으로 생성

public class Managers : MonoBehaviour
{
    private static Managers _instance = null;

    public static Managers Instance
    {
        get
        {
            // 현재 프로그램이 종료중인지 체크
            // 종료중 체크를 하지 않으면, 게임이 종료될 때 오류가 나타나 저장등이 제대로 되지 않을 수 있음
            if (_isApplicationQuitting)
                return null;
            // 종료중이 아니라면 sInstance 변수가 Null인지 체크
            else if (_instance == null)
            {
                // _Instance가 null이라면, 즉, 한번도 Managers가 호출된적이 없다면
                // 게임오브젝트를 그 즉시 생성
                // AddComponent를 하는 순간 Managers스크립트의 Awake() 함수가 호출
                _instance = new GameObject("Managers").AddComponent<Managers>();

                // 씬이 전환되도 게임오브젝트가 파괴되지 않도록 지정
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private static bool _isApplicationQuitting;

    #region Managers

    /// <summary>
    /// It's Inventory Manager(Singleton)
    /// </summary>
    /// 

    private InventoryManager _inventory = null;

    /// <summary> It's Save and Load Manager (Singleton)</summary>
    private SaveManager _save = null;

    private ResourceManager _resource = null;
    private EventManager _event = null;
    #endregion

    #region Manager Properties

    public static InventoryManager Inventory { get => Instance._inventory; }
    public static SaveManager Save { get => Instance._save; }
    public static ResourceManager Resouce { get => Instance._resource; }
    public static EventManager Event { get => Instance._event; }
    #endregion

    #region Properties
    public int CurrentStageLevel { get; set; }
    #endregion

    /// <summary>
    /// Create Singleton Managers.
    /// </summary>
    private void CreateManagers()
    {

        // 인벤토리 매니저를 생성하기 위해서 새로운 게임오브젝트 _Invetory를 생성하고 해당 게임오브젝트에 InventoryManager 컴포넌트를 추가
        _inventory = new GameObject("Inventory").AddComponent<InventoryManager>();
        // Inspector창에서 보기 편하기 위해서 Manager 게임오브젝트의 하위에 집어넣음
        _inventory.transform.SetParent(this.transform);

        _save = new GameObject("Save").AddComponent<SaveManager>();
        _save.transform.parent = this.transform;

        _resource = new GameObject("Resource").AddComponent<ResourceManager>();
        _resource.transform.SetParent(this.transform);

        _event= new GameObject("Event").AddComponent<EventManager>();
        _event.transform.SetParent(this.transform);
    }


    #region Behaviours
    private void Awake()
    {
        this.CreateManagers();
    }

    private void OnDestroy()
    {
        _isApplicationQuitting = true;
    }
    #endregion
}
