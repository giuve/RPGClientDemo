using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigLoaderData {
    public string path { set; get; }
    public LoaderResult Result { set; get; }
    public Dictionary<string, string> Format { set; get; }
    public List<Dictionary<string, string>> Data { set; get; }
}
