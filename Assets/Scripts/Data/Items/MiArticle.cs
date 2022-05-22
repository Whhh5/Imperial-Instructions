using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiArticle
{
    public ulong id;
    public string title;
    public ulong price;

    public MiArticle()
    {

    }
    public MiArticle(ulong id,string title,ulong price)
    {
        this.id = id;
        this.title = title;
        this.price = price;
    }
}
