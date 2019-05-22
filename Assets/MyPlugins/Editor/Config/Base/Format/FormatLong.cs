using UnityEngine;
using System.Collections;
using System;

namespace Editor.Config
{
    public class FormatLong : FormatBase
    {
        public FormatLong(string itemName) : base(itemName)
        {
        }
        public override object Parse(string src)
        {
            return Convert.ToInt64(src);
        }

        public override string ToFormat()
        {
            return FormatFactory.DOC_FORMAT_TYPE_LONG;
        }

        public override string ExportCs()
        {
            return ToFormat();
        }
        
        public override string ExportCsXmlTo()
        {
            return "ToInt64";
        }
    }
}

