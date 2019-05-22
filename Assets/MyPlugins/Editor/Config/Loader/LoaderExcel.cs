using UnityEngine;
using System.Collections;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;

namespace Editor.Config
{
    public class LoaderExcel : LoaderBase
    {
        public override LoaderData Loader(string path)
        {
            LoaderData ret = base.Loader(path);
            path = Application.dataPath + path;

            using (ExcelPackage package = new ExcelPackage(new FileStream(path, FileMode.Open)))
            {
                //只取第一页
                if(package.Workbook.Worksheets.Count < 1)
                {
                    return ret;
                }
                ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                {
                    Dictionary<string, string> formatDict = new Dictionary<string, string>();
                    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

                    //名字和列数的对应关系
                    Dictionary<int, string> colDict = new Dictionary<int, string>();
                    
                    //第二行开始，第一行是备注
                    int row = 2;
                    for(int col = 1; col <= sheet.Dimension.End.Column; col++)
                    {
                        string name = sheet.Cells[row, col].Value.ToString();
                        string format = sheet.Cells[row+1, col].Value.ToString();
                        if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(format))
                        {
                            CLog.LogErrorFormat("Load error: name or format null in row {0} col {1} file {3}", row, col, path);
                            return ret;
                        }
                        formatDict.Add(name, format);
                        colDict.Add(col, name);
                    }
                    row+=2;

                    for(;row <= sheet.Dimension.End.Row; row++)
                    {
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        list.Add(dict);
                        for (int col = 1; col <= formatDict.Count; col++)
                        {
                            dict.Add(colDict[col], sheet.Cells[row, col].Value.ToString());
                        }
                    }
                    ret.Format = formatDict;
                    ret.Data = list;
                    return ret;
                }
            }
        }
    }
}

