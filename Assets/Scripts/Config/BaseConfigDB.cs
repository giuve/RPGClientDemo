using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Game.Config
{
    public class BaseConfigDB
    {
        protected static Dictionary<int, T> Init<T>(string path)where T : BaseConfig, new()
        {
            Dictionary<int, T> _dict = new Dictionary<int, T>();
            
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
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
                if (null == itemName && item.Name != "Format")
                {
                    itemName = item.Name;
                }
                //如果XML文件中，有其他名字，警告并跳过
                if (null != itemName && itemName != item.Name)
                {
                    CLog.LogWarningFormat("Config loader warning: path {0} Multiple item name \"{1}\"", path, item.Name);
                    continue;
                }

                T t = new T();
                t.Parse(item);
                _dict.Add(t.ID, t);
            }

            return _dict;
        }
    }
}

