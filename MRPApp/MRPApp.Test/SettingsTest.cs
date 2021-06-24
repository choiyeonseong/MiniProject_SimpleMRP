using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRPApp.View.Setting;
using System;
using System.Linq;

namespace MRPApp.Test
{
    [TestClass]
    public class SettingsTest
    {
        // Db사에 중복된 데이터 있는지 테스트
        [TestMethod]
        public void IsDuplicateDataTest()
        {
            var inputCode = "PC010001"; // DB상에 있는 값
            var expectVal = true;       // 예상값

            var code = Logic.DataAccess.GetSettings().Where(d => d.BasicCode.Contains(inputCode)).FirstOrDefault();
            var realVal = code != null ? true : false;

            Assert.AreNotEqual(expectVal, realVal); // 값이 같으면 pass, 다르면 fail
        }
    }
}
