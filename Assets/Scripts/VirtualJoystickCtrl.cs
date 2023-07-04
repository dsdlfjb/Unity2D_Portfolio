using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class VirtualJoystickCtrl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 조이스틱의 _rectTransform.anchoredPosition (기준값)에서 부터 의 타겟방향을 잡으려면 받는 값의 기준값을 _rectTransform.anchoredPosition에서 시작하게..
    [SerializeField]
    RectTransform _lever;
    RectTransform _rectTransform; // JoyStick 트랜스폼

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
        //if(eventData.position.x > Screen.width * 0.5)// 화면의 우측을 터치할때
        //{
        //    Vector2 vec = new Vector2(eventData.position.x - Screen.width, eventData.position.y);//클릭위치에서 현재 화면의 x값을 빼서 기준을 우측하단 모서리로 바꿈
        //    var inputDir = vec - _rectTransform.anchoredPosition;//anchoredPosition 앵커를 기준으로한 피봇의 위치값
        //    var clampedDir = inputDir.magnitude < _leverRange ? inputDir : inputDir.normalized * _leverRange;//일반화 하여 범위 안에서 동작하게 함
        //    _lever.anchoredPosition = clampedDir;
        //    _inputDir = clampedDir / _leverRange;
        //}
        //else//화면의 좌측을 터치할때
        //{
        //    var inputDir = eventData.position - _firstInputDir; 
        //    Debug.Log(inputDir);
        //    //var inputDir = eventData.position - _rectTransform.anchoredPosition;//anchoredPosition 앵커를 기준으로한 피봇의 위치값
        //    var clampedDir = inputDir.magnitude < _leverRange ? inputDir : inputDir.normalized * _leverRange;//일반화 하여 범위 안에서 동작하게 함
        //    _lever.anchoredPosition = clampedDir;
        //    _inputDir = clampedDir / _leverRange;
        //}
        //-------------------------------------------------------
        //--------------어느 해상도에서도 오차 없이 움직임------------
        var inputDir = eventData.position - _firstInputDir;
        //Debug.Log(inputDir);
        //var inputDir = eventData.position - _rectTransform.anchoredPosition;//anchoredPosition 앵커를 기준으로한 피봇의 위치값
        var clampedDir = inputDir.magnitude < _leverRange ? inputDir : inputDir.normalized * _leverRange;//일반화 하여 범위 안에서 동작하게 함
        _lever.anchoredPosition = clampedDir;
        _inputDir = clampedDir / _leverRange;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _firstInputDir = eventData.position;
        //Debug.Log(eventData.position);
        //Debug.Log(Screen.width);
    }// 이 스크립트가 붙은 오브젝트에  마우스 드래그를 시작했을때 호출
    public void OnDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
    } // 드래그 중일동안 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        _lever.anchoredPosition = Vector2.zero;
        _inputDir = _lever.anchoredPosition;
    } //드래그가 끝났을때 호출

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_InputDir.x + " : " + _InputDir.y);

    }
}
