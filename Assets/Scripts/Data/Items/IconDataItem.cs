using MiManchi.MiBaseClass;
using MiManchi.MiInterface;
using MiManchi.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiManchi
{
    namespace Data
    {
        public class IconDataItem : Dataitembase
        {
            [ReadOnly, SerializeField] List<IconData> _data = new List<IconData>();
            protected List<IconData> data { get => _data; set => _data = value; }
            public override List<object> GetData()
            {
                List<object> list = new List<object>();
                foreach (var item in data)
                {
                    list.Add(item);
                }
                return list;
            }

            public virtual void SetData(List<IconData> data)
            {
                this.data = data;
            }
        }
    }
}
