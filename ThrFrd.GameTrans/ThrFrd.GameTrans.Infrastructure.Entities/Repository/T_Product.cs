using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Repository
{
    [Table("T_Product")]
    public  class T_Product:IEntity<T_Product>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long GameID { get; set; }
        public string Category { get; set; }
        public string Client { get; set; }
        public string Domain { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> OriginalPrice { get; set; }

        public override T_Product Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.Id == Id);
        }

        public override T_Product Find(System.Linq.Expressions.Expression<Func<T_Product, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.Product.FirstOrDefault(where);
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Name;
                    GameID = item.GameID;
                    Category = item.Category;
                    Client = item.Client;
                    Domain = item.Domain;
                    Amount = item.Amount;
                    Description = item.Description;
                    return this;
                }
                return null;
            }
        }

        public override bool Any(System.Linq.Expressions.Expression<Func<T_Product, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.Product.Any(where);
            }
        }

    }
}
