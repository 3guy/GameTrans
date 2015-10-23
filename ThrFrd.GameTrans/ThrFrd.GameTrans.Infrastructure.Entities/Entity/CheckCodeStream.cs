using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Enum;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class CheckCodeStream : IAccessible<T_CheckCodeStream, CheckCodeStream>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
        public CheckCodeType Type { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime ExpireDate { get; set; }


        protected override void Covariant(T_CheckCodeStream model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.UserName = model.UserName;
                this.Mobile = model.Mobile;
                this.Code = model.Code;
                this.Type = (CheckCodeType)model.Type;
                this.RecordDate = model.RecordDate;
                this.ExpireDate = model.ExpireDate;
            }
        }

        protected override T_CheckCodeStream Inverter(CheckCodeStream model)
        {
            if (model != null)
            {
                return new T_CheckCodeStream()
                {
                    Id = model.Id,
                    Code = model.Code,
                    Mobile = model.Mobile,
                    ExpireDate = model.ExpireDate,
                    RecordDate = model.RecordDate,
                    Type = (int)model.Type,
                    UserName = model.UserName
                };
            }
            return null;
        }
    }
}
