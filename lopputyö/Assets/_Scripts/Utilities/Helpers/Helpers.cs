using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Helpers
{
    static Camera _camera;
    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    static List<Timer> timers = new List<Timer>();

    public static Vector3 GetMousePosition()
    {
        return Camera.ScreenToWorldPoint(Input.mousePosition);
    }


    public static void CreateAutoTimer(float time, Action end)
    {
        timers.Add(new Timer(time, end));
    }
    public static Timer CreateTimer(float time, Action end)
    {
        return new Timer(time, end);
    }

    public static int FindValue<T>(T _begin, Func<int,T> _getNewComparable, Func<T, T, bool> _returnComparision, int _iterationMax, int _iterationIncrement = 1)
    {
        int _minIndex = -1;
        for (int i = 0; i < _iterationMax; i += _iterationIncrement)
        {
            T _newComparable = _getNewComparable(i);
            if(_returnComparision(_newComparable, _begin))
            {
                _begin = _newComparable;
                _minIndex = i;
            }
        }
        return _minIndex;
    }
    public static int FindMin(float _begin, Func<int, float> _getNewComparable, int _iterationMax, int _iterationIncrement = 1)
    {
        return FindValue<float>(_begin, _getNewComparable, (x, y) => { return x < y; }, _iterationMax, _iterationIncrement);
    }
    public static int FindMax(float _begin, Func<int, float> _getNewComparable, int _iterationMax, int _iterationIncrement = 1)
    {
        return FindValue<float>(_begin, _getNewComparable, (x, y) => { return x > y; }, _iterationMax, _iterationIncrement);
    }

    public static void Tick(float deltaTime)
    {
        if (timers.Count > 0)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                if (timers[i].Count(deltaTime))
                {
                    timers.RemoveAt(i);
                }

            }
        }
    }

    public static void AddAutounsubDelegate(Func<Delegate> _action)
    {
        SceneManager.sceneUnloaded += (s1) => _action();
    }

    public static float Distance(this Vector2 _vector, Vector2 _pos1, Vector2 _pos2)
    {
        return Vector2.SqrMagnitude(_pos1 - _pos2);
    }

}
public class Timer
{
    float time;
    Action End;
    public Timer(float _time, Action _end)
    {
        time = _time;
        End = _end;
    }

    public bool Count(float tick)
    {
        time -= tick;
        if (time <= 0)
        {
            End?.Invoke();

            return true;
        }
        return false;
    }
    
}
