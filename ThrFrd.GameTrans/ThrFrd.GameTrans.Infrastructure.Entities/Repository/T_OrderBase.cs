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
    [Table("T_OrderBase")]
    public class T_OrderBase : IEntity<T_OrderBase>
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
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 交易猫/ 手动下单，登录user name
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 充值人
        /// </summary>
        public string PayerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 转账订单号
        /// </summary>
        public string TransferAccountNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TransferredAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TransferAccountTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TransferMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BeneficiaryAccountNo { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreaterUser { get; set; }

        public override T_OrderBase Find(string key)
        {
            int Id = Int32.Parse(key);
            return Find(c => c.ID == Id);
        }

        public override T_OrderBase Find(Expression<Func<T_OrderBase, bool>> where)
        {
            using (Context ctx = new Context())
            {
                var item = ctx.OrderBase.FirstOrDefault(where);
                if (item != null)
                {
                    this.ID = item.ID;

                    this.CreateTime = item.CreateTime;

                    this.TotalPrice = item.TotalPrice;

                    this.State = item.State;

                    this.Source = item.Source;

                    this.PayerName = item.PayerName;

                    this.Comments = item.Comments;

                    this.TransferAccountNumber = item.TransferAccountNumber;

                    this.TransferredAmount = item.TransferredAmount;

                    this.TransferAccountTime = item.TransferAccountTime;

                    this.TransferMethod = item.TransferMethod;

                    this.BeneficiaryAccountNo = item.BeneficiaryAccountNo;

                    this.CreaterUser = item.CreaterUser;

                }
                return null;
            }
        }

        public override bool Any(Expression<Func<T_OrderBase, bool>> where)
        {
            using (Context ctx = new Context())
            {
                return ctx.OrderBase.Any(where);
            }
        }
    }
}