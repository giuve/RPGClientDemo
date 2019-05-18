using UnityEngine;
using System.Collections;
using System;

namespace Editor.Config
{
    public abstract class FormatBase
    {
        public FormatBase(string itemName)
        {
            Name = itemName;
        }

        public abstract object Parse(string src);

        public abstract string ExportCs();
        public abstract string ExportCsParse();

        public string Name { set; get; }

        protected static readonly string TEMLATE_CS_PARSE_ITEM = "_{0} = XmlConvert.{1}(item.GetAttribute(\"{2}\"));";
    }
}

