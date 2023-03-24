using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace DXSample.ViewModels {
    public class MainViewModel : ViewModelBase {
        protected IWindowService WindowService { get { return GetService<IWindowService>(); } }
        public ChildViewModel ChildWindowViewModel {
            get { return GetValue<ChildViewModel>(); }
            set { SetValue(value); }
        }

        [Command]
        public void ShowChildWindow() {
            if(ChildWindowViewModel == null) 
                ChildWindowViewModel = new ChildViewModel() { Caption = "Hello, World!" };                                                 
            WindowService.Show(ChildWindowViewModel);            
        }
        public bool CanShowChildWindow() {
            return !WindowService.IsWindowAlive;
        }

        [Command]
        public void CloseChildWindow() {
            ChildWindowViewModel = null;
            WindowService.Close();            
        }
        public bool CanCloseChildWindow() {
            return WindowService.IsWindowAlive;
        }
    }
}
