using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
//using FormsChatApp.Annotations;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using FormsChatApp.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FormsChatApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public Action<ObservableCollection<Message>> OnMessageInsertAction;

        public Socket socket;

        private string _messageText;
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();


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

        public Command PageLoadCqommand => new Command(() =>
        {
            Debug.WriteLine("Page Loaded");


            ProcessSockets();
        });

        public Command SubmitCommand => new Command(() => {
            Debug.WriteLine("Submit: " + MessageText);
            var message = new Message("Stian", MessageText);

            var json = JsonConvert.SerializeObject(message);
            Debug.WriteLine("send:" + json);
            socket.Emit("input", json);
        });

        public Command<Message> DeleteMessageCommand => new Command<Message>(message =>
        {
            var json = JsonConvert.SerializeObject(message);
            Debug.WriteLine("delete: " + json);
            socket.Emit("delete", json);
        });



        public void ProcessSockets()
        {

            socket = IO.Socket("http://stiancronje.com:4000");

            socket.On("output", data => GetOutput(data));
            socket.On("deleted", data => DeleteMessage(data));
        }

        public void GetOutput(object data)
        {
            var messages = JsonConvert.DeserializeObject<ObservableCollection<Message>>(data.ToString());
            Console.WriteLine("got output: " + messages.Count);
            for (int i = 0; i < messages.Count; i++)
            {
                Debug.WriteLine("json: " + messages[i]);
                Messages.Add(messages[i]);
            }
            if (OnMessageInsertAction != null) OnMessageInsertAction(Messages);
            //OnMessageInsertAction?.Invoke(Messages);
        }

        void DeleteMessage(object data)
        {
            var message = JsonConvert.DeserializeObject<Message>(data.ToString());
            Messages.Remove(message);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
