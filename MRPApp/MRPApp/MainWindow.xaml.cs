﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MRPApp.View;
using MRPApp.View.Account;
using MRPApp.View.Store;
using MRPApp.View.Setting;
using MRPApp.View.Schedule;

namespace MRPApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }       

        private void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            //// 로그인된 계정 표시
            //if (Commons.LOGINED_USER != null)
            //    BtnLoginedId.Content = $"{Commons.LOGINED_USER.UserEmail} ({Commons.LOGINED_USER.UserName})";
        }

        private async void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var result = await this.ShowMessageAsync("종료", "프로그램을 종료하시겠습니까?",
                MessageDialogStyle.AffirmativeAndNegative, null);

            if (result == MessageDialogResult.Affirmative)
                Application.Current.Shutdown(); // 프로그램 종료
        }

        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new SettingList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnSetting_Click : {ex}");
                this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActiveControl.Content = new ScheduleList();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnSchedule_Click : {ex}");
                this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            }
        }

        private void BtnMonitoring_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
