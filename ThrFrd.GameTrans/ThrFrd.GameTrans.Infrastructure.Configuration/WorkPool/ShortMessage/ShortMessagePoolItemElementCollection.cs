using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;

namespace ThrFrd.GameTrans.Infrastructure.Configuration.WorkPool.Email
{
    public class ShortMessagePoolItemElementCollection : ConfigurationElementCollection
    {
        static int seed = 0;
        static int leng = 0;
        protected override ConfigurationElement CreateNewElement()
        {
            return new ShortMessagePoolItemElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ShortMessagePoolItemElement)element).UserName;
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
                return "item";
            }
        }

        public ShortMessagePoolItemElement this[int index]
        {
            get
            {
                return (ShortMessagePoolItemElement)BaseGet(index);
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

        public ShortMessagePoolItemElement RandomGet()
        {
            if (leng == 0)
            {
                leng = this.Count;
            }
            ShortMessagePoolItemElement element = BaseGet(seed % leng) as ShortMessagePoolItemElement;
            Interlocked.Increment(ref seed);
            return element;
        }
    }
}
