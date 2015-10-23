using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;

namespace GoodMan.Tool.Configration.WorkPool.Email
{
    public class EmailPoolItemElementCollection : ConfigurationElementCollection
    {
        static int seed = 0;
        static int leng = 0;
        protected override ConfigurationElement CreateNewElement()
        {
            return new EmailPoolItemElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EmailPoolItemElement)element).Address;
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

        public EmailPoolItemElement this[int index]
        {
            get
            {
                return (EmailPoolItemElement)BaseGet(index);
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

        public EmailPoolItemElement RandomGet()
        {
            if (leng == 0)
            {
                leng = this.Count;
            }
            EmailPoolItemElement element = BaseGet(seed % leng) as EmailPoolItemElement;
            Interlocked.Increment(ref seed);
            return element;
        }
    }
}
