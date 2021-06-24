using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

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
                LoadGridData();     // 데이터 그리드
                InitErrorMessage(); // 에러메시지 숨김
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
                throw ex;
            }
        }
      
        // 수정 버튼
        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var settings = GrdData.SelectedItem as Model.Settings;
            //settings.CodeName = TxtCodeName.Text;
            //settings.CodeDesc = TxtCodeDesc.Text;
            settings.ModDate = DateTime.Now;

            try
            {
                var result = Logic.DataAccess.SetSettings(settings);
                if(result == 0)
                {
                    Commons.LOGGER.Error("데이터 수정시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 수정 성공 : {settings.BasicCode}");   // 로그에 남음
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
            var search = TxtSearch.Text.Trim();

            var settings = Logic.DataAccess.GetSettings().Where(s => s.CodeName.Contains(search)).ToList();
            this.DataContext = settings;
        }

        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs() != true) return;    // 입력할 값이 유효한지 확인
            
            var settings = new Model.Settings();
            //settings.BasicCode = TxtBasicCode.Text;
            //settings.CodeName = TxtCodeName.Text;
            //settings.CodeDesc = TxtCodeDesc.Text;
            settings.RegDate = DateTime.Now;
            settings.RedID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSettings(settings);
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 입력시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 입력실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 입력 성공 : {settings.BasicCode}");   // 로그에 남음
                    ClearInputs();  // 새로고침후 textbox 초기화
                    LoadGridData(); // 수정 후 그리드 데이터 새로고침
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex}");
            }
        }

        // 선택한 셀값을 textbox에 표시
        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var settings = GrdData.SelectedItem as Model.Settings;
                //TxtBasicCode.Text = settings.BasicCode;
                //TxtCodeName.Text = settings.CodeName;
                //TxtCodeDesc.Text = settings.CodeDesc;

                //TxtBasicCode.IsReadOnly = true;
                //TxtBasicCode.Background = new SolidColorBrush(Colors.LightGray);
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


        // 삭제 버튼 - 거의 사용하지 않음
        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var settings = GrdData.SelectedItem as Model.Settings;

            if (settings == null)   // 선택된 값이 없을 때
            {
                await Commons.ShowMessageAsync("삭제", "삭제할 코드를 선택하세요");
                return;
            }
            else
            {
                try
                {
                    var result = Logic.DataAccess.DelSettings(settings);
                    if (result == 0)
                    {
                        Commons.LOGGER.Error("데이터 삭제시 오류발생");
                        await Commons.ShowMessageAsync("오류", "데이터 삭제실패!");
                    }
                    else
                    {
                        Commons.LOGGER.Info($"데이터 삭제 성공 : {settings.BasicCode}");   // 로그에 남음
                        ClearInputs();  // 새로고침후 textbox 초기화
                        LoadGridData(); // 수정 후 그리드 데이터 새로고침
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 {ex}");
                }
            }
        }

        // 엔터키로 텍스트박스에서 버튼으로 이동(KeyDown 이벤트)
        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==System.Windows.Input.Key.Enter)
                BtnSearch_Click(sender, e);
        }

        // 에러메시지 초기화
        private void InitErrorMessage()
        {
            //LblBasicCode.Visibility = LblCodeName.Visibility
            //      = LblCodeDesc.Visibility = Visibility.Hidden;
        }

        // 데이터 그리드 새로고침
        private void LoadGridData()
        {
            List<Model.Settings> settings = Logic.DataAccess.GetSettings();
            this.DataContext = settings;
        }

        // textbox 초기화
        private void ClearInputs()
        {
            //TxtBasicCode.IsReadOnly = false;
            //TxtBasicCode.Background = new SolidColorBrush(Colors.White);

            //TxtBasicCode.Text = TxtCodeName.Text = TxtCodeDesc.Text = string.Empty; // ""
            //TxtBasicCode.Focus();
        }

        // 입력데이터 검증 메서드
        public bool IsValidInputs()
        {
            var isValid = true;
            InitErrorMessage();

            //if (string.IsNullOrEmpty(TxtBasicCode.Text))
            //{
            //    LblBasicCode.Visibility = Visibility.Visible;
            //    LblBasicCode.Text = "코드를 입력하세요.";
            //    isValid = false;
            //}
            //else if (Logic.DataAccess.GetSettings().Where(s => s.BasicCode.Equals(TxtBasicCode.Text)).Count() > 0)
            //{
            //    LblBasicCode.Visibility = Visibility.Visible;
            //    LblBasicCode.Text = "중복코드가 존재합니다.";
            //    isValid = false;
            //}

            //if (string.IsNullOrEmpty(TxtCodeName.Text))
            //{
            //    LblCodeName.Visibility = Visibility.Visible;
            //    LblCodeName.Text = "코드명를 입력하세요.";
            //    isValid = false;
            //}

            return isValid;
        }
    }
}
