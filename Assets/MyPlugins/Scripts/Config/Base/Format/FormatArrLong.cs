using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Editor.Config
{
    public class FormatArrLong : FormatArr
    {
        public FormatArrLong(string itemName, int extCount) : base(itemName, extCount)
        {
        }

        protected override IList GetFinalList()
        {
            return new List<long>();
        }

        public override string ExportCs()
        {
            return string.Format(base.ExportCs(), FormatFactory.DOC_FORMAT_TYPE_LONG);
        }

        protected override object ParseItem(string str)
        {
            return Convert.ToInt64(str);
        }
    }
}
