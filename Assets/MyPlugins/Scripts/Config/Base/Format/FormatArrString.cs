using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Editor.Config
{
    public class FormatArrString : FormatArr
    {
        public FormatArrString(string itemName, int extCount) : base(itemName, extCount)
        {
            
        }

        protected override IList GetFinalList()
        {
            return new List<string>();
        }

        protected override object ParseItem(string str)
        {
            return str;
        }

        public override string ExportCs()
        {
            return string.Format(base.ExportCs(), FormatFactory.DOC_FORMAT_TYPE_STRING);
        }
    }

}
