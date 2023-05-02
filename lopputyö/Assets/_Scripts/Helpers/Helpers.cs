using System;
using System.Collections.Generic;
using UnityEngine;



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
