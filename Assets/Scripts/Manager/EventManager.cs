using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Enum;

/*
    이 이벤트 메니저는 게임에서 매우 중요
    외부에서는 Managers.Event.RegistEvent를 통해서 해당하는 이벤트키와, Action을 등록 가능
*/

public class EventManager : MonoBehaviour
{
    private Dictionary<EEventKey, Action> _dicBasicEvent_none;
    private Dictionary<EEventKey, Action<object>> _dicBasicEvent_one;
    private Dictionary<EEventKey, Action<object, object>> _dicBasicEvent_two;

    private void Awake()
    {
        _dicBasicEvent_none = new Dictionary<EEventKey, Action>();
        _dicBasicEvent_one = new Dictionary<EEventKey, Action<object>>();
        _dicBasicEvent_two = new Dictionary<EEventKey, Action<object, object>>();
    }

    #region ## Resist And Remove ##

    // RegistEvent와 RemoveEvent는 오버로딩을 통해서 기억하기 쉽고 작성하기 편하도록 함
    // 해당 이벤트를 실행시킬 때에는 InvokeEvent함수를 호출

    // RegistEvent를 한뒤에 RemoveEvent를 통해서 지우지 않는다면 RegistEvent로 등록했던 GameObject가 파괴되더라도
    // 계속해서 참조하려고 하기 때문에 RemoveEvent를 통해서 꼭 지워야함

    public void RegistEvent(EEventKey eventType, Action action)
    {
        if (_dicBasicEvent_none.ContainsKey(eventType))
            _dicBasicEvent_none[eventType] += action;

        else
            _dicBasicEvent_none.Add(eventType, action);
    }

    public void RegistEvent(EEventKey eventType, Action<object> action)
    {
        if (_dicBasicEvent_one.ContainsKey(eventType))
            _dicBasicEvent_one[eventType] += action;

        else
            _dicBasicEvent_one.Add(eventType, action);
    }

    public void RegistEvent(EEventKey eventType, Action<object, object> action)
    {
        if (_dicBasicEvent_two.ContainsKey(eventType))
            _dicBasicEvent_two[eventType] += action;

        else
            _dicBasicEvent_two.Add(eventType, action);
    }

    public void RemoveEvent(EEventKey eventType, Action action)
    {
        if (_dicBasicEvent_none.ContainsKey(eventType))
            _dicBasicEvent_none[eventType] -= action;

        else
            Debug.LogError($"{nameof(_dicBasicEvent_none)}.{eventType} is Already Empry In Dictionary.");
    }

    public void RemoveEvent(EEventKey eventType, Action<object> action)
    {
        if (_dicBasicEvent_one.ContainsKey(eventType))
            _dicBasicEvent_one[eventType] -= action;

        else
            Debug.LogError($"{nameof(_dicBasicEvent_one)}.{eventType} is Already Empry In Dictionary.");
    }

    public void RemoveEvent(EEventKey eventType, Action<object, object> action)
    {
        if (_dicBasicEvent_two.ContainsKey(eventType))
            _dicBasicEvent_two[eventType] -= action;

        else
            Debug.LogError($"{nameof(_dicBasicEvent_two)}.{eventType} is Already Empry In Dictionary.");
    }
    #endregion

    #region ## Invoke ##
    public void InvokeEvent(EEventKey type)
    {
        if (_dicBasicEvent_none.ContainsKey(type))
            _dicBasicEvent_none[type]?.Invoke();

        else
            Debug.Log($"\n{nameof(EventManager)}.{nameof(_dicBasicEvent_none)} has not {type}.\n Please Chack.\n");
    }

    public void InvokeEvent(EEventKey type, object param1)
    {
        if (_dicBasicEvent_one.ContainsKey(type))
            _dicBasicEvent_one[type]?.Invoke(param1);

        else
            Debug.Log($"\n{nameof(EventManager)}.{nameof(_dicBasicEvent_one)} has not {type}.\n Please Check.\n");
    }

    public void InvokeEvent(EEventKey type, object param1, object param2)
    {
        if (_dicBasicEvent_two.ContainsKey(type))
            _dicBasicEvent_two[type]?.Invoke(param1, param2);

        else
            Debug.Log($"\n{nameof(EventManager)}.{nameof(_dicBasicEvent_two)} has not {type}.\n Please Check.\n");
    }
    #endregion
}