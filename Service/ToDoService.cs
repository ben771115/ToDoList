using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace WebToDoList.Service
{
    public class ToDoService
    { 
            public static ToDo GetDbAuth(string id)
            {
                using (var cn = new SqlConnection(ConfigurationManager.AppSettings["ErpConnection"]))
                {
                    var model = cn.Query<ToDo>(@"
                    select [EMPNO],[EMP_NAME],
                      case when DPT='0041' then 'AD' 
                       when DPT='0030' then 'SD'
                       when DPT='0030' then 'SM'
                       when DPT='0038' then 'MM'
                       when DPT='0039' then 'HR'
                       when DPT='0039' then 'FI'
                       when DPT='0047' then 'IS'
                       when DPT='0040' then 'NC' END as [MDL]
                      from hr030 
                      where [EMPNO]=@EMPNO", new EmpLv { EMPNO = id }).FirstOrDefault();
                    return model;
                }
            }

            public static bool InsertDbAuth(EmpLv model)
            {
                using (var cnDb = new SqlConnection(ConfigurationManager.AppSettings["ErpConnection"]))
                {
                    var sqlcmdDb = new StringBuilder();

                    sqlcmdDb.Append(@"insert into [EMP_LV]([EMPNO],[EMP_NAME],[MDL],[LV])");

                    sqlcmdDb.Append(@" values (@EMPNO,@EMP_NAME,@MDL,@LV)");

                    var DbInsertEmp_Lv = cnDb.ExecuteScalar<EmpLv>(sqlcmdDb.ToString(), model);
                };
                return true;
            }

            public static bool DeleteDbAuth(string id)
            {
                using (var cnDb = new SqlConnection(ConfigurationManager.AppSettings["ErpConnection"]))
                {
                    var sqlcmdDb = new StringBuilder();
                    sqlcmdDb.AppendFormat(@"delete from [EMP_LV] where EMPNO='{0}'", id);

                    cnDb.Execute(sqlcmdDb.ToString());
                };
                return true;
            }
      
    }
}