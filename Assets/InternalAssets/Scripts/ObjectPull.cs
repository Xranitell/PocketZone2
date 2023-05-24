using System.Collections.Generic;
using UnityEngine;

public class ObjectPull<T>
{
    public readonly Queue<T> _pull = new Queue<T>();

    public void PushToPull(T pullableObject)
    {
         _pull.Enqueue(pullableObject);
    }

    public T GetFromPull()
    {
        var obj = _pull.Dequeue();
        return obj;
    }
}