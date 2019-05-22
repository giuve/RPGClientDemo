using UnityEngine;
using System.Collections;
using System;

namespace Editor.Config {
    public class FormatFloat : FormatBase
    {
        public FormatFloat(string itemName) : base(itemName)
        {

        }

        public override string ExportCs()
        {
            return ToFormat();
        }

        public override object Parse(string src)
        {
            return Convert.ToSingle(src);
        }

        public override string ExportCsXmlTo()
        {
            return "ToSingle";
        }

        public override string ToFormat()
        {
            return FormatFactory.DOC_FORMAT_TYPE_FLOAT;
        }
    }
}


