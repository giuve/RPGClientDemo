using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Editor.Config {
    public class FormatArrFloat : FormatArr
    {
        public FormatArrFloat(string itemName, int extCount) : base(itemName, extCount)
        {
        }
        
        protected override string getBaseType()
        {
            return FormatFactory.DOC_FORMAT_TYPE_FLOAT;
        }

        public override string ExportCsXmlTo()
        {
            return "ToSingle";
        }

        protected override IList GetFinalList()
        {
            return new List<float>();
        }

        protected override object ParseItem(string str)
        {
            return Convert.ToSingle(str);
        }
    }
}

