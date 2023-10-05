using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Enum;

/*
    �� �̺�Ʈ �޴����� ���ӿ��� �ſ� �߿�
    �ܺο����� Managers.Event.RegistEvent�� ���ؼ� �ش��ϴ� �̺�ƮŰ��, Action�� ��� ����
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

    // RegistEvent�� RemoveEvent�� �����ε��� ���ؼ� ����ϱ� ���� �ۼ��ϱ� ���ϵ��� ��
    // �ش� �̺�Ʈ�� �����ų ������ InvokeEvent�Լ��� ȣ��

    // RegistEvent�� �ѵڿ� RemoveEvent�� ���ؼ� ������ �ʴ´ٸ� RegistEvent�� ����ߴ� GameObject�� �ı��Ǵ���
    // ����ؼ� �����Ϸ��� �ϱ� ������ RemoveEvent�� ���ؼ� �� ��������

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