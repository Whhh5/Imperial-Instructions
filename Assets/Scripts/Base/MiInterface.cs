using MiManchi.MiBaseStruct;
using MiManchi.MiEnum;
using MiManchi.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiManchi
{
    namespace MiInterface
    {
        public interface IDataItemMothodBase
        {
            public List<object> GetData();
        }
        public interface IUIDialog
        {
            public Task ShowAsync(MiEnum.DialogMode mode);
            public Task HideAsync(MiEnum.DialogMode mode);
        }
        public interface MiIUIPointEvent : IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IDisposable 
        {
            
        }

        public interface Acticle
        {
            
        }

        public interface Valuable
        {
            public Task<ulong> GetId();
            public Task<ulong> GetCost();
        }

        public interface IUIPage : ICloneable
        {
            public Task Initialization();
            public Task ShowAsync();
            public Task Distroy();
        }
        public interface IStatusPattern
        {
            public uint GetState();
            public void SetState(uint state);
        }


        public interface IWeapon
        {
            public GameObject GetMain(GameObject par);
            public void Put();
        }
        public interface IObjPool
        {
            public void SettingOriginal(GameObject original);
            public void SettingId(ulong id);
            public void Destroy();
        }
        public interface ICommon_GameObject : ICloneable
        {
            public GameObject GetMain();
            public void Prepare();
            public void SetParameter(object[] value);
            public void Destroy();
        }
        public interface ICommon_Weapon : ICommon_GameObject
        {
            public void Active(object[] value);
        }
        public interface ICommon_Object : ICommon_GameObject
        {
            public void SettingId(ulong id);
            public ulong GetId();
            public CommonGameObjectInfo GetInfo();
            public ObjectType GetObjectType();
            public void AddBloodValue(float value);
            public void AddBSetBloodClick(Action<CommonGameObjectInfo> action);
            public void SetBlood(float value);
        }
        public interface IEffects : ICommon_GameObject, ICommon_Weapon
        {
            public void Play();
            public void Pause();
            public void Continue();
        }
        public interface IBuildingFacilities : ICommon_GameObject, ICommon_Weapon
        {
            public void OnMainCollisionEnter2D(Collision2D collision2D);
            public void OnMainCollisionExit2D(Collision2D collision2D);
            public void OnMainCollisionStay2D(Collision2D collision2D);
            public void OnMainTriggerEnter2D(Collider2D collider2D);
            public void OnMainTriggerExit2D(Collider2D collider2D);
            public void OnMainTriggerStay2D(Collider2D collider2D);
        }
    }
}