using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrFrd.GameTrans.Infrastructure.Entities;
using ThrFrd.GameTrans.Infrastructure.Entities.EFContext;
using ThrFrd.GameTrans.Infrastructure.Entities.Entity;
using ThrFrd.GameTrans.Tool;

namespace ThrFrd.GameTrans.Admin.Biz
{
   public class PlaceOrderBiz
    {
       public static ItemListBox<OrderBase> GetPlaceOrderListByPage(int page, int pageSize, string key, int status, string starttime, string endtime, ref int count)
       {

            try
            {
                using (Context ctx = new Context())
                {
                    string userName = System.Web.HttpContext.Current.User.Identity.Name;
                    var expression = from o in ctx.OrderBase  where o.CreaterUser == userName
                                     join d in ctx.OrderDetail on o.ID equals d.OrderID
                                     join p in ctx.Product on o.ID equals p.OrderID
                                     join ga in ctx.Game on p.GameID equals ga.id into gm
                                     from g in gm.DefaultIfEmpty()
                                     select new { o, d, p, g };

                    if (!string.IsNullOrEmpty(key))
                    {
                        expression = expression.Where(a => a.o.CreaterUser.Contains(key) || a.o.Source.Contains(key) || a.p.Name.Contains(key) 
                            || a.p.Description.Contains(key) || (a.g != null && a.g.name.Contains(key)));
                    }
                    if (status != -100)
                    {
                        expression = expression.Where(a => a.o.State == status);
                    }
                    if (!string.IsNullOrEmpty(starttime))
                    {
                        DateTime startTime = DateTime.Parse(starttime + " 00:00:00");
                        expression = expression.Where(a => a.o.CreateTime >= startTime);
                    }
                    if (!string.IsNullOrEmpty(endtime))
                    {
                        DateTime endTime = DateTime.Parse(endtime + " 23:59:59");
                        expression = expression.Where(a => a.o.CreateTime < endTime);
                    }

                    List<OrderBase> orderList = expression
                        .ToList()
                        .GroupBy(a => new { a.o })
                        .Select(a => new OrderBase().Set(a.FirstOrDefault().o))
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .ToList();

                    return new ItemListBox<OrderBase>(orderList).BuildPage(count, page, pageSize, new PageParameter() { Style = "admin" });

                }
            }
            catch (Exception ex)
            {
                LogHelper.Write(CommonLogger.Application, ex);
            }
            return null;
        }
       

    }
}
