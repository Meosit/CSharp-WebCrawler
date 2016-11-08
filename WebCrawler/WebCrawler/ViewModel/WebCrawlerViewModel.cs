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
            _clickerCommand = new RelayCommand(() =>
            {
                _clickerCount++;
                OnPropertyChanged("ClickerValue");
            });
            _clickerResetCommand = new RelayCommand(() =>
            {
                _clickerCount = 0;
                OnPropertyChanged("ClickerValue");
            });
        }

        public string ClickerValue
        {
            get
            {
                return (_clickerCount == 0) ? "Click Me!" : Convert .ToString(_clickerCount);
            }
        }

        public ICommand ClickerCommand
        {
            get
            {
                return _clickerCommand;
            }
        }

        public ICommand ClickerResetCommand
        {
            get
            {
                return _clickerResetCommand;
            }
        }

        private int _clickerCount = 0;
        private ICommand _clickerCommand;
        private ICommand _clickerResetCommand;

    }
}
