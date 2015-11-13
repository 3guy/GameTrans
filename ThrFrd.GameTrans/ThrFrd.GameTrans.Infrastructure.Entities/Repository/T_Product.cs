using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    [Table("T_Product")]
    public class T_Product : IEntity<T_Product>
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public override T_Product Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_Product Find(Expression<Func<T_Product, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Product.FirstOrDefault(where);
                if (item != null)
                {
                    this.Id = item.Id;

                    this.Name = item.Name;

                    this.OrderID = item.OrderID;

                    this.GameID = item.GameID;

                    this.Category = item.Category;

                    this.Client = item.Client;

                    this.Domain = item.Domain;

                    this.Amount = item.Amount;

                    this.Description = item.Description;

                    this.OriginalPrice = item.OriginalPrice;
                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_Product, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Product.Any(where);
            }
        }
    }
}
