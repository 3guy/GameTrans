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
    [Table("T_OrderDetail")]
    public class T_OrderDetail : IEntity<T_OrderDetail>
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long GameID { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Numbers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PayAccount { get; set; }

        /// <summary>
        /// 挂单元宝数
        /// </summary>
        public long GoldAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PayDeviceAccount { get; set; }

        /// <summary>
        /// 对应游戏玩家player表id
        /// </summary>
        public long Pay4PlayerId { get; set; }

        public override T_OrderDetail Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_OrderDetail Find(Expression<Func<T_OrderDetail, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.OrderDetail.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;

                    this.ProductName = item.ProductName;

                    this.GameID = item.GameID;

                    this.Numbers = item.Numbers;

                    this.UnitPrice = item.UnitPrice;

                    this.TotalPrice = item.TotalPrice;

                    this.PayTime = item.PayTime;

                    this.PayAccount = item.PayAccount;

                    this.GoldAccount = item.GoldAccount;

                    this.PayDeviceAccount = item.PayDeviceAccount;

                    this.Pay4PlayerId = item.Pay4PlayerId;

                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_OrderDetail, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.OrderDetail.Any(where);
            }
        }
    }
}
