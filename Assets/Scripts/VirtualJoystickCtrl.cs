using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class VirtualJoystickCtrl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ���̽�ƽ�� _rectTransform.anchoredPosition (���ذ�)���� ���� �� Ÿ�ٹ����� �������� �޴� ���� ���ذ��� _rectTransform.anchoredPosition���� �����ϰ�..
    [SerializeField]
    RectTransform _lever;
    RectTransform _rectTransform; // JoyStick Ʈ������

    [SerializeField, Range(30, 100)]
    float _leverRange = 50;

    Vector2 _inputDir;
    Vector2 _firstInputDir;
    public Vector2 _InputDir => _inputDir;
    void Awake() { _rectTransform = GetComponent<RectTransform>(); }
    // Start is called before the first frame update
    void Start()
    {
        _inputDir = Vector2.zero;
        _firstInputDir = Vector2.zero;
    }

    void ControlLever(PointerEventData eventData)
    {
        //if(eventData.position.x > Screen.width * 0.5)// ȭ���� ������ ��ġ�Ҷ�
        //{
        //    Vector2 vec = new Vector2(eventData.position.x - Screen.width, eventData.position.y);//Ŭ����ġ���� ���� ȭ���� x���� ���� ������ �����ϴ� �𼭸��� �ٲ�
        //    var inputDir = vec - _rectTransform.anchoredPosition;//anchoredPosition ��Ŀ�� ���������� �Ǻ��� ��ġ��
        //    var clampedDir = inputDir.magnitude < _leverRange ? inputDir : inputDir.normalized * _leverRange;//�Ϲ�ȭ �Ͽ� ���� �ȿ��� �����ϰ� ��
        //    _lever.anchoredPosition = clampedDir;
        //    _inputDir = clampedDir / _leverRange;
        //}
        //else//ȭ���� ������ ��ġ�Ҷ�
        //{
        //    var inputDir = eventData.position - _firstInputDir; 
        //    Debug.Log(inputDir);
        //    //var inputDir = eventData.position - _rectTransform.anchoredPosition;//anchoredPosition ��Ŀ�� ���������� �Ǻ��� ��ġ��
        //    var clampedDir = inputDir.magnitude < _leverRange ? inputDir : inputDir.normalized * _leverRange;//�Ϲ�ȭ �Ͽ� ���� �ȿ��� �����ϰ� ��
        //    _lever.anchoredPosition = clampedDir;
        //    _inputDir = clampedDir / _leverRange;
        //}
        //-------------------------------------------------------
        //--------------��� �ػ󵵿����� ���� ���� ������------------
        var inputDir = eventData.position - _firstInputDir;
        //Debug.Log(inputDir);
        //var inputDir = eventData.position - _rectTransform.anchoredPosition;//anchoredPosition ��Ŀ�� ���������� �Ǻ��� ��ġ��
        var clampedDir = inputDir.magnitude < _leverRange ? inputDir : inputDir.normalized * _leverRange;//�Ϲ�ȭ �Ͽ� ���� �ȿ��� �����ϰ� ��
        _lever.anchoredPosition = clampedDir;
        _inputDir = clampedDir / _leverRange;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _firstInputDir = eventData.position;
        //Debug.Log(eventData.position);
        //Debug.Log(Screen.width);
    }// �� ��ũ��Ʈ�� ���� ������Ʈ��  ���콺 �巡�׸� ���������� ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
    } // �巡�� ���ϵ��� ȣ��
    public void OnEndDrag(PointerEventData eventData)
    {
        _lever.anchoredPosition = Vector2.zero;
        _inputDir = _lever.anchoredPosition;
    } //�巡�װ� �������� ȣ��

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_InputDir.x + " : " + _InputDir.y);

    }
}
