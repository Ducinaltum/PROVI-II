using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();

    public static void RegisterService<T>(T service)
    {
        var type = typeof(T);

        if (_services.ContainsKey(type))
            _services[type] = service;
        else
            _services.Add(type, service);
    }

    public static bool TryGetService<T>(out T service)
    {
        if (_services.TryGetValue(typeof(T), out var obj))
        {
            service = (T)obj;
            return true;
        }
        service = default;
        return false;
    }

    public static void UnregisterService<T>()
    {
        _services.Remove(typeof(T));
    }
}
