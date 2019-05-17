using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Editor.Config;
using System;

namespace Editor.Config {
    public class Analyzer
    {

        public List<Dictionary<string, ItemData>> Analysis(LoaderData data)
        {
            /// 查检是否有格式数据
            /// 每行数据，属性的数量是否匹配
            if (null == data)
            {
                return null;
            }

            if (null == data.Format || data.Format.Count <= 0)
            {
                return null;
            }

            if (null == data.Data || data.Data.Count <= 0)
            {
                return null;
            }

            foreach (Dictionary<string, string> dict in data.Data)
            {
                if (dict.Count != data.Format.Count)
                {
                    return null;
                }
            }

            //检查每一列格式是否符合条件,并产出格式字典
            Dictionary<string, string>.Enumerator iter = data.Format.GetEnumerator();
            Dictionary<string, FormatBase> formatDict = new Dictionary<string, FormatBase>();
            while (iter.MoveNext())
            {
                FormatBase fData = FormatFactory.CreateFormat(iter.Current.Key, iter.Current.Value);
                if (null == fData)
                {
                    CLog.LogErrorFormat("Analysis Error: file {0} item \"{1}\" format \"{2}\" error", data.Path, iter.Current.Key, iter.Current.Value);
                    return null;
                }
                formatDict.Add(iter.Current.Key, fData);
            }

            //把每一个数据转换到ItemData中
            List<Dictionary<string, ItemData>> itemList = new List<Dictionary<string, ItemData>>();
            foreach (Dictionary<string, string> dict in data.Data)
            {
                Dictionary<string, ItemData> itemDict = new Dictionary<string, ItemData>();
                itemList.Add(itemDict);

                Dictionary<string, string>.Enumerator dataIter = dict.GetEnumerator();
                while (dataIter.MoveNext())
                {
                    string key = dataIter.Current.Key;
                    string value = dataIter.Current.Value;

                    if (!formatDict.ContainsKey(key))
                    {
                        CLog.LogErrorFormat("Analysis Error: item \"{0}\" and value \"{1}\" is not match format in file {2}", key, value, data.Path);
                        return null;
                    }

                    try
                    {
                        itemDict.Add(key, new ItemData(value, formatDict[key]));
                    }
                    catch (Exception ex)
                    {
                        CLog.LogErrorFormat("Analysis Error: item \"{0}\" and value \"{1}\" parse exception in file {2}, Exception: {3}", key, value, data.Path, ex.Message);
                        return null;
                    }
                }
            }

            return itemList;
        }


    }
}


