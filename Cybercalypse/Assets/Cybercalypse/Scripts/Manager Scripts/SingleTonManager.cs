using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class SingleTonManager<T> : MonoBehaviour where T : SingleTonManager<T>
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : SingleTon 패턴의 템플릿 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    private static T _instance;
    public static T instance
    {
        get
        {
            return _instance;
        }
        private set
        {

        }
    }

    protected virtual void Awake()
    {
        if(Equals(instance,null))
        {
            _instance = (T)this;
            Debug.Log("There active Instance : " + typeof(T));
        }
        else if(!Equals(instance,null))
        {
            _instance = (T)this;
            Debug.Log("There active Reload Instance " + typeof(T));
        }
        else
        {
            Debug.Log("There dose not active Instance " + typeof(T));
        }
    }
}