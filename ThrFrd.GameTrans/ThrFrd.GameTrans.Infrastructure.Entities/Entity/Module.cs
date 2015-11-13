using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    /// <summary>
    /// 配置项表
    /// </summary>
    public class Module : IAccessible<T_Module, Module>
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Url { get; set; }
        public int ParentID { get; set; }
        public int SeqNo { get; set; }
        public string Icon { get; set; }
        public bool IsDisplay { get; set; }

        public bool IsSelect { get; set; }

        public List<Module> ChildrenList { get; set; }

        protected override void Covariant(T_Module model)
        {
            if (model != null)
            {
                this.ID = model.ID;
                this.Code = model.Code;
                this.Name = model.Name;
                this.ParentID = model.ParentID;
                this.SeqNo = model.SeqNo;
                this.Url = model.Url;
                this.Icon = model.Icon;
                this.IsDisplay = model.IsDisplay;
                this.Level = model.Level;
            }
        }

        protected override T_Module Inverter(Module model)
        {
            if (model != null)
            {
                return new T_Module()
                {

                    ID = model.ID,
                    Code = model.Code,
                    Name = model.Name,
                    ParentID = model.ParentID,
                    SeqNo = model.SeqNo,
                    Url = model.Url,
                    Icon = model.Icon,
                    IsDisplay = model.IsDisplay,
                    Level = model.Level
                };
            }
            return null;
        }
    }
}
