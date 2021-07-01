using MRPApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
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

        #region Schedules 테이블 데이터 처리
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

        #region Process 테이블 데이터 처리
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

        #region Report 가상 테이블 데이터 처리 
        internal static List<Model.Report> GetReportDatas(string startDate, string endDate, string plantCode)
        {
            var connString = ConfigurationManager.ConnectionStrings["MRPConnString"].ToString();
            var list = new List<Model.Report>();
            var lastObj = new Model.Report();   // 최종 Report값 담는 변수

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();    // 필수 중요 -> close는 using이 해줌

                var sqlQuery = $@"SELECT sch.SchIdx, sch.PlantCode, sch.SchAmount, prc.PrcDate, 
                                         prc.PrcOkAmount, prc.PrcFailAmount 
                                    FROM Schedules AS sch 
                                   INNER JOIN(
                                              SELECT smr.SchIdx, smr.PrcDate, 
                                                     SUM(smr.PrcOk) AS PrcOkAmount, SUM(smr.PrcFail) AS PrcFailAmount 
                                                FROM (
                                                      SELECT p.SchIdx, p.PrcDate, 
                                                        CASE p.PrcResult WHEN 1 THEN 1 ELSE 0 END AS PrcOk, 
                                                        CASE p.PrcResult WHEN 0 THEN 1 ELSE 0 END AS PrcFail 
                                                        FROM Process AS p 
                                                     ) AS smr 
                                               GROUP BY smr.SchIdx, smr.PrcDate 
                                             ) AS prc 
                                      ON sch.SchIdx = prc.SchIdx 
                                   WHERE sch.PlantCode = '{plantCode}' 
                                     AND prc.PrcDate BETWEEN '{startDate}' AND '{endDate}'";

                var cmd = new SqlCommand(sqlQuery, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var tmp = new Report
                    {
                        SchIdx = (int)reader["SchIdx"],
                        PlantCode = reader["PlantCode"].ToString(),
                        PrcDate = DateTime.Parse(reader["PrcDate"].ToString()),
                        SchAmount = (int)reader["SchAmount"],
                        PrcOkAmount = (int)reader["PrcOkAmount"],
                        PrcFailAmount = (int)reader["PrcFailAmount"]
                    };
                    list.Add(tmp);
                    lastObj = tmp;  // 마지막 값을 할당
                }

                // 시작일부터 종료일까지 없는 값 만들어주는 로직
                var DtStart = DateTime.Parse(startDate);
                var DtEnd = DateTime.Parse(endDate);
                var DtCurrent = DtStart;

                while (DtCurrent < DtEnd)
                {
                    var count = list.Where(c => c.PrcDate.Equals(DtCurrent)).Count();
                    if (count == 0)
                    {
                        // 새로운 Report(없는 날짜)
                        var tmp = new Report
                        {
                            SchIdx = lastObj.SchIdx,
                            SchAmount = 0,
                            PrcFailAmount = 0,
                            PrcOkAmount = 0,
                            PrcDate = DtCurrent,
                        };
                        list.Add(tmp);
                    }
                    DtCurrent = DtCurrent.AddDays(1); // 날하루 증가
                }
            }
            list.Sort((reportA, reportB) => reportA.PrcDate.CompareTo(reportB.PrcDate)); // 가장오래된 날짜부터 오름차순 정렬
            return list;
        }
        #endregion
    }
}
