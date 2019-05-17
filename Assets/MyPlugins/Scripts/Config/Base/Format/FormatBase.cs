using UnityEngine;
using System.Collections;
using System;

namespace Editor.Config
{
    public abstract class FormatBase
    {
        public FormatBase(string itemName)
        {
            Name = itemName;
        }

        public abstract object Parse(string src);

        public abstract string ExportCs();

        public string Name { set; get; }
    }
}

