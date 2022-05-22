using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Config;
using System.Collections.Generic;

namespace EditorTool
{
    public class ExcelBuild : Editor
    {
        [MenuItem("CustomEditor/CreateItemAsset")]
        public static void CreaterItemAsset()
        {
            Type excelDataNameType = Type.GetType(CommonPath.ExcelDataNamePath);
            Type excelDataNameSpaceType = Type.GetType(CommonPath.ExcelDataNameSpacePath);
            object excelDataNameTypeObj = Activator.CreateInstance(excelDataNameType);
            object excelDataNameSpaceTypeObj = Activator.CreateInstance(excelDataNameSpaceType);
            List<string> commonBasic;
            List<string> excelDataNameSpaceString = new List<string>();
            List<Type> characterDataType;
            foreach (var item in excelDataNameSpaceType.GetFields())
            {
                excelDataNameSpaceString.Add(item.GetValue(excelDataNameSpaceTypeObj).ToString());
            }
            for (int i = 0; i < excelDataNameSpaceString.Count; i++)
            {
                commonBasic = new List<string>();
                characterDataType = new List<Type>();
                foreach (var item in excelDataNameType.GetFields())
                {
                    commonBasic.Add($"{excelDataNameSpaceString[i]}{item.GetValue(excelDataNameTypeObj).ToString()}");
                    characterDataType.Add(Type.GetType(CommonPath.DataNameSpacePath + "." + excelDataNameSpaceString[i] + "." + item.GetValue(excelDataNameTypeObj.ToString()).ToString()));
                }
                for (int j = 0; j < commonBasic.Count; j++)
                {
                    Type itemType = Type.GetType(characterDataType[j] + CommonPath.ItemString);
                    if(itemType == null)
                    {
                        Debug.Log($"------- Absence Class  - {excelDataNameSpaceString[i]} - {CommonPath.DataNameSpacePath + "." + excelDataNameSpaceString[i] + "." + excelDataNameType.GetFields()[j].GetValue(excelDataNameTypeObj.ToString())} -------");
                        continue;
                    }
                    object itemTypeObj = Activator.CreateInstance(itemType, true);
                    Type excelToolType = Type.GetType(CommonPath.ExcelToolPath);
                    object excelToolTypeObj = Activator.CreateInstance(excelToolType, true);
                    var excelData = excelToolType.GetMethod("CreaterItemArrayWithExcel", bindingAttr: BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static)
                        .MakeGenericMethod(characterDataType[j])
                        .Invoke(excelToolTypeObj, new object[] { CommonPath.ExcelsFolderPath + $"/{commonBasic[j]}.xlsx" });
                    if (excelData == null)
                    {
                        Debug.Log($" CS : ExcelBuild  -  Data Is None  -  Path : {CommonPath.ExcelsFolderPath}" + $"/{commonBasic[j]}.xlsx");
                        return;
                    }
                    itemType.GetMethod("SetData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public)
                        .Invoke(itemTypeObj, new object[] {excelData
                        });
                    if (!Directory.Exists(CommonPath.AssetPath))
                    {
                        Directory.CreateDirectory(CommonPath.AssetPath);
                    }

                    string assetPath = string.Format("{0}{1}.asset", CommonPath.AssetPath, commonBasic[j]);

                    AssetDatabase.CreateAsset((UnityEngine.Object)itemTypeObj, assetPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    Debug.Log($"Create Item {commonBasic[j]}");
                }
            }
        }
    }
}
