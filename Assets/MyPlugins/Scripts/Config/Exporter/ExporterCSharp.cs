using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Editor.Config
{
    public class ExporterCSharp : ExporterBase
    {
        public ExporterCSharp(string srcPath, string tarPath, List<Dictionary<string, ItemData>> data) 
            :base(srcPath, tarPath, data)
        {

        }

        public override void Export()
        {
            if(null == _data || _data.Count <= 0)
            {
                CLog.LogErrorFormat("Export error: no data in path {0}", _tarPath);
                return;
            }
            //CS要导出2个文件,ValueObject, ConfigDB
            ExportValueObject();
            ExportConfigDB();
        }

        private void ExportValueObject()
        {
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

            string srcFileName = Path.GetFileNameWithoutExtension(_srcPath);
            //类名
            _fileStringList.Add(string.Format(TEMPLATE_VO_CLASS_NAME, srcFileName));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _tailStack.Push(TEMPLATE_BRACKETS_TAIL);

            //构造方法和解析方法
            ExportParseMethod();

            //各字段,只取第一行数据进行生成即可
            Dictionary<string, ItemData>.Enumerator iter = _data[0].GetEnumerator();
            while (iter.MoveNext())
            {
                //ID在基类中已定义，此处不用导出
                if("ID" == iter.Current.Value.Format.Name)
                {
                    continue;
                }
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
            SaveFile(string.Format(_tarPath, srcFileName+"Config"));
        }

        private void ExportParseMethod()
        {
            //生成构造方法
            //_fileStringList.Add(string.Format(TEMPLATE_CONSTRUCTION_METHOD, Path.GetFileNameWithoutExtension(_path)));
            //_fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            //_fileStringList.Add(TEMPLATE_CALL_PARSE_METHOD);
            //_fileStringList.Add(TEMPLATE_BRACKETS_TAIL);

            //生成解析函数
            _fileStringList.Add(TEMPLATE_PARSE_METHOD);
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);

            Dictionary<string, ItemData>.Enumerator iter = _data[0].GetEnumerator();
            while (iter.MoveNext())
            {
                _fileStringList.AddRange(iter.Current.Value.ExportCsParse());
            }

            _fileStringList.Add(TEMPLATE_BRACKETS_TAIL);
        }

        private void ExportConfigDB()
        {
            _fileStringList.Clear();
            
            //引用(using)
            _fileStringList.Add(TEMPLATE_USING_XML);
            _fileStringList.Add(TEMPLATE_USING_COLLECTIONS);
            _fileStringList.Add(TEMPLATE_USING_UNITY);
 
            //命名空间
            _fileStringList.Add(TEMPLATE_NAMESPACE);
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _tailStack.Push(TEMPLATE_BRACKETS_TAIL);

            string srcFileName = Path.GetFileNameWithoutExtension(_srcPath);

            //类名
            _fileStringList.Add(string.Format(TEMPLATE_DB_CLASS_NAME, srcFileName));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _tailStack.Push(TEMPLATE_BRACKETS_TAIL);

            //XML文件路径
            _fileStringList.Add(string.Format(TEMLATE_DB_PATH, _srcPath));
            //字典声明
            _fileStringList.Add(string.Format(TEMPLATE_DB_DICT_STATEMENT, srcFileName));

            //初始化函数
            _fileStringList.Add(string.Format(TEMPLATE_DB_INIT_METHOD));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _fileStringList.Add(TEMPLATE_DB_ALEADY_INIT);
            _fileStringList.Add(string.Format(TEMPLATE_DB_BASE_INIT, srcFileName));
            _fileStringList.Add(TEMPLATE_BRACKETS_TAIL);

            //预初始化函数
            _fileStringList.Add(string.Format(TEMPLATE_DB_PRE_INIT_METHOD));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _fileStringList.Add(TEMPLATE_DB_CALL_INIT);
            _fileStringList.Add(TEMPLATE_BRACKETS_TAIL);

            //获取整个字典
            _fileStringList.Add(string.Format(TEMPLATE_DB_GET_DICT, srcFileName));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _fileStringList.Add(TEMPLATE_DB_CALL_INIT);
            _fileStringList.Add(TEMPLATE_DB_RETURN_DICT);
            _fileStringList.Add(TEMPLATE_BRACKETS_TAIL);

            //获取整个字典的迭代器
            _fileStringList.Add(string.Format(TEMPLATE_DB_GET_ITER, srcFileName));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _fileStringList.Add(TEMPLATE_DB_CALL_INIT);
            _fileStringList.Add(TEMPLATE_DB_RETURN_ITER);
            _fileStringList.Add(TEMPLATE_BRACKETS_TAIL);

            //获取某个ValueObject
            _fileStringList.Add(string.Format(TEMPLATE_DB_GET_VO, srcFileName));
            _fileStringList.Add(TEMPLATE_BRACKETS_HEAD);
            _fileStringList.Add(TEMPLATE_DB_CALL_INIT);
            _fileStringList.Add(TEMPLATE_DB_RETURN_VO);
            _fileStringList.Add(TEMPLATE_DB_RETURN_NULL);
            _fileStringList.Add(TEMPLATE_BRACKETS_TAIL);

            //收尾
            while (_tailStack.Count > 0)
            {
                _fileStringList.Add(_tailStack.Pop());
            }

            //保存到文件上 
            SaveFile(string.Format(_tarPath, srcFileName + "ConfigDB"));
        }

        //C#文件模板
        //花括号
        public static readonly string TEMPLATE_BRACKETS_HEAD = "{";
        public static readonly string TEMPLATE_BRACKETS_TAIL = "}";

        //引用
        private static readonly string TEMPLATE_USING_COLLECTIONS = "using System.Collections.Generic;";
        private static readonly string TEMPLATE_USING_XML = "using System.Xml;";
        private static readonly string TEMPLATE_USING_UNITY = "using UnityEngine;";

        //命名空间
        private static readonly string TEMPLATE_NAMESPACE = "namespace Game.Config";

        //ValueObject类名(XxConfig)
        private static readonly string TEMPLATE_VO_CLASS_NAME = "public class {0}Config : BaseConfig";
        private static readonly string TEMPLATE_DB_CLASS_NAME = "public class {0}ConfigDB : BaseConfigDB";

        //构造方法
        private static readonly string TEMPLATE_CONSTRUCTION_METHOD = "public {0}(XmlElement item)";

        //调用Parse
        private static readonly string TEMPLATE_CALL_PARSE_METHOD = "Parse(item);";

        //解析方法
        private static readonly string TEMPLATE_PARSE_METHOD = "public override void Parse(XmlElement item)";

        //DB中XML文件路径
        private static readonly string TEMLATE_DB_PATH = "private static readonly string PATH = Application.dataPath + \"{0}\";";
        //DB中字典声明
        private static readonly string TEMPLATE_DB_DICT_STATEMENT = "private static Dictionary<int, {0}Config> _dict = null;";

        //初始化函数
        private static readonly string TEMPLATE_DB_INIT_METHOD = "private static void Init()";

        //预初始化函数
        private static readonly string TEMPLATE_DB_PRE_INIT_METHOD = "public static void PreInit()";

        //调用初始化函数
        private static readonly string TEMPLATE_DB_CALL_INIT = "Init();";
        //已经初始化
        private static readonly string TEMPLATE_DB_ALEADY_INIT = "if (null != _dict) return;";
        //调用基类初始化方法
        private static readonly string TEMPLATE_DB_BASE_INIT = " _dict = Init<{0}Config>(PATH);";
        //获取整个字典的方法
        private static readonly string TEMPLATE_DB_GET_DICT = "public static Dictionary<int, {0}Config> GetConfigDict()";
        //返回整个字典
        private static readonly string TEMPLATE_DB_RETURN_DICT = "return _dict;";
        //获取整个字典的迭代器
        private static readonly string TEMPLATE_DB_GET_ITER = "public static Dictionary<int, {0}Config>.Enumerator GetConfigIter()";
        //返回整个字典的迭代器
        private static readonly string TEMPLATE_DB_RETURN_ITER = "return _dict.GetEnumerator();";
        //获取某个ValueObject
        private static readonly string TEMPLATE_DB_GET_VO = "public static {0}Config GetConfigValueById(int id)";
        //返回某个ValueObject
        private static readonly string TEMPLATE_DB_RETURN_VO = "if (_dict.ContainsKey(id)) return _dict[id];";
        //返回空
        private static readonly string TEMPLATE_DB_RETURN_NULL = "return null;";

    }
}

