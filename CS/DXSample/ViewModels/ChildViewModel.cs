using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Windows;
using System.Windows.Threading;

namespace DXSample.ViewModels {
    public class ChildViewModel : ViewModelBase {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        public string Caption {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public WindowState WindowState {
            get { return GetValue<WindowState>(); }
            set { SetValue(value); }
        }
        [Command]
        public void CloseWindow() {
            CurrentWindowService.Close();
        }
        [Command]
        public void TemporarilyHideWindow() {
            CurrentWindowService.Hide();
            var timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(3) };
            timer.Tick += (o, e) => {
                CurrentWindowService.SetWindowState(WindowState.Maximized);
                CurrentWindowService.Show();
                CurrentWindowService.Activate();                
                timer.Stop();
            };
            timer.Start();
        }
    }
}
