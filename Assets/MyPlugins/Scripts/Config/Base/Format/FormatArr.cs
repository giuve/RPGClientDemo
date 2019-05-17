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
            string str = "{0}";
            for(int i = 0; i < _extCount; i++)
            {
                str = string.Format(str, "List<{0}>");
            }
            return str;
        }

        //获取最后一层List的格式
        protected abstract IList GetFinalList();
        protected abstract object ParseItem(string str);
        protected int _extCount;

        //数组分割符(4级应该够用了吧)-_-|||
        public static readonly char[] ARRAY_SEPARATOR = { ',', '|', '^', '#'};

        
    }


    
}

