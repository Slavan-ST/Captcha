using ReactiveUI;
using System.Threading;
using System.Windows.Input;

namespace Sample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Check = ReactiveCommand.Create(() =>
            {
                if (IsVerified)
                {
                    Message = "The captcha has been passed!" + "  InputText: " + Text + "  is:" + IsVerified.ToString();
                }
                else
                {
                    Message = "The captcha has not been passed! Please repeat!" + "  InputText: " + Text + "  is:" + IsVerified.ToString();
                }
            });
        }

        public ICommand Check { get; set; }

        string _text = "";
        string _message = "";
        bool _isVerified = false;


        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        public bool IsVerified
        {
            get => _isVerified;
            set => this.RaiseAndSetIfChanged(ref _isVerified, value);
        }

    }
}
