using MiManchi.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiInterface;

public class Buttle : MiManchi.MiBaseClass.MiBaseMonoBeHaviourClass, IWeapon
{
    [SerializeField,ReadOnly] GameObject primitive;
    [SerializeField] GameObject main;
    [SerializeField] List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public GameObject GetMain(GameObject primitive)
    {
        //foreach (var pars in particleSystems)
        //{
        //    pars.Play();
        //}
        this.primitive = primitive;
        return main;
    }
    public void Put()
    {
        //foreach (var pars in particleSystems)
        //{
        //    pars.Stop();
        //}
        CommonManager.Instance.prefab.Put(primitive, gameObject);
    }
}
