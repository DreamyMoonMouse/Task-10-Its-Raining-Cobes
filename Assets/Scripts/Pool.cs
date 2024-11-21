using UnityEngine;
using System.Collections.Generic;

public class Pool<T> where T : Component
{
    private Queue<T> _poolQueue;
    private T _prefab;
    
    public Pool(T prefab, int initialSize)
    {
        _prefab = prefab;
        _poolQueue = new Queue<T>();

        for (int i = 0; i < initialSize; i++)
        {
            T instance = Object.Instantiate(prefab);
            instance.gameObject.SetActive(false);
            _poolQueue.Enqueue(instance);
        }
    }

    public T GetObject()
    {
        if (_poolQueue.Count > 0)
        {
            T instance = _poolQueue.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            T instance = Object.Instantiate(_prefab);
            return instance;
        }
    }

    public void ReturnObject(T instance)
    {
        instance.gameObject.SetActive(false);
        _poolQueue.Enqueue(instance);
    }
}

