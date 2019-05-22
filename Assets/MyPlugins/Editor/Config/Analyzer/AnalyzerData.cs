
using System.Collections.Generic;

namespace Editor.Config
{
    public class AnalyzerData
    {
        public string SrcPath;
        public string TarPath;
        public Dictionary<string, FormatBase> Format;
        public List<Dictionary<string, ItemData>> Data;
    }
}
