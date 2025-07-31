using UnityEngine;
using System.Collections.Generic;
using System;

public class Pool<T> where T : Component
{
    private Queue<T> _queue = new();
    private T _prefab;
    private Action<T> _created;
    private HashSet<T> _activeObjects = new();
    private int _totalCreatedCount;
    
    public int ActiveCount => _activeObjects.Count;
    public int TotalCreatedCount => _totalCreatedCount; 
    
    public Pool(T prefab, int initialSize, Action<T> created = null)
    {
        _prefab = prefab;
        _created = created;

        for (int i = 0; i < initialSize; i++)
        {
            T instance = UnityEngine.Object.Instantiate(_prefab);
            instance.gameObject.SetActive(false);
            _queue.Enqueue(instance);
            _totalCreatedCount++;
        }
    }

    public T GetObject()
    {
        T instance;
        
        if (_queue.Count > 0)
        {
            instance = _queue.Dequeue();
        }
        else
        {
            instance = UnityEngine.Object.Instantiate(_prefab);
            _totalCreatedCount++;
        }

        _created?.Invoke(instance);
        instance.gameObject.SetActive(true);
        _activeObjects.Add(instance);
        return instance;
    }

    public void ReturnObject(T instance)
    {
        instance.gameObject.SetActive(false);
        _activeObjects.Remove(instance);
        _queue.Enqueue(instance);
    }
}

