using UnityEngine;
using System.Collections;

namespace Editor.Config
{
    /// <summary>
    /// 工厂类，根据格式字符串生成相应的格式对象
    /// </summary>
    public class FormatFactory
    {
        public static FormatBase CreateFormat(string itemName, string formatName)
        {
            formatName = formatName.Trim();
            //自定义类型
            if (formatName == DOC_FORMAT_TYPE_CUSTOM)
            {
                return new FormatCustom(itemName);
            }
            if (formatName.StartsWith(DOC_FORMAT_TYPE_ARR))
            {
                return GetFormatArr(itemName, formatName);
            }
            else
            {
                return GetFormatBase(itemName, formatName);
            }
        }

        protected static FormatBase GetFormatArr(string itemName, string str)
        {
            string arrStart = DOC_FORMAT_TYPE_ARR + "(";
            string arrEnd = ")";

            int extCount = 0;

            //开始剥离
            while (true)
            {
                if (!str.StartsWith(arrStart) || !str.EndsWith(arrEnd)) break;

                str = str.Substring(arrStart.Length, str.Length - arrStart.Length - arrEnd.Length);
                extCount++;
            }

            if (extCount <= 0)
            {
                return null;
            }

            switch (str.Trim())
            {
                case DOC_FORMAT_TYPE_INT:
                    {
                        return new FormatArrInt(itemName, extCount);
                    }
                case DOC_FORMAT_TYPE_FLOAT:
                    {
                        return new FormatArrFloat(itemName, extCount);
                    }
                case DOC_FORMAT_TYPE_LONG:
                    {
                        return new FormatArrLong(itemName, extCount);
                    }
                case DOC_FORMAT_TYPE_STRING:
                    {
                        return new FormatArrString(itemName, extCount);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        protected static FormatBase GetFormatBase(string itemName, string str)
        {
            switch (str.Trim())
            {
                case DOC_FORMAT_TYPE_INT:
                    {
                        return new FormatInt(itemName);
                    }
                case DOC_FORMAT_TYPE_FLOAT:
                    {
                        return new FormatFloat(itemName);
                    }
                case DOC_FORMAT_TYPE_LONG:
                    {
                        return new FormatLong(itemName);
                    }
                case DOC_FORMAT_TYPE_STRING:
                    {
                        return new FormatString(itemName);
                    }
                default:
                    {
                        return null;
                    }
            }
        }






        //常量
        public const string DOC_FORMAT_ITEM_NAME = "Format";
        //格式定义
        public const string DOC_FORMAT_TYPE_INT = "int";
        public const string DOC_FORMAT_TYPE_FLOAT = "foat";
        public const string DOC_FORMAT_TYPE_LONG = "long";
        public const string DOC_FORMAT_TYPE_STRING = "string";
        public const string DOC_FORMAT_TYPE_ARR = "arr";
        public const string DOC_FORMAT_TYPE_CUSTOM = "custom";
    }

}

