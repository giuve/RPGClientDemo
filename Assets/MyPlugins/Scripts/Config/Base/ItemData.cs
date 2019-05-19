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
            _format.Parse(_srcString);
        }

        public KeyValuePair<string, string> ExportCs()
        {
            string key = string.Format(TEMPLATE_ATTRIBUTE_NORMAL, _format.ExportCs(), _format.Name);
            string value = string.Format(TEMPLATE_ATTRIBUTE_NORMAL_GET, _format.ExportCs(), _format.Name, _format.Name);
            return new KeyValuePair<string, string>(key, value);
        }

        public List<string> ExportCsParse()
        {
            return _format.ExportCsParse();
        }

        public bool IsArrayType()
        {
            return _format is FormatArr;
        }

        protected string _srcString;
        protected object _data;
        protected FormatBase _format;
        public FormatBase Format { get { return _format; } }

        private static readonly string TEMPLATE_ATTRIBUTE_NORMAL = "private {0} _{1};";
        private static readonly string TEMPLATE_ATTRIBUTE_NORMAL_GET = "public {0} {1} {{ get {{ return _{2}; }} }}";
    }
}

