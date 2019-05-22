using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
namespace Editor.Config
{
    public class ExporterExcel : ExporterBase
    {
        public ExporterExcel(AnalyzerData aData) : base(aData)
        {
        }

        public override void Export()
        {

            if (null == _data || _data.Data.Count <= 0)
            {
                CLog.LogErrorFormat("Export error: no data in path {0}", _data.SrcPath);
                return;
            }

            
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Sheet1");
                int row = 2;//第一行是备注，第二行是名字
                Dictionary<string, FormatBase>.Enumerator iter = _data.Format.GetEnumerator();
                //要把列数和相应数据对应起来
                Dictionary<string, int> columnDict = new Dictionary<string, int>();
                int column = 1;
                while (iter.MoveNext())
                {
                    columnDict.Add(iter.Current.Key, column);
                    sheet.Cells[row, column].Value = iter.Current.Key;
                    sheet.Cells[row + 1, column++].Value = iter.Current.Value.ToFormat();
                }
                row++;
                foreach (Dictionary<string, ItemData> dict in _data.Data)
                {
                    row++;
                    Dictionary<string, ItemData>.Enumerator itemIter = dict.GetEnumerator();
                    {
                        while (itemIter.MoveNext())
                        {
                            if (!columnDict.ContainsKey(itemIter.Current.Key))
                            {
                                CLog.LogErrorFormat("Export error: The data not format \"{0}\" in file {1}", itemIter.Current.Key, _data.SrcPath);
                                continue;
                            }
                            sheet.Cells[row, columnDict[itemIter.Current.Key]].Value = itemIter.Current.Value.SrcString;
                        }
                    }
                }
                if (File.Exists(_data.TarPath))
                {
                    File.Delete(_data.TarPath);
                }
                using (Stream stream = new FileStream(_data.TarPath, FileMode.Create))
                {
                    package.SaveAs(stream);
                }
            }
        }
    }
}

