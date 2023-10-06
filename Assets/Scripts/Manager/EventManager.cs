using System.Collections.Generic;
using UnityEngine;
using System;
using Enum;

/*
    이 이벤트 메니저는 게임에서 매우 중요.
    외부에서는 Managers.Event.RegistEvent를 통해서 해당하는 이벤트키와, Action을 등록 가능
*/

public class EventManager : MonoBehaviour
{
    // Action, Action<object>, Action<object,object>의 차이는
    // 파라미터를 몇개를 받을것인지에 따라 다름
    // 보통 2개까지도 잘 쓰지않지만, 필요에따라 Dictionary를 추가해도 됨
    private Dictionary<EEventKey, Action> _dicBasicEvents_none;
    private Dictionary<EEventKey, Action<object>> _dicBasicEvents_one;
    private Dictionary<EEventKey, Action<object, object>> _dicBasicEvents_two;

    private void Awake()
    {
        _dicBasicEvents_none = new Dictionary<EEventKey, Action>();
        _dicBasicEvents_one = new Dictionary<EEventKey, Action<object>>();
        _dicBasicEvents_two = new Dictionary<EEventKey, Action<object, object>>();
    }


    #region ## Regist and Remove ##

    // Regist =====================================================================================
    public void RegistEvent(EEventKey evtType, Action action)
    {
        if (_dicBasicEvents_none.ContainsKey(evtType))
        {
            _dicBasicEvents_none[evtType] += action;
        }
        else
        {
            _dicBasicEvents_none.Add(evtType, action);
        }
    }

    public void RegistEvent(EEventKey evtType, Action<object> action)
    {
        if (_dicBasicEvents_one.ContainsKey(evtType))
        {
            _dicBasicEvents_one[evtType] += action;
        }
        else
        {
            _dicBasicEvents_one.Add(evtType, action);
        }
    }

    public void RegistEvent(EEventKey evtType, Action<object, object> action)
    {
        if (_dicBasicEvents_two.ContainsKey(evtType))
        {
            _dicBasicEvents_two[evtType] += action;
        }
        else
        {
            _dicBasicEvents_two.Add(evtType, action);
        }
    }

    // Remove =====================================================================================
    public void RemoveEvent(EEventKey evtType, Action action)
    {
        if (_dicBasicEvents_none.ContainsKey(evtType))
        {
            _dicBasicEvents_none[evtType] -= action;
        }
        else
        {
            Debug.LogError($"{nameof(_dicBasicEvents_none)}.{evtType} is Already Empry In Dictionary.");
        }
    }

    public void RemoveEvent(EEventKey evtType, Action<object> action)
    {
        if (_dicBasicEvents_one.ContainsKey(evtType))
        {
            _dicBasicEvents_one[evtType] -= action;
        }
        else
        {
            Debug.LogError($"{nameof(_dicBasicEvents_one)}.{evtType} is Already Empry In Dictionary.");
        }
    }

    public void RemoveEvent(EEventKey evtType, Action<object, object> action)
    {
        if (_dicBasicEvents_two.ContainsKey(evtType))
        {
            _dicBasicEvents_two[evtType] -= action;
        }
        else
        {
            Debug.LogError($"{nameof(_dicBasicEvents_two)}.{evtType} is Already Empry In Dictionary.");
        }
    }
    #endregion


    #region ## Invoke ##
    public void InvokeEvent(EEventKey type)
    {
        if (_dicBasicEvents_none.ContainsKey(type))
        {
            _dicBasicEvents_none[type]?.Invoke();
        }
        else
        {
            Debug.Log($"\n{nameof(EventManager)}.{nameof(_dicBasicEvents_none)} has not {type}.\n Please Check.\n");
        }
    }

    public void InvokeEvent(EEventKey type, object param1)
    {
        if (_dicBasicEvents_one.ContainsKey(type))
        {
            _dicBasicEvents_one[type]?.Invoke(param1);
        }
        else
        {
            Debug.Log($"\n{nameof(EventManager)}.{nameof(_dicBasicEvents_one)} has not {type}.\n Please Check.\n");
        }
    }

    public void InvokeEvent(EEventKey type, object param1, object param2)
    {
        if (_dicBasicEvents_two.ContainsKey(type))
        {
            _dicBasicEvents_two[type]?.Invoke(param1, param2);
        }
        else
        {
            Debug.Log($"\n{nameof(EventManager)}.{nameof(_dicBasicEvents_two)} has not {type}.\n Please Check.\n");
        }
    }
    #endregion
}
