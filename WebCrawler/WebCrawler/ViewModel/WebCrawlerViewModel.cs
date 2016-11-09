using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawler.ViewModel
{
    class WebCrawlerViewModel : BaseViewModel
    {

        public WebCrawlerViewModel()
        {
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
