using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MRPApp.View.Process
{
    /// <summary>
    /// ProcessView.xaml에 대한 상호 작용 논리
    /// 1. 공정계획에서 오늘의 생산 계획 일정 불러옴
    /// 2. 없으면 에러표시, 시작버튼 비활성화
    /// 3. 있으면 오늘의 날짜 표시, 시작버튼 활성화
    /// 4. 시작버튼 클릭시 새 공정을 생성, DB에 입력
    ///     공정코드 : PRC20210618001(PRC+yyyy+MM+dd+NNN)
    /// 5. 공정처리 애니메이션 시작
    /// 6. 로드타임 후 애니메이션 중지
    /// 7. 센서링값 리턴될때까지 대기
    /// 8. 센서링 결과값에 따라서 생산품 색상 변경
    /// 9. 현재 공정의 DB값 업데이트
    /// 10. 결과 레이블 값 수정/표시
    /// </summary>
    public partial class ProcessView : Page
    {
        // 금일 일정
        private Model.Schedules currSchedules;

        public ProcessView()
        {
            InitializeComponent();
        }

        private async void  Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                currSchedules = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(Commons.PLANTCODE))
                                .Where(s => s.SchDate.Equals(DateTime.Parse(today))).FirstOrDefault();
                if (currSchedules == null)  // b == true
                {
                    await Commons.ShowMessageAsync("공정", "공정계획이 없습니다. 계획일정을 먼저 입력하세요");
                    // TODO : 시작버튼 비활성화
                    LblProcessDate.Content = string.Empty;
                    BtnStartProcess.IsEnabled = false;
                    return;
                }
                else 
                {
                    // 공정계획 표시
                    MessageBox.Show($"{today} 공정 시작합니다.");
                    LblProcessDate.Content = currSchedules.SchDate.ToString("yyyy년 MM월 dd일");
                    LblSchLoadTime.Content = $"{currSchedules.SchLoadTime} 초";
                    LblSchAmount.Content= $"{currSchedules.SchAmount} 개";
                    BtnStartProcess.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 ProcessView Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnEditMyAccount_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new EditAccount()); // 계정정보 수정 화면으로 변경
        }

        private void BtnStartProcess_Click(object sender, RoutedEventArgs e)
        {
            // 기어 애니메이션 속성
            DoubleAnimation da = new DoubleAnimation();
            
            da.From = 0;
            da.To = 360;    // 360도 회전
            da.Duration = TimeSpan.FromSeconds(currSchedules.SchLoadTime);    // 일정 계획 로드타임
            //da.RepeatBehavior = RepeatBehavior.Forever;   // 연속해서 계속 돔

            RotateTransform rt = new RotateTransform(); // 회전 움직임
            Gear1.RenderTransform = rt;
            Gear1.RenderTransformOrigin = new Point(0.5, 0.5);  // 회전 중심
            Gear2.RenderTransform = rt;
            Gear2.RenderTransformOrigin = new Point(0.5, 0.5);

            rt.BeginAnimation(RotateTransform.AngleProperty, da);   // 애니메이션 속성 설정(회전)

            // 제품 애니메이션 속성
            DoubleAnimation ma = new DoubleAnimation();

            ma.From = 138;  // 시작 위치
            ma.To = 525;    // 옮겨지는 x값의 최대값
            ma.Duration = TimeSpan.FromSeconds(currSchedules.SchLoadTime);  // 일정 계획 로드타임
            //ma.AccelerationRatio = 0.5;   // 가속
            //ma.AutoReverse = true;        // 왕복 운동

            Product.BeginAnimation(Canvas.LeftProperty, ma);    // 애니메이션 속성 설정
        }
    }
}
