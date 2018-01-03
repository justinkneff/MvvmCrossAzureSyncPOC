using Windows.UI.Xaml.Controls;
using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Platform;
using MvvmCrossSyncPOC.UWP.Modules;

namespace MvvmCrossSyncPOC.UWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new MvvmCrossSyncPOC.Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxIoCProvider CreateIocProvider()
        {
            return new AutofacMvxIocProvider(AutofacBootstrapper.Initialize());
        }
    }
}
