using UnityEngine;

// �� ��ũ��Ʈ�� �̱���Ŵ������� �����ϴ� ��ũ��Ʈ
// �ܺ� Ŭ�������� �ѹ��̶� �� Ŭ������ ȣ���ϸ� �� ��� DonDestroy�� ���ؼ� Inspector�� ����
// "Managers"��� �̸��� ���ӿ�����Ʈ�� �����ǰ�, �����Ǵ� �Ŵ����� �ش� ���ӿ�����Ʈ�� �ڽ����� ����

public class Managers : MonoBehaviour
{
    private static Managers _instance = null;

    public static Managers Instance
    {
        get
        {
            // ���� ���α׷��� ���������� üũ
            // ������ üũ�� ���� ������, ������ ����� �� ������ ��Ÿ�� ������� ����� ���� ���� �� ����
            if (_isApplicationQuitting)
                return null;
            // �������� �ƴ϶�� sInstance ������ Null���� üũ
            else if (_instance == null)
            {
                // _Instance�� null�̶��, ��, �ѹ��� Managers�� ȣ������� ���ٸ�
                // ���ӿ�����Ʈ�� �� ��� ����
                // AddComponent�� �ϴ� ���� Managers��ũ��Ʈ�� Awake() �Լ��� ȣ��
                _instance = new GameObject("Managers").AddComponent<Managers>();

                // ���� ��ȯ�ǵ� ���ӿ�����Ʈ�� �ı����� �ʵ��� ����
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

        // �κ��丮 �Ŵ����� �����ϱ� ���ؼ� ���ο� ���ӿ�����Ʈ _Invetory�� �����ϰ� �ش� ���ӿ�����Ʈ�� InventoryManager ������Ʈ�� �߰�
        _inventory = new GameObject("Inventory").AddComponent<InventoryManager>();
        // Inspectorâ���� ���� ���ϱ� ���ؼ� Manager ���ӿ�����Ʈ�� ������ �������
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
