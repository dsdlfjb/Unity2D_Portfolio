using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum EItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("[ 메인 정보 ]")]
    public EItemType _eitemType;
    public int _itemId;
    public string _itemName;
    [TextArea]
    public string _itemDesc;
    public Sprite _itemIcon;

    [Header("[ 레벨 데이터 ]")]
    public float _baseDamage;
    public int _baseCount;
    public float[] _damages;
    public int[] _counts;

    [Header("[ 무기 ]")]
    public GameObject _projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
