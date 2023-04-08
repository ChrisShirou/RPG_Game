using System.IO;
using UnityEngine;
using ExcelDataReader;
using System.Data;
using System.Collections.Generic;
using System;

public class ExcelManager : MonoBehaviour
{
    public List<int> LevelExp = new List<int>();
    public Dictionary<string, List<string>> ItemData = new Dictionary<string, List<string>>();

    string FilePath;
    string excelSheetName;
    // Start is called before the first frame update
    void Awake()
    {
        LoadexpExcel();
        LoadItemExcel();
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
    void LoadexpExcel()
    {
        FilePath = Application.dataPath + "/expList.xlsx";
        excelSheetName = "工作表1";

        var excelRowData = ReadExcel(FilePath, excelSheetName);

        for (int i = 0; i < excelRowData.Count; i++)
        {
            if (excelRowData[i][1].ToString() == "")
            { return; }
            LevelExp.Add(Int32.Parse(excelRowData[i][1].ToString()));
            //Debug.Log(LevelExp[i]);
        }
    }
    void LoadItemExcel()
    {
        FilePath = Application.dataPath + "/Item.xlsx";
        excelSheetName = "工作表1";

        var excelRowData = ReadExcel(FilePath, excelSheetName);

        string index;
        List<string> strs = new List<string>();
        for (int i = 0; i < excelRowData.Count; i++)
        {
            if (excelRowData[i][0].ToString() == "")
            { return; }
            index = excelRowData[i][0].ToString();
            for (int y = 1; y < 8; y++)
            {
                strs.Add(excelRowData[i][y].ToString());
            }
            ItemData.Add(index, strs);
        }
        //Debug.Log(ItemData[0][1]);
        //Debug.Log(ItemData["0001"][0]);
    }
}
