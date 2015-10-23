using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GoodMan.Tool.Configration.WorkPool.Email
{
    public class EmailPoolElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EmailPoolElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EmailPoolElement)element).Partition;
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

        public EmailPoolElement this[int index]
        {
            get
            {
                return (EmailPoolElement)BaseGet(index);
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
        public EmailPoolElement this[string name]
        {
            get
            {
                return (EmailPoolElement)BaseGet(name);
            }
        }

    }
}
