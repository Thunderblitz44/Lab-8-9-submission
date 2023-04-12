using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class XYZAccessor<T> where T : Component
{
    public Vector3 GetPosition(T obj)
    {
        return obj.transform.position;
    }

    public void SetPosition(T obj, Vector3 position)
    {
        obj.transform.position = position;
    }
}
