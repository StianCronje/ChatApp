using System.ComponentModel;
using System.Runtime.CompilerServices;
using FormsChatApp.Annotations;
using Xamarin.Forms;

namespace FormsChatApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _message;

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                if (value == _message) return;
                _message = value;
                OnPropertyChanged();
            }
        }

        public Command TestCommand
        {
            get
            {
                return new Command(() =>
                {
                    Message = "Testing: " + Name;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
