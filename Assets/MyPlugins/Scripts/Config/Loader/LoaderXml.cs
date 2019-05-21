using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using Editor.Config;

namespace Editor.Config
{
    public class LoaderXml : LoaderBase
    {

        public override LoaderData Loader(string path)
        {
            path = Application.dataPath + path;
            LoaderData ret = base.Loader(path);

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            Dictionary<string, string> formatDict = new Dictionary<string, string>();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            IEnumerator iter = xml.GetEnumerator();

            string itemName = null;

            while (iter.MoveNext())
            {


                XmlElement item = iter.Current as XmlElement;
                if (null == item) continue;

                //这里可能是根节点
                if (!item.HasAttributes && null == itemName)
                {
                    iter = item.GetEnumerator();
                    continue;
                }

                //只取第一行数据的名字
                if (null == itemName && item.Name != FormatFactory.DOC_FORMAT_ITEM_NAME)
                {
                    itemName = item.Name;
                }
                //如果XML文件中，有其他名字，警告并跳过
                if (null != itemName && itemName != item.Name)
                {
                    CLog.LogWarningFormat("loader warning: path {0} Multiple item name \"{1}\"", path, item.Name);
                    continue;
                }

                Dictionary<string, string> dict = new Dictionary<string, string>();

                if (item.Name != FormatFactory.DOC_FORMAT_ITEM_NAME)
                {
                    list.Add(dict);
                }

                foreach (XmlAttribute atr in item.Attributes)
                {
                    if (item.Name == FormatFactory.DOC_FORMAT_ITEM_NAME)
                    {
                        formatDict.Add(atr.Name, atr.Value);
                        continue;
                    }

                    dict.Add(atr.Name, atr.Value);
                }
            }

            ret.Format = formatDict;
            ret.Data = list;
            return ret;
        }
    }

}
