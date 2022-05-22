using MiManchi.MiBaseClass;
using MiManchi.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiManchi
{
    namespace Data
    {
        public class CharacterDataItem : Dataitembase
        {
            [ReadOnly, SerializeField] List<CharacterData> _data = new List<CharacterData>();
            protected List<CharacterData> data { get => _data; set => _data = value; }

            public override List<object> GetData()
            {
                List<object> list = new List<object>();
                foreach (var item in data)
                {
                    list.Add(item);
                }
                return list;
            }

            public virtual void SetData(List<CharacterData> data)
            {
                this.data = data;
            }
        }
    }
}