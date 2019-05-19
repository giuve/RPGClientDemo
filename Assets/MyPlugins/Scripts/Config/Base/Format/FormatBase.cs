using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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

        public virtual List<string> ExportCsParse()
        {
            List<string> list = new List<string>();
            list.Add(string.Format(TEMLATE_CS_PARSE_ITEM, Name, ExportCsXmlTo(), Name));
            return list;
        }
        public virtual string ExportCsXmlTo()
        {
            return "";
        }

        public string Name { set; get; }

        protected static readonly string TEMLATE_CS_PARSE_ITEM = "_{0} = XmlConvert.{1}(item.GetAttribute(\"{2}\"));";
    }
}

