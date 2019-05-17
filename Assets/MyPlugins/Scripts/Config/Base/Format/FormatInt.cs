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

        public override string ExportCs()
        {
            return FormatFactory.DOC_FORMAT_TYPE_INT;
        }

        public override object Parse(string src)
        {
            return Convert.ToInt32(src);
        }
    }
}

