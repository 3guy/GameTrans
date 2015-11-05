using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.Entity
{
    public class Product : IAccessible<T_Product, Product>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long GameID { get; set; }
        public string Category { get; set; }
        public string Client { get; set; }
        public string Domain { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> OriginalPrice { get; set; }


        protected override void Covariant(T_Product model)
        {
            if (model != null)
            {
                Id = model.Id;
                Name = model.Name;
                GameID = model.GameID;
                Category = model.Category;
                Client = model.Client;
                Domain = model.Domain;
                Amount = model.Amount;
                Description = model.Description;
               
            }

        }
        protected override T_Product Inverter(Product item)
        {
            if (item != null)
            {
                return new T_Product()
                {
                    Id = item.Id,
                    Name = item.Name,
                    GameID = item.GameID,
                    Category = item.Category,
                    Client = item.Client,
                    Domain = item.Domain,
                    Amount = item.Amount,
                    Description = item.Description
                };
            }
            return null;
        }


    }
}
