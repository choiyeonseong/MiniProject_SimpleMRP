using MRPApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRPApp.Logic
{
    public class DataAccess
    {
        #region Settings 테이블 데이터 처리
        public static List<Settings> GetSettings()
        {
            List<Model.Settings> list;

            using (var ctx = new MRPEntities()) // DBContext 이름으로 connection
                list = ctx.Settings.ToList();   // SELECT

            return list;
        }
        public static int SetSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Settings.AddOrUpdate(item); // UPDATE
                return ctx.SaveChanges();       // COMMIT
            }
        }
        public static int DelSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                var obj = ctx.Settings.Find(item.BasicCode);    // 데이터 검색
                ctx.Settings.Remove(obj);   // DELETE
                return ctx.SaveChanges();
            }
        }
        #endregion

        #region Schedules 테이블에서 데이터 가져오기
        internal static List<Schedules> GetSchedules()
        {
            List<Model.Schedules> list;

            using (var ctx = new MRPEntities()) // DBContext 이름으로 connection
                list = ctx.Schedules.ToList();  // SELECT

            return list;
        }

        internal static int SetSchedules(Schedules item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Schedules.AddOrUpdate(item); // UPDATE
                return ctx.SaveChanges();       // COMMIT
            }
        }

        #endregion

        #region Process
        internal static List<Process> GetProcesses()
        {
            List<Model.Process> list;

            using (var ctx = new MRPEntities())
                list = ctx.Process.ToList(); // SELECT

            return list;
        }

        internal static int SetProcess(Process item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Process.AddOrUpdate(item); // INSERT | UPDATE
                return ctx.SaveChanges(); // COMMIT
            }
        }

        #endregion
    }
}
