using System;
using System.Windows.Input;
using WebCrawler.Model;
using WebCrawlerCore;

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
            StartCommand = new AsyncCommand(async () =>
            {
                if (StartCommand.CanExecute)
                {
                    StartCommand.CanExecute = false;
                    CrawlResult = await _model.ExecuteCrawlingAsync();
                    StartCommand.CanExecute = true;
                }
            });
        }

        public AsyncCommand StartCommand { get; }

        public CrawlResult CrawlResult {
            get
            {
                return _crawlResult;
            }
            set
            {
                if (_crawlResult != value)
                {
                    _crawlResult = value;
                    OnPropertyChanged();
                }
            }
        }

        // Clicker stuff
        public string ClickerValue => (_clickerCount == 0) ? "Click Me!" : Convert .ToString(_clickerCount);
        public ICommand ClickerCommand { get; }
        public ICommand ClickerResetCommand { get; }
        private int _clickerCount = 0;
        private CrawlResult _crawlResult;
    }
}
