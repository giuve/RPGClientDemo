using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Xml;

public class ConfigEditor : Editor {

	[MenuItem("配置/生成", false)]
    public static void CreateConfigFiles()
    {
        string path = Application.dataPath + "/Resources/Config/City.xml";

        ConfigLoaderXml loader = new ConfigLoaderXml();
        ConfigLoaderData loaderData = loader.Loader(path);

        ConfigAnalyzer analyzer = new ConfigAnalyzer();
        analyzer.Analysis(loaderData);
    }

}
