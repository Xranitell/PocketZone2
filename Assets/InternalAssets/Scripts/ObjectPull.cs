using System.Collections.Generic;
using UnityEngine;

public class ObjectPull<T>
{
    public readonly List<T> Pull = new List<T>();
    public int Count => Pull.Count;
    
    public void PushToPull(T pullableObject)
    {
         Pull.Add(pullableObject);
    }

    public T GetFromPull()
    {
        var obj = Pull[0];
        Pull.RemoveAt(0);
        return obj;
    }
    public T GetFromPull(T objectType)
    {
        var obj = Pull.Find(x=> (objectType as GameObject)?.name == (x as GameObject)?.name);
        Pull.RemoveAt(0);
        return obj;
    }
}