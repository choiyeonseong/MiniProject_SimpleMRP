using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MRPApp.View.Schedule
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ScheduleList : Page
    {
        public ScheduleList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadControlData();// 콤보박스 데이터 로딩
                LoadGridData();     // 테이블 그리드 데이터 로딩
                InitErrorMessage(); // 에러메시지 숨김
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
                throw ex;
            }
        }

        // 콤보박스 데이터 로딩 초기화
        private void LoadControlData()
        {
            // 공장 코드
            var plantCodes = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("PC01")).ToList();
            CboPlantCode.ItemsSource = plantCodes;
            CboGridPlantCode.ItemsSource = plantCodes;
            // 공정 설비
            var facilityIDs = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("FAC1")).ToList();
            CboSchFacilityID.ItemsSource = facilityIDs;
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // 수정할 값이 유효한지 검사
            if (IsValidUpdates() != true) return;

            var item = GrdData.SelectedItem as Model.Schedules;

            item.PlantCode = CboPlantCode.SelectedValue.ToString();
            item.SchDate = DateTime.Parse(DtpSchDate.Text);
            item.SchLoadTime = int.Parse(TxtSchLoadTime.Text);
            if(TmpSchStartTime.SelectedDateTime!=null)
                item.SchStartTime = TmpSchStartTime.SelectedDateTime.Value.TimeOfDay;
            if(TmpSchEndTime.SelectedDateTime!=null)
                item.SchEndTime = TmpSchEndTime.SelectedDateTime.Value.TimeOfDay;
            item.SchFacilityID = CboSchFacilityID.SelectedValue.ToString();
            item.SchAmount = (int)NudSchAmount.Value;
            item.ModDate = DateTime.Now;
            item.ModID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSchedules(item);   // UPDATE
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 수정시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 수정 성공 : {item.SchIdx}");   // 로그에 남음
                    ClearInputs();  // 새로고침후 textbox 초기화
                    LoadGridData(); // 수정 후 그리드 데이터 새로고침
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex}");
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = DtpSearchDate.Text;

            var list = Logic.DataAccess.GetSchedules().Where(s => s.SchDate.Equals(DateTime.Parse(search))).ToList();   // 공정일자 검색
            this.DataContext = list;
        }

        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            // 입력할 값이 유효한지 확인
            if (IsValidInputs() != true) return;

            var item = new Model.Schedules();

            item.PlantCode = CboPlantCode.SelectedValue.ToString();
            item.SchDate = DateTime.Parse(DtpSchDate.Text);
            item.SchLoadTime = int.Parse(TxtSchLoadTime.Text);
            if (TmpSchStartTime.SelectedDateTime != null)   // 예외처리 - 날짜 선택 안된 경우
                item.SchStartTime = TmpSchStartTime.SelectedDateTime.Value.TimeOfDay;
            if (TmpSchEndTime.SelectedDateTime != null)     // 예외처리 - 날짜 선택 안된 경우
                item.SchEndTime = TmpSchEndTime.SelectedDateTime.Value.TimeOfDay;
            item.SchFacilityID = CboSchFacilityID.SelectedValue.ToString();
            item.SchAmount = (int)NudSchAmount.Value;
            item.RegDate = DateTime.Now;
            item.RegID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSchedules(item);   // INSERT
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 입력시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 입력실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 입력 성공 : {item.SchIdx}");   // 로그에 남음
                    ClearInputs();  // 새로고침후 textbox 초기화
                    LoadGridData(); // 수정 후 그리드 데이터 새로고침
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex}");
            }
        }

        // 선택한 셀값을 입력창에 표시
        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ClearInputs();  // 이전에 표시된 데이터 삭제
            try
            {
                var item = GrdData.SelectedItem as Model.Schedules;

                TxtSchIdx.Text = item.SchIdx.ToString();
                CboPlantCode.SelectedValue = item.PlantCode;
                DtpSchDate.Text = item.SchDate.ToString();
                TxtSchLoadTime.Text = item.SchLoadTime.ToString();
                if(item.SchStartTime!=null)
                    TmpSchStartTime.SelectedDateTime = new DateTime(item.SchStartTime.Value.Ticks);
                if(item.SchEndTime!=null)
                    TmpSchEndTime.SelectedDateTime = new DateTime(item.SchEndTime.Value.Ticks);
                CboSchFacilityID.SelectedValue = item.SchFacilityID;
                NudSchAmount.Value = item.SchAmount;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex}");
            }
        }

        // 신규 버튼
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();  // 새로운 데이터 입력하기 위해 textbox 초기화
        }

        // 입력창 초기화
        private void ClearInputs()
        {
            TxtSchIdx.Text = "";
            CboPlantCode.SelectedItem = null;
            DtpSchDate.Text = "";
            TxtSchLoadTime.Text = "";
            TmpSchStartTime.SelectedDateTime = null;
            TmpSchEndTime.SelectedDateTime = null;
            CboSchFacilityID.SelectedItem = null;
            NudSchAmount.Value = 0;

            CboPlantCode.Focus();   // 커서 위치 초기화
        }

        // 에러메시지 초기화
        private void InitErrorMessage()
        {
            LblPlantCode.Visibility
                = LblSchAmount.Visibility
                  = LblSchDate.Visibility
                  = LblSchEndTime.Visibility
                  = LblSchIdx.Visibility
                  = LblSchStartTime.Visibility
                  = LblSchFacilityID.Visibility
                  = LblSchLoadTime.Visibility = Visibility.Hidden;
        }

        // 데이터 그리드 새로고침
        private void LoadGridData()
        {
            List<Model.Schedules> schedules = Logic.DataAccess.GetSchedules();
            this.DataContext = schedules;
        }

        // 입력데이터 검증 메서드 (입력창이 많을수록 길어짐)
        public bool IsValidInputs()
        {
            var isValid = true;
            InitErrorMessage();

            if (CboPlantCode.SelectedValue == null)
            {
                LblPlantCode.Visibility = Visibility.Visible;
                LblPlantCode.Text = "공장을 선택하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(DtpSchDate.Text))
            {
                LblSchDate.Visibility = Visibility.Visible;
                LblSchDate.Text = "공정일을 입력하세요";
                isValid = false;
            }

            // 공장별로 공정일이 이미 DB상에 값이 있으면 중복 안됨
            // PC010001(수원) 2021-06-24
            if (CboPlantCode.SelectedValue != null && string.IsNullOrEmpty(DtpSchDate.Text))
            {
                var result = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(CboPlantCode.SelectedValue.ToString()))
                    .Where(d => d.SchDate.Equals(DateTime.Parse(DtpSchDate.Text))).Count();
                if (result > 0)
                {
                    LblSchDate.Visibility = Visibility.Visible;
                    LblSchDate.Text = "해당 공장 공정일에 계획이 이미 있습니다.";
                    isValid = false;
                }
            }

            if (string.IsNullOrEmpty(TxtSchLoadTime.Text))
            {
                LblSchLoadTime.Visibility = Visibility.Visible;
                LblSchLoadTime.Text = "로드타임을 입력하세요";
                isValid = false;
            }

            // TODO : 로드타임에 숫자가 아닌값이 들어가면 안됨(pass)

            if (CboSchFacilityID.SelectedValue == null)
            {
                LblSchFacilityID.Visibility = Visibility.Visible;
                LblSchFacilityID.Text = "공정설비를 선택하세요";
                isValid = false;
            }

            if (NudSchAmount.Value <= 0)
            {
                LblSchAmount.Visibility = Visibility.Visible;
                LblSchAmount.Text = "계획수량은 0개 이상입니다.";
                isValid = false;
            }

            return isValid;
        }

        // 수정데이터 검증 메서드 (입력창이 많을수록 길어짐)
        public bool IsValidUpdates()
        {
            var isValid = true;
            InitErrorMessage();

            if (CboPlantCode.SelectedValue == null)
            {
                LblPlantCode.Visibility = Visibility.Visible;
                LblPlantCode.Text = "공장을 선택하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(DtpSchDate.Text))
            {
                LblSchDate.Visibility = Visibility.Visible;
                LblSchDate.Text = "공정일을 입력하세요";
                isValid = false;
            }

            //// 공장별로 공정일이 이미 DB상에 값이 있으면 중복 안됨
            //// PC010001(수원) 2021-06-24
            //if (CboPlantCode.SelectedValue != null && string.IsNullOrEmpty(DtpSchDate.Text))
            //{
            //    var result = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(CboPlantCode.SelectedValue.ToString()))
            //        .Where(d => d.SchDate.Equals(DateTime.Parse(DtpSchDate.Text))).Count();
            //    if (result > 0)
            //    {
            //        LblSchDate.Visibility = Visibility.Visible;
            //        LblSchDate.Text = "해당 공장 공정일에 계획이 이미 있습니다.";
            //        isValid = false;
            //    }
            //}

            if (string.IsNullOrEmpty(TxtSchLoadTime.Text))
            {
                LblSchLoadTime.Visibility = Visibility.Visible;
                LblSchLoadTime.Text = "로드타임을 입력하세요";
                isValid = false;
            }

            // TODO : 로드타임에 숫자가 아닌값이 들어가면 안됨(pass)

            if (CboSchFacilityID.SelectedValue == null)
            {
                LblSchFacilityID.Visibility = Visibility.Visible;
                LblSchFacilityID.Text = "공정설비를 선택하세요";
                isValid = false;
            }

            if (NudSchAmount.Value <= 0)
            {
                LblSchAmount.Visibility = Visibility.Visible;
                LblSchAmount.Text = "계획수량은 0개 이상입니다.";
                isValid = false;
            }

            return isValid;
        }
    }
}
