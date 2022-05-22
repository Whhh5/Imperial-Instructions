using UnityEngine;
namespace Config
{
    public class CommonPath
    {
        public static readonly string ExcelsFolderPath = Application.dataPath + "/Excel";
        public static readonly string AssetPath = "Assets/Resources/DataAssets/Localize/";
        public static readonly string ExcelToolPath = "EditorTool.ExcelTool";
        public static readonly string DataNameSpacePath = "Data";

        public static readonly string ExcelDataNameSpacePath = "Config.Excel.ExcelDataNameSpace";
        public static readonly string ExcelDataNamePath = "Config.Excel.ExcelDataName";
        public static readonly string ItemString = "Item";
    }
    namespace Excel
    {
        public class ExcelDataNameSpace
        {
            public static readonly string Localize = "Localize";
            public static readonly string Master = "Master";
        }
        public class ExcelDataName
        {
            public static readonly string LocalizeCharacterDate = "CharacterData";
            public static readonly string LocalizeIconData = "IconData";
            public static readonly string LcalizeAttackData = "AttackData";
            public static readonly string LcalizeSkillsData = "SkillsData";
        }
    }
}