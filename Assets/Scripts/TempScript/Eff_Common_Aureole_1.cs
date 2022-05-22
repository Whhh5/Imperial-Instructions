using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi;

public class Eff_Common_Aureole_1 : MiObjPoolPublicParameter
{
    [SerializeField] ParticleSystem main;
    private void Update()
    {
        if (main.particleCount <= 0)
        {
            Destroy();
        }
    }
    public void Play()
    {
        main.time = 0;
        main.Clear();
        main.Play();
    }
    public void Stop()
    {
        main.time = 0;
        main.Stop();
    }
    public void Continue()
    {
        main.Play();
    }
    public void Pause()
    {
        main.Pause();
    }
    public override void Destroy()
    {
        base.Destroy();
    }
}
