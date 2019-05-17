using UnityEngine;
using System.Collections;
using System;

namespace Editor.Config
{
    public class FormatString : FormatBase
    {
        public FormatString(string itemName) : base(itemName)
        {
        }

        public override string ExportCs()
        {
            return FormatFactory.DOC_FORMAT_TYPE_STRING;
        }

        public override object Parse(string src)
        {
            return src;
        }
    }

}
