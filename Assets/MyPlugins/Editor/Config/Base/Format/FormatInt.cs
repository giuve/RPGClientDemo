using UnityEngine;
using System.Collections;
using System;

namespace Editor.Config
{
    public class FormatInt : FormatBase
    {
        public FormatInt(string itemName) : base(itemName)
        {
        }
        public override object Parse(string src)
        {
            return Convert.ToInt32(src);
        }
        public override string ToFormat()
        {
            return FormatFactory.DOC_FORMAT_TYPE_INT;
        }
        public override string ExportCs()
        {
            return ToFormat();
        }
        
        public override string ExportCsXmlTo()
        {
            return "ToInt32";
        }
    }
}

