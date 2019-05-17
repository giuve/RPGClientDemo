using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Editor.Config
{
    public class ExporterBase
    {
        public ExporterBase(string path, List<Dictionary<string, ItemData>> data)
        {
            _path = path;
            _data = data;
        }

        public virtual void Export()
        {
            SaveFile();
        }
        public void SaveFile()
        {
            if(File.Exists(_path))
            {
                File.Delete(_path);
            }
            StreamWriter sw = new StreamWriter(_path);
            foreach(string str in _fileStringList)
            {
                sw.WriteLine(str);
            }
            sw.Flush();
            sw.Close();
        }

        protected bool IsHaveArr()
        {
            if (null == _data || _data.Count <= 0) return false;
            Dictionary<string, ItemData>.Enumerator iter = _data[0].GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.Value.IsArrayType()) return true;
            }
            return false;
        }

        protected string _path;
        protected List<Dictionary<string, ItemData>> _data;

        //文件结尾，比如namespace、class等会有与之配对的结尾符号
        protected Stack<string> _tailStack = new Stack<string>();
        protected List<string> _fileStringList = new List<string>();
    }
}
