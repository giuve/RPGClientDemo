using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Editor.Config
{
    public class FormatArrInt : FormatArr
    {
        public FormatArrInt(string itemName, int extCount) : base(itemName, extCount)
        {
        }

        public override string ExportCs()
        {
            return string.Format(base.ExportCs(), FormatFactory.DOC_FORMAT_TYPE_INT);
        }

        protected override IList GetFinalList()
        {
            return new List<int>();
        }

        protected override object ParseItem(string str)
        {
            return Convert.ToInt32(str);
        }
    }
}
