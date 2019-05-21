using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Editor.Config
{
    public abstract class ExporterBase
    {
        public ExporterBase(string srcPath, string tarPath, List<Dictionary<string, ItemData>> data)
        {
            _srcPath = srcPath;
            _tarPath = tarPath;
            _data = data;
        }

        public abstract void Export();
   
        protected void SaveFile()
        {
            SaveFile(_tarPath);
        }

        protected void SaveFile(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            StreamWriter sw = new StreamWriter(path);
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

        protected string _srcPath;
        protected string _tarPath;
        protected List<Dictionary<string, ItemData>> _data;

        //文件结尾，比如namespace、class等会有与之配对的结尾符号
        protected Stack<string> _tailStack = new Stack<string>();
        protected List<string> _fileStringList = new List<string>();
    }
}
