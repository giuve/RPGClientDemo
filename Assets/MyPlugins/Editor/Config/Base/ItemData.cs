using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Editor.Config
{
    public class ItemData
    {
        public ItemData(string src, FormatBase format)
        {
            _srcString = src;
            _format = format;
            Parse();
        }

        protected void Parse()
        {
            if (null == _format) return;
            _data = _format.Parse(_srcString);
        }
     
        protected string _srcString;
        protected object _data;
        protected FormatBase _format;
        public FormatBase Format { get { return _format; } }
        public object Data { get { return _data; } }
        public string SrcString { get { return _srcString; } }
    }
}

