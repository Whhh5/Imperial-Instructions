using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public string strProperty = "str";
    public int intProperty = 852;
    public bool boolProperty = false;

    private void NonPublic()
    {
        Debug.Log($"{GetType()}  NonPublic: ");
    }
    public void Speak(string str)
    {
        Debug.Log($"{GetType()}  Speak: {str}");
    }
    public void Genericity<T>(T property)
    {
        Debug.Log($"{GetType()}  Genericity: T:{typeof(T)}  {property}");
    }
}

