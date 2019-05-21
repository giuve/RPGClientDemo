using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.IO;

namespace Editor.Config
{
    public class ConfigEditor : UnityEditor.Editor
    {

        [MenuItem("配置/生成", false)]
        public static void CreateConfigFiles()
        {
            string path = "/Resources/Config/City.xml";

            LoaderXml loader = new LoaderXml();
            LoaderData loaderData = loader.Loader(path);

            Analyzer analyzer = new Analyzer();
            List<Dictionary<string, ItemData>> analysisData = analyzer.Analysis(loaderData);

            ExporterCSharp exporter = new ExporterCSharp(path, Application.dataPath + "/Scripts/Config/{0}.cs", analysisData);
            exporter.Export();
        }

    }

}
