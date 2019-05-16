using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigAnalyzer
{

    public AnalysisResult Analysis(ConfigLoaderData data)
    {
        /// 查检是否有格式数据
        /// 每行数据，属性的数量是否匹配
        if(null == data) return AnalysisResult.NO_DATA;
        if (null == data.Format || data.Format.Count <= 0) return AnalysisResult.NO_FORMAT;
        if (null == data.Data || data.Data.Count <= 0) return AnalysisResult.NO_DATA;

        foreach (Dictionary<string, string> dict in data.Data)
        {
            if (dict.Count != data.Format.Count) return AnalysisResult.DATA_COUNT_NOT_MATCH;
        }

        //检查每一列格式是否符合条件,并产出格式字典
        Dictionary<string, string>.Enumerator iter = data.Format.GetEnumerator();
        Dictionary<string, FormatTypeData> formatTypeDict = new Dictionary<string, FormatTypeData>();
        while (iter.MoveNext())
        {
            FormatTypeData fData = GetFormatType(iter.Current.Value);
            if(fData.Type == FormatType.ERR || fData.BaseType == FormatBaseType.ERR)
            {
                CLog.LogErrorFormat("Analysis Error: file {0} item \"{1}\" format \"{2}\" error", data.path, iter.Current.Key, iter.Current.Value);
                break;
            }
            formatTypeDict.Add(iter.Current.Key, fData);
        }

        //检查每一行数据，否符合相应格式

        return AnalysisResult.SUCCESS;
    }
    
    protected FormatTypeData GetFormatType(string str)
    {
        str = str.Trim();
        //自定义类型
        if(str == DOC_FORMAT_TYPE_CUSTOM)
        {
            FormatTypeData data = new FormatTypeData();
        }
        if (str.StartsWith(DOC_FORMAT_TYPE_ARR))
        {
            return GetFormatArr(str);
        }
        else
        {
            return GetFormatDataBase(str);
        }
    }

    protected FormatTypeData GetFormatArr(string str)
    {
        string arrStart = DOC_FORMAT_TYPE_ARR + "(";
        string arrEnd = ")";
        
        int extCount = 0;
        
        //开始剥离
        while (true)
        {
            if (!str.StartsWith(arrStart) || !str.EndsWith(arrEnd)) break;

            str = str.Substring(arrStart.Length, str.Length - arrStart.Length - arrEnd.Length);
            extCount ++;
        }

        FormatTypeData data = new FormatTypeData();
        if(extCount <= 0)
        {
            data.Type = FormatType.ERR;
            data.BaseType = FormatBaseType.ERR;
            return data;
        }
        data.ExtCount = extCount;
        data.Type = FormatType.ARR;
        data.BaseType = GetFormatBase(str);
        return data;
    }

    /// <summary>
    /// 获取一个属性的基本类型
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected FormatTypeData GetFormatDataBase(string str)
    {
        FormatTypeData data = new FormatTypeData();
        data.Type = FormatType.NORMAL;
        data.BaseType = GetFormatBase(str);
        return data;
    }

    protected FormatBaseType GetFormatBase(string str)
    {
        switch (str.Trim())
        {
            case DOC_FORMAT_TYPE_INT:
                {
                    return FormatBaseType.INT;
                }
            case DOC_FORMAT_TYPE_FLOAT:
                {
                    return FormatBaseType.FLOAT;
                }
            case DOC_FORMAT_TYPE_LONG:
                {
                    return FormatBaseType.LONG;
                }
            case DOC_FORMAT_TYPE_STRING:
                {
                    return FormatBaseType.STRING;
                }
            default:
                {
                    return FormatBaseType.ERR;
                }
        }
    }

    //常量
    public const string DOC_FORMAT_ITEM_NAME      = "Format";
    //格式定义
    public const string DOC_FORMAT_TYPE_INT       = "int";
    public const string DOC_FORMAT_TYPE_FLOAT     = "foat";
    public const string DOC_FORMAT_TYPE_LONG      = "long";
    public const string DOC_FORMAT_TYPE_STRING    = "string";
    public const string DOC_FORMAT_TYPE_ARR       = "arr";
    public const string DOC_FORMAT_TYPE_CUSTOM    = "custom";
}

public enum AnalysisResult
{
    SUCCESS = 0,//成功
    NO_FORMAT,//没找到格式定义，或者定义不正确
    NO_DATA,//没找到数据，或者数据不正确
    DATA_COUNT_NOT_MATCH,//数据跟格式数量不匹配
}

public enum FormatType
{
    NORMAL,                 //基础类型
    ARR,                    //数组类型(包括数组内嵌)
    CUSTOM,                 //自定义类型
    ERR,                    //错误
}

public enum FormatBaseType
{
    NONE,                   //自定义类型无基础格式
    INT,                    //int型
    FLOAT,                  //float型
    LONG,                   //long型
    STRING,                 //string型
    ERR,                    //错误
}