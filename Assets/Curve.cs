using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;
using System;
using UnityEditor.Events;
using UnityEngine.EventSystems;

public class Curve : MonoBehaviour , IDragHandler
{
    TT tt = new TT();
    Func<Task> o;

    int? a = true ? 1 : 2;

    [SerializeField] AnimationCurve animationCurve;
    // Start is called before the first frame update

    static bool isTo = false;
    int sum = isTo == true ? 1 : 3;

    public delegate Task<string> del();
    //public delegate TResult Func2<out TResult>();
    void Start()
    {
        isTo = true;
        tt.AddListener(C);
        UnityEventTools.AddPersistentListener(tt);
        tt.RemoveListener(E);
        Debug.Log(tt.GetPersistentEventCount()+"  "+ (UnityEventBase.GetValidMethodInfo(this,"C",new Type[2] { typeof(string),typeof(int) }) != null) + (TT.GetValidMethodInfo(tt, "C", new Type[2] { typeof(string), typeof(char) }) != null));


        //o = async () => { await A("2"); };
        //o();
        StartObjectAsync(async () => await A("2"));
    }

    public void StartObjectAsync(Func<Task> handler)
    {
        handler.Invoke();
        Debug.Log(1);
    }

    async Task A(string str)
    {
        await B();
        str += await D("D");
        Debug.Log(3);
        Debug.Log(str);
        
    }

    async Task B()
    {
        //Func<Task> handler = null;
        //await handler();
    }
    async Task<string> D(string str)
    {
        //Func<Task> handler = null;
        //await handler();
        return str;
    }

    void C(string str,int number = 0)
    {
        Debug.Log($"{str} is {number}");
    }

    void E(string str, int number = 0)
    {
        Debug.Log($"{str} is {number}");
    }

    private void OnMouseDown()
    {
        Debug.Log(animationCurve.Evaluate(0.5f));
        if (tt == null)
        {
            Debug.Log($"{nameof(tt)} is Null");
        }
        else
        {
            Debug.Log(tt.GetPersistentEventCount());
        }
        tt.Invoke("222",2);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0f, 0f, 100f, 100f), "Hellow");
        GUI.Label(new Rect(150, 100f, 100f, 100f), "Hellow");
    }
    private void Reset()
    {
        Debug.Log("Curve");
        //user this
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log(pause);
        //player is playing
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Quit");
    }
    private void OnDrawGizmos()
    {
        //draw line
        Gizmos.DrawWireSphere(transform.position, 2.0f);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //give image end dispose
    }

    private void OnBecameVisible()
    {
        Debug.Log("eye look");
        //look eye == true
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}