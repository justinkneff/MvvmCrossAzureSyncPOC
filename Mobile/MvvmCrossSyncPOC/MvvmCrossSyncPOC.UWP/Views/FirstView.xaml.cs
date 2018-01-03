using Windows.UI.Xaml;
using MvvmCross.Uwp.Views;
using MvvmCrossSyncPOC.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using MvvmCross.Platform;

namespace MvvmCrossSyncPOC.UWP.Views
{
    public sealed partial class FirstView : MvxWindowsPage
    {
        public FirstView()
        {
            this.InitializeComponent();
        }
        
    }
}
