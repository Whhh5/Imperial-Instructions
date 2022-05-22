using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace EditorTool
{
    public class ExcelTool
    {
        public static List<T> CreaterItemArrayWithExcel<T>(string filePath) where T : class , new()
        {
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum);
            List<T> Tarray = new List<T>();
            T TtypeClass = new T();
            Type Ttype;
            List<string> propertysInExcel = new List<string>();
            List<string> fieldsInTClass = new List<string>();
            //Get Type T Class In Property
            foreach (var item in TtypeClass.GetType().GetFields())
            {
                fieldsInTClass.Add(item.Name);
            }
            for (int i = 0; i < rowNum; i++)
            {
                if (collect[i][0] as string == "H")
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        propertysInExcel.Add(collect[i][j].ToString());
                    }
                    continue;
                }
                if (collect[i][0] as string == "#") continue;
                if(propertysInExcel.Count == 0)
                {
                    Debug.Log($"------ None Load Samething Property. Plase Check Table . Path : {filePath} ------");
                    return null;
                }
                TtypeClass = new T();
                Ttype = TtypeClass.GetType();

                for (int j = 0; j < columnNum; j++)
                {
                    if (collect[i][j].ToString() == string.Empty) continue;
                    if (fieldsInTClass.Contains(propertysInExcel[j]))
                    {
                        var tempStr = Ttype.GetField(propertysInExcel[j]);

                        if (collect[i][j] is double)
                        {
                            var propertyObj = uint.Parse(collect[i][j].ToString());
                            tempStr.SetValue(TtypeClass, propertyObj);
                        }
                        else
                        {
                            var propertyObj = collect[i][j].ToString();
                            tempStr.SetValue(TtypeClass, propertyObj);
                        }
                    }
                    else Debug.Log($"None Contains Property {propertysInExcel[j]}");
                }
                Tarray.Add(TtypeClass);
            }
            return Tarray;
        }
        static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelDataReader.AsDataSet();

            columnNum = result.Tables[0].Columns.Count;
            rowNum = result.Tables[0].Rows.Count;
            return result.Tables[0].Rows;
        }
    }
}