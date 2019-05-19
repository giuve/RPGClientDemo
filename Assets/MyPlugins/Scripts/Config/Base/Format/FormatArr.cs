using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Config
{
    public abstract class FormatArr : FormatBase
    {
        public FormatArr(string itemName, int extCount):base(itemName)
        {
            if(extCount <= 0)
            {
                throw new System.Exception("FormatArr'extCount must be greater than 0!");
            }

            if(extCount > ARRAY_SEPARATOR.Length)
            {
                throw new System.Exception("Array separator count not enough!");
            }
            _extCount = extCount;
        }

        public override object Parse(string src)
        {
            return Parse(src, _extCount);
        }

        public IList Parse(string src, int extCount)
        {
            if(extCount >= 0)
            {
                string[] arrStr = src.Split(ARRAY_SEPARATOR[extCount]);
                if (extCount > 0)
                {
                    extCount--;
                    List<IList> list = new List<IList>();
                    foreach (string str in arrStr)
                    {
                        list.Add(Parse(str, extCount));
                    }
                    return list;
                }

                //最后一层，解析成相应格式
                IList finalList = GetFinalList();
                foreach (string str in arrStr)
                {
                    finalList.Add(ParseItem(str));
                }
                return finalList;
            }
            return null;
        }

        public override string ExportCs()
        {
            return string.Format(ExportCs(_extCount), getBaseType());
        }

        private string ExportCs(int extCount)
        {
            string str = "{0}";
            for (int i = 0; i < extCount; i++)
            {
                str = string.Format(str, "List<{0}>");
            }
            return str;
        }

        protected abstract string getBaseType();
 
        public override List<string> ExportCsParse()
        {
            List<string> list = new List<string>();
            list.Add(string.Format(TEMLATE_CS_PARSE_ARRAY_GET_ATTR, Name, _extCount, Name));
            list.AddRange(ExportCsParse(_extCount));
            return list;
        }

        private List<string> ExportCsParse(int extCount)
        {
            if (0 >= extCount) return new List<string>();

            List<string> list = new List<string>();
            string valueType = string.Format(ExportCs(extCount--), getBaseType());
            list.Add(string.Format(TEMLATE_CS_PARSE_ARRAY_DEFINITION, valueType, Name, extCount, valueType));
            //最外层list没有父list
            if (extCount != _extCount - 1)
            {
                list.Add(string.Format(TEMLATE_CS_PARSE_ARRAY_ADD_LIST, Name, extCount + 1, Name, extCount));
            }
            list.Add(string.Format(TEMLATE_CS_PARSE_ARRAY_SPLIT, Name, extCount, Name, extCount + 1, ARRAY_SEPARATOR[extCount]));
            list.Add(string.Format(TEMLATE_CS_PARSE_ARRAY_FOREACH, Name, extCount, Name, extCount));
            list.Add(ExporterCSharp.TEMPLATE_BRACKETS_HEAD);
            if(0 == extCount)
            {
                list.Add(GetParseCsConvert(extCount));
            }
            else
            {
                list.AddRange(ExportCsParse(extCount));
            }
            list.Add(ExporterCSharp.TEMPLATE_BRACKETS_TAIL);
            return list;
        }

        protected virtual string GetParseCsConvert(int extCount)
        {
            return string.Format(TEMLATE_CS_PARSE_ARRAY_CONVERT, Name, extCount, ExportCsXmlTo(), Name, extCount);
        }

        //获取最后一层List的格式
        protected abstract IList GetFinalList();
        protected abstract object ParseItem(string str);
        protected int _extCount;

        //数组分割符(4级应该够用了吧)-_-|||
        public static readonly char[] ARRAY_SEPARATOR = { ',', '|', '^', '#'};

        protected static readonly string TEMLATE_CS_PARSE_ARRAY_GET_ATTR = "string {0}Str{1} = item.GetAttribute(\"{2}\");";
        protected static readonly string TEMLATE_CS_PARSE_ARRAY_DEFINITION = "{0} {1}List{2} = new {3}();";
        protected static readonly string TEMLATE_CS_PARSE_ARRAY_ADD_LIST = "{0}List{1}.Add({2}List{3});";
        protected static readonly string TEMLATE_CS_PARSE_ARRAY_SPLIT = "string[] {0}Arr{1} = {2}Str{3}.Split('{4}');";
        protected static readonly string TEMLATE_CS_PARSE_ARRAY_FOREACH = "foreach (string {0}Str{1} in {2}Arr{3})";
        protected static readonly string TEMLATE_CS_PARSE_ARRAY_CONVERT = "{0}List{1}.Add(XmlConvert.{2}({3}Str{4}));";
        protected static readonly string TEMLATE_CS_PARSE_ARRAY_NO_CONVERT = "{0}List{1}.Add({2}Str{3});";
    }


    
}

