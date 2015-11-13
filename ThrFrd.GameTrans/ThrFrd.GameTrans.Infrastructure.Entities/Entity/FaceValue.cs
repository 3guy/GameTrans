using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    /// <summary>
    /// 面值表（暂时不需要用）
    /// </summary>
    public class FaceValue : IAccessible<T_FaceValue, FaceValue>
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Price { get; set; }

        protected override void Covariant(T_FaceValue model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.Name = model.Name;
                this.Price = model.Price;

            }
        }

        protected override T_FaceValue Inverter(FaceValue model)
        {
            if (model != null)
            {
                var entity = new T_FaceValue();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Price = model.Price;
                return entity;
            }
            return null;
        }
    }
}
