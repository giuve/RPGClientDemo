using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Editor.Config
{
    public class FormatString : FormatBase
    {
        public FormatString(string itemName) : base(itemName)
        {
        }

        public override string ToFormat()
        {
            return FormatFactory.DOC_FORMAT_TYPE_STRING;
        }

        public override string ExportCs()
        {
            return ToFormat();
        }

        public override object Parse(string src)
        {
            return src;
        }
        public override List<string> ExportCsParse()
        {
            List<string> list = new List<string>();
            list.Add(string.Format("_{0} = item.GetAttribute(\"{1}\");", Name, Name));
            return list;
        }
    }

}
