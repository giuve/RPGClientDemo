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
        
        //C#文件模板
        //花括号
        private static readonly string TEMPLATE_BRACKETS_HEAD = "{";
        private static readonly string TEMPLATE_BRACKETS_TAIL = "}";

        private static readonly string TEMPLATE_USING_COLLECTIONS = "using System.Collections.Generic;";
        private static readonly string TEMPLATE_NAMESPACE = "namespace Game.Config";
        private static readonly string TEMPLATE_CLASS_NAME = "public class {0}Config : BaseConfig";
        
    }
}

