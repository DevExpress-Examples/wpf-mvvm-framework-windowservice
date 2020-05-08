using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DevExpress.Mvvm;

namespace DXSample.ViewModels {
    public class ChildViewModel : ViewModelBase {
        protected ICurrentWindowService CurrentWindowService { get { return this.GetService<ICurrentWindowService>(); } }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand TemporarilyHideWindowCommand { get; private set; }
        public string Caption {
            get { return GetProperty(() => Caption); }
            set { SetProperty(() => Caption, value); }
        }
        public WindowState WindowState {
            get { return GetProperty(() => WindowState); }
            set { SetProperty(() => WindowState, value); }
        }
        public ChildViewModel() {
            CloseWindowCommand = new DelegateCommand(CloseWindow);
            TemporarilyHideWindowCommand = new DelegateCommand(TemporarilyHideWindow);
        }
        void CloseWindow() {
            this.CurrentWindowService.Close();
        }
        void TemporarilyHideWindow() {
            this.CurrentWindowService.Hide();
            var timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(3) };
            timer.Tick += (o, e) => {
                this.CurrentWindowService.SetWindowState(WindowState.Maximized);
                this.CurrentWindowService.Show();
                this.CurrentWindowService.Activate();
                timer.Stop();
            };
            timer.Start();
        }
    }
}