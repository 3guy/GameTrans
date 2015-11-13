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
    /// 订单关联商品信息
    /// </summary>
    public class Product : IAccessible<T_Product, Product>
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
        public int OrderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long GameID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 客户端
        /// </summary>
        public string Client { get; set; }
        /// <summary>
        /// 服务器
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long Amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal OriginalPrice { get; set; }

        protected override void Covariant(T_Product model)
        {
            if (model != null)
            {
                this.Id = model.Id;
                this.Name = model.Name;
                this.GameID = model.GameID;
                this.OrderID = model.OrderID;
                this.Category = model.Category;
                this.Client = model.Client;
                this.Domain = model.Domain;
                this.Amount = model.Amount;
                this.Description = model.Description;
                this.OriginalPrice = model.OriginalPrice;

            }
        }

        protected override T_Product Inverter(Product model)
        {
            if (model != null)
            {
                var entity = new T_Product();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.OrderID = model.OrderID;
                entity.GameID = model.GameID;
                entity.Category = model.Category;
                entity.Client = model.Client;
                entity.Domain = model.Domain;
                entity.Amount = model.Amount;
                entity.Description = model.Description;
                entity.OriginalPrice = model.OriginalPrice;
                return entity;
            }
            return null;
        }
    }
}