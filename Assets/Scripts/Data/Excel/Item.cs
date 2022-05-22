using MiManchi.MiInterface;

namespace MiManchi
{
    namespace Data
    {
        /// <summary>
        /// ClassName => Config.Common.ExcelDataNamePath.property
        /// </summary>
        [System.Serializable]
        public class CharacterData
        {
            public ulong id;
            public string name;
            public ulong price;
        }
        /// <summary>
        /// ClassName => Config.Common.ExcelDataNamePath.property
        /// </summary>
        [System.Serializable]
        public class IconData
        {
            public ulong id;
            public string name;
            public ulong price;
        }
        [System.Serializable]
        public class SkillsData
        {
            public ulong id;
            public string name;
            public ulong price;
            public float attack;
            public string prefabName;
        }
    }
}