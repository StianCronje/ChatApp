using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FormsChatApp.Annotations;
using FormsChatApp.Models;
using Xamarin.Forms;

namespace FormsChatApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public Action<ObservableCollection<Message>> OnMessageInsertAction;

        private string _messageText;
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>()
        {
            new Message("Stian", "Some Text"),
            new Message("Stian", "Some Text 2"),
            new Message("Stian", "More Text"),
            new Message("Stian", "Testing"),
            new Message("Stian", "Hi there this is some long text")
        };
        

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set
            {
                if (Equals(value, _messages)) return;
                _messages = value;
                OnPropertyChanged();
            }
        }

        public string MessageText
        {
            get => _messageText;
            set
            {
                if (value == _messageText) return;
                _messageText = value;
                OnPropertyChanged();
            }
        }

        public Command SubmitCommand => new Command(() => {
            Debug.WriteLine("Submit: " + MessageText);
            Messages.Add(new Message("Stian", MessageText));
            OnMessageInsertAction?.Invoke(Messages);
        });

        public Command<Message> DeleteMessageCommand => new Command<Message>(message => {
            Messages.Remove(message);
        });


        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
