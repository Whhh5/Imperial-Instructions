using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;

public class BattleTest : MiBaseMonoBeHaviourClass
{
    bool active = false;
    public bool isActive { get => active; set => active = value; }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var type = GetType();
            var field = type.GetProperty("isActive");
            if (field != null)
            {
                var v = field.GetValue(this);
                Log(Color.blue, field.GetValue(this).ToString());
                field.SetValue(this, new object[] { true});
                Log(Color.blue, field.GetValue(this).ToString());
            }
        }
    }

    public void Takle()
    {
        Log(Color.blue, "Init");
    }
}
