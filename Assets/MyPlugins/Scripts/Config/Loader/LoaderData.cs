using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Editor.Config
{
    public class LoaderData
    {
        public string Path { set; get; }
        public Dictionary<string, string> Format { set; get; }
        public List<Dictionary<string, string>> Data { set; get; }
    }
}
