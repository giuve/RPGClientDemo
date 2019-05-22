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
            string path = "/Resources/Config/City.xlsx";

            //LoaderXml loader = new LoaderXml();
            LoaderExcel loader = new LoaderExcel();
            LoaderData loaderData = loader.Loader(path);

            Analyzer analyzer = new Analyzer();
            AnalyzerData analysisData = analyzer.Analysis(loaderData);
            analysisData.TarPath = Application.dataPath + "/Scripts/Config/{0}.cs";
            ExporterCSharp exporter = new ExporterCSharp(analysisData);
            exporter.Export();
            //analysisData.TarPath = Application.dataPath + "/Resources/Config/City.xlsx";
            //ExporterExcel exporter = new ExporterExcel(analysisData);
            //exporter.Export();
        }

    }

}
