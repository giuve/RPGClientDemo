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

        public override string ExportCs()
        {
            return FormatFactory.DOC_FORMAT_TYPE_LONG;
        }

        public override object Parse(string src)
        {
            return Convert.ToInt64(src);
        }
    }
}

