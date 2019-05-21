using UnityEngine;
using System.Collections;
using System.Xml;

namespace Game.Config
{
    public abstract class BaseConfig
    {
        protected int _ID;
        public int ID { get { return _ID; } }
        public abstract void Parse(XmlElement item);
    }
}

