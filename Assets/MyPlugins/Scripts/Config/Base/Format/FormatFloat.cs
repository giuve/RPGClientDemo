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
            return FormatFactory.DOC_FORMAT_TYPE_FLOAT;
        }

        public override object Parse(string src)
        {
            return Convert.ToSingle(src);
        }
    }
}


