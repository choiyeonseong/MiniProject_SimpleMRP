using System;

namespace MRPApp.Model
{
    /// <summary>
    /// 가상의 테이블 Report 생성, 실제 DB에는 없음
    /// </summary>
    public class Report
    {
        public int SchIdx { get; set; }
        public string PlantCode { get; set; }
        public Nullable<int> SchAmount { get; set; }
        public System.DateTime PrcDate { get; set; }
        public Nullable<int> PrcOkAmount { get; set; }
        public Nullable<int> PrcFailAmount { get; set; }
    }
}
