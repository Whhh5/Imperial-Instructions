using MiManchi.MiBaseClass;
using MiManchi.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiManchi
{
    namespace Data
    {
        public class SkillsDataItem: Dataitembase
        {
            [ReadOnly, SerializeField] List<SkillsData> _data = new List<SkillsData>();
            protected List<SkillsData> data { get => _data; set => _data = value; }
            public override List<object> GetData()
            {
                List<object> list = new List<object>();
                foreach (var item in data)
                {
                    list.Add(item);
                }
                return list;
            }

            public virtual void SetData(List<SkillsData> data)
            {
                this.data = data;
            }
        }
    }
}