using System;
using System.Windows;
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
                    try
                    {
                        CrawlResult = await _model.ExecuteCrawlingAsync();
                    }
                    catch (CrawlConfigException e)
                    {
                        StatusValue = $"Config error, Restart your program! ({e.Message})";
                    }
                    catch (Exception e)
                    {
                        StatusValue = $"Error! {e.Message}";
                    }
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

        private string _statusValue;
        public string StatusValue
        {
            get { return _statusValue; }
            set
            {
                if (_statusValue != value)
                {
                    _statusValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _clickerCount = 0;
        private CrawlResult _crawlResult;
    }
}
