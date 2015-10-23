using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Data;
using System.Collections;
using System.Data.Entity;
using System.Data.Common;

namespace ThrFrd.GameTrans.Infrastructure.Entities.EFContext
{
    public static class ContextExtensions
    {
        public static int ExecuteCommand(this Context ctx,string sqlCommand, params DbParameter[] parame)
        {
            return ctx.Database.ExecuteSqlCommand(sqlCommand, parame);
        }
    }
}
