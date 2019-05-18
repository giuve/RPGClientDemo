using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Editor.Config
{
    public class ExporterCSharp : ExporterBase
    {
        public ExporterCSharp(string path, List<Dictionary<string, ItemData>> data) 
            :base(path, data)
        {

        }

        public override void Export()
        {
            if(null == _data || _data.Count <= 0)
            {
                CLog.LogErrorFormat("Export error: no data in path {0}", _path);
                return;
            }
            _fileStringList.Clear();

            //引用(using)
            _fileStringList.Add(TEMPLATE_USING_XML);
            if (IsHaveArr())
            {
                _fileStringList.Add(TEMPLATE_USING_COLLECTIONS);
            }

            //命名空间
            _fileStringList.Add(TEMPLATE_NAMESPACE);
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _tailStack.Push(TEMPLATE_BRACKETS_TAIL);

            //类名
            _fileStringList.Add(string.Format(TEMPLATE_CLASS_NAME, Path.GetFileNameWithoutExtension(_path)));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _tailStack.Push(TEMPLATE_BRACKETS_TAIL);

            //构造方法和解析方法
            ExportParseMethod();

            //各字段,只取第一行数据进行生成即可
            Dictionary<string, ItemData>.Enumerator iter = _data[0].GetEnumerator();
            while (iter.MoveNext())
            {
                //导出的key是private字段，value是public的只读字段
                KeyValuePair<string, string> pair = iter.Current.Value.ExportCs();
                _fileStringList.Add(pair.Key);
                _fileStringList.Add(pair.Value);
            }
            
            //收尾
            while (_tailStack.Count > 0)
            {
                _fileStringList.Add(_tailStack.Pop());
            }

            //保存到文件上 
            base.Export();
        }

        public void ExportParseMethod()
        {
            //生成构造方法
            _fileStringList.Add(string.Format(TEMPLATE_CONSTRUCTION_METHOD, Path.GetFileNameWithoutExtension(_path)));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _fileStringList.Add(TEMPLATE_CALL_PARSE_METHOD);
            _fileStringList.Add(TEMPLATE_BRACKETS_TAIL);

            //生成解析函数
            _fileStringList.Add(TEMPLATE_PARSE_METHOD);
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);

            Dictionary<string, ItemData>.Enumerator iter = _data[0].GetEnumerator();
            while (iter.MoveNext())
            {
                _fileStringList.Add(iter.Current.Value.ExportCsParse());
            }

            _fileStringList.Add(TEMPLATE_BRACKETS_TAIL);
        }

        //C#文件模板
        //花括号
        private static readonly string TEMPLATE_BRACKETS_HEAD = "{";
        private static readonly string TEMPLATE_BRACKETS_TAIL = "}";

        //引用
        private static readonly string TEMPLATE_USING_COLLECTIONS = "using System.Collections.Generic;";
        private static readonly string TEMPLATE_USING_XML = "using System.Xml;";

        //命名空间
        private static readonly string TEMPLATE_NAMESPACE = "namespace Game.Config";

        //类名
        private static readonly string TEMPLATE_CLASS_NAME = "public class {0} : BaseConfig";

        //构造方法
        private static readonly string TEMPLATE_CONSTRUCTION_METHOD = "public {0}(XmlElement item)";

        //调用Parse
        private static readonly string TEMPLATE_CALL_PARSE_METHOD = "Parse(item);";

        //解析方法
        private static readonly string TEMPLATE_PARSE_METHOD = "private void Parse(XmlElement item)";
    }
}

