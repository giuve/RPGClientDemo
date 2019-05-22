using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Editor.Config
{
    public abstract class ExporterBase
    {
        public ExporterBase(AnalyzerData aData)
        {
            _data = aData;
        }

        public abstract void Export();
   
        protected bool IsHaveArr()
        {
            if (null == _data || null == _data.Format) return false;
            Dictionary<string, FormatBase>.Enumerator iter = _data.Format.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.Value is FormatArr) return true;
            }
            return false;
        }

        protected AnalyzerData _data;

        //文件结尾，比如namespace、class等会有与之配对的结尾符号
        protected Stack<string> _tailStack = new Stack<string>();
        protected List<string> _fileStringList = new List<string>();
    }
}
