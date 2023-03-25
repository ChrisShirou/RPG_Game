using System.IO;
using UnityEngine;
using ExcelDataReader;
using System.Data;
using System.Collections.Generic;
using System;

public class ExcelManager : MonoBehaviour
{
    public List<int> LevelExp = new List<int>();
    // Start is called before the first frame update
    void Awake()
    {
        string FilePath = Application.dataPath + "/expList.xlsx";
        string excelSheetName = "工作表1";

        var excelRowData = ReadExcel(FilePath, excelSheetName);

        for (int i = 0; i < excelRowData.Count; i++)
        {
            if (excelRowData[i][1].ToString() == "")
            { return; }
            LevelExp.Add(Int32.Parse(excelRowData[i][1].ToString()));
            Debug.Log(LevelExp[i]);
        }
    }
    DataRowCollection ReadExcel(string excelPath, string excelSheet)
    {
        using (FileStream fileStream = File.Open(excelPath, FileMode.Open, FileAccess.Read))
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

            var result = excelReader.AsDataSet();

            return result.Tables[excelSheet].Rows;
        }
    }
}
