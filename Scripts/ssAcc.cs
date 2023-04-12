using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZAccessor2<T> where T : Component
{
    public float X(T component)
    {
        return component.transform.position.x;
    }

    public float Y(T component)
    {
        return component.transform.position.y;
    }

    public float Z(T component)
    {
        return component.transform.position.z;
    }
}

public class ssAcc : MonoBehaviour
{
    private XYZAccessor2<Transform> accessor;

  
    void Start()
    {
        accessor = new XYZAccessor2<Transform>(); 
    }

 
    void Update()
    {
       
        float x = accessor.X(transform);
        float y = accessor.Y(transform);
        float z = accessor.Z(transform);

       
        Debug.Log("X: " + x + ", Y: " + y + ", Z: " + z);
    }
}
