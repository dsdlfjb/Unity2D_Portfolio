// 싱글톤매니저들을 관리하는 스크립트
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance = null;

    public static Managers Instance
    {
        get
        {
            // 현재 프로그램이 종료중인지 체크
            // 종료중 체크를 하지 않으면, 게임이 종료될 때 오류가 나타나 저장 등이 제대로 되지 않을 수도 있음
            if (_isApplicationQuitting)
                return null;
            // 종료중이 아니라면 _instance 변수가 Null인지 체크
            else if (_instance == null)
            {
                _instance = new GameObject("Managers").AddComponent<Managers>();

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
    private InventoryManager _inventory = null;

    /// <summary>
    /// It's Save and Load Manager(Singleton)
    /// </summary>
    private SaveManager _save = null;

    private ResourceManager _resource = null;
    private EventManager _event = null;
    #endregion

    #region Manager Properties
    public static InventoryManager Inventory { get => Instance._inventory; }
    public static SaveManager Save { get => Instance._save; }
    public static ResourceManager Resource { get => Instance._resource; }
    public static EventManager Event { get => Instance._event; }
    #endregion

    #region Properties
    public int CurrentStageLevel { get; set; }
    #endregion

    void CreateManagers()
    {
        // 안밴토리 매니저를 생성하기 위해서 새로운 게임 오브젝트 Inventory를 생성하고 해당 게임 오브젝트에 InventoryManager 컴포넌트를 추가
        _inventory = new GameObject("Inventory").AddComponent<InventoryManager>();
        // Inspector 창에서 보기 편하게 Manager 게임 오브젝트의 하위(자식)에 집어넣음
        _inventory.transform.SetParent(this.transform);

        _save = new GameObject("Save").AddComponent<SaveManager>();
        _save.transform.SetParent(this.transform);

        _resource = new GameObject("Resource").AddComponent<ResourceManager>();
        _resource.transform.SetParent(this.transform);

        _event = new GameObject("Event").AddComponent<EventManager>();
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
