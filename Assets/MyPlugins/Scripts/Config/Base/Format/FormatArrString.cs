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

        protected override string getBaseType()
        {
            return FormatFactory.DOC_FORMAT_TYPE_STRING;
        }

        public override string ExportCsXmlTo()
        {
            return "ToString";
        }

        protected override string GetParseCsConvert(int extCount)
        {
            return string.Format(TEMLATE_CS_PARSE_ARRAY_NO_CONVERT, Name, extCount, Name, extCount);
        }
    }

}
