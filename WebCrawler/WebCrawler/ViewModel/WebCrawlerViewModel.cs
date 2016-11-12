using System;
using System.Windows.Input;
using WebCrawler.Model;

namespace WebCrawler.ViewModel
{
    class WebCrawlerViewModel : BaseViewModel
    {

        private readonly WebCrawlerModel _model;

        public WebCrawlerViewModel()
        {
            _model = new WebCrawlerModel();
            ClickerCommand = new RelayCommand(() =>
            {
                _clickerCount++;
                OnPropertyChanged("ClickerValue");
            });
            ClickerResetCommand = new RelayCommand(() =>
            {
                _clickerCount = 0;
                OnPropertyChanged("ClickerValue");
            });
        }

        // Clicker stuff
        public string ClickerValue => (_clickerCount == 0) ? "Click Me!" : Convert .ToString(_clickerCount);
        public ICommand ClickerCommand { get; }
        public ICommand ClickerResetCommand { get; }
        private int _clickerCount = 0;
    }
}
