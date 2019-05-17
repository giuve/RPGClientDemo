using UnityEngine;
using System.Collections;
using System;

namespace Editor.Config
{
    public class FormatCustom : FormatBase
    {
        public FormatCustom(string itemName) : base(itemName)
        {

        }

        public override string ExportCs()
        {
            throw new NotImplementedException();
        }

        public override object Parse(string src)
        {
            throw new NotImplementedException();
        }
    }
}

