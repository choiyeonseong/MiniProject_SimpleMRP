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
        // Settings 테이블에서 데이터 가져오기
        public static List<Settings> GetSettings()
        {
            List<Model.Settings> settings;

            using (var ctx = new MRPEntities()) // DBContext 이름으로 connection
                settings = ctx.Settings.ToList();

            return settings;
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
    }
}
