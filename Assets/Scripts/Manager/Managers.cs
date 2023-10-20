// �̱���Ŵ������� �����ϴ� ��ũ��Ʈ
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance = null;

    public static Managers Instance
    {
        get
        {
            // ���� ���α׷��� ���������� üũ
            // ������ üũ�� ���� ������, ������ ����� �� ������ ��Ÿ�� ���� ���� ����� ���� ���� ���� ����
            if (_isApplicationQuitting)
                return null;
            // �������� �ƴ϶�� _instance ������ Null���� üũ
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
        // �ȹ��丮 �Ŵ����� �����ϱ� ���ؼ� ���ο� ���� ������Ʈ Inventory�� �����ϰ� �ش� ���� ������Ʈ�� InventoryManager ������Ʈ�� �߰�
        _inventory = new GameObject("Inventory").AddComponent<InventoryManager>();
        // Inspector â���� ���� ���ϰ� Manager ���� ������Ʈ�� ����(�ڽ�)�� �������
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
