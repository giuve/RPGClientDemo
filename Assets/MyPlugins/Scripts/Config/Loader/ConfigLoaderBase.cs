using UnityEngine;
using System.Collections;

public class ConfigLoaderBase
{
    public virtual ConfigLoaderData Loader(string path)
    {
        ConfigLoaderData ret = new ConfigLoaderData();
        ret.path = path;
        ret.Result = LoaderResult.NOT_INIT;
        return ret;
    }
}

public enum LoaderResult
{
    SUCCESS = 0,
    NOT_INIT,
}