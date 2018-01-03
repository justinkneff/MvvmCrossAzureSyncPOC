using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCrossSyncPOC.Core.AzureAbstractions;
using MvvmCrossSyncPOC.Core.Services;

namespace MvvmCrossSyncPOC.Core.ViewModels
{
    public class FirstViewModel
        : MvxViewModel
    {
        private readonly IToDoItemRepositoryService _todoService;
        private AppSettings _appSettings;
        private readonly ICloudServiceContainer _cloudServiceContainer;

        private bool hasRun { get; set; } = false;

        public FirstViewModel(IToDoItemRepositoryService todoService, AppSettings appSettings, ICloudServiceContainer cloudServiceContainer)
        {
            _todoService = todoService;
            _appSettings = appSettings;
            _cloudServiceContainer = cloudServiceContainer;
            DoStuffSelected = new MvxCommand(async () => await ExecuteDoStuff());
        }

        private async Task ExecuteDoStuff()
        {
            //If has run before make sure to remove the instantiation of the tables
            if (hasRun)
            {
                //ex: user log's and you don't terminate the app so it can the re-initialize the tables.
                await _cloudServiceContainer.GetCloudService().ReinitializeTables();
                await _todoService.ReinitializeTable();
            }
            //get the offline database name
            _appSettings.OfflineFile = hello;
            //pull from the server
            await _todoService.PullAsync();
            //mark that we have done this before so we konw on consecutive runs to re-establish the database
            hasRun = true;
        }

        string hello = "Hello";
        public string Hello
        {
            get { return hello; }
            set
            {
                SetProperty(ref hello, value);
                RaisePropertyChanged(nameof(Hello));
            }
        }

        string hellolbl = "Type the offline file name";
        public string HelloLabel
        {
            get { return hellolbl; }
            set
            {
                SetProperty(ref hellolbl, value);
                RaisePropertyChanged(nameof(HelloLabel));
            }
        }

        public IMvxCommand DoStuffSelected { get; }
    }
}
