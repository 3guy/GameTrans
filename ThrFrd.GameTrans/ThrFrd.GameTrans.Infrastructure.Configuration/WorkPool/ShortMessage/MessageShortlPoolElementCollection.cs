using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ThrFrd.GameTrans.Infrastructure.Configuration.WorkPool.Email
{
    public class ShortMessagePoolElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ShortMessagePoolElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ShortMessagePoolElement)element).Partition;
        }
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        protected override string ElementName
        {
            get
            {
                return "pool";
            }
        }

        public ShortMessagePoolElement this[int index]
        {
            get
            {
                return (ShortMessagePoolElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
        public ShortMessagePoolElement this[string name]
        {
            get
            {
                return (ShortMessagePoolElement)BaseGet(name);
            }
        }

    }
}
