using UnityEngine;
using System.Collections;

namespace Editor.Config
{
    public class LoaderBase
    {
        public virtual LoaderData Loader(string path)
        {
            LoaderData ret = new LoaderData();
            ret.Path = path;
            return ret;
        }
    }
}
