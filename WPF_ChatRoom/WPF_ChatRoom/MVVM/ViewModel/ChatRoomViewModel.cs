using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_ChatRoom.Core;
using WPF_ChatRoom.MVVM.Model;

namespace WPF_ChatRoom.MVVM.ViewModel
{
    public class ChatRoomViewModel : ObservableObject
    {
        #region 成员
        public ObservableCollection<ContactModel> Contacts { get; set; }

        private MessageManager _messageManager;


        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }


        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        #endregion
        public ChatRoomViewModel()
        {
            Contacts = new ObservableCollection<ContactModel>();
            _messageManager = MessageManager.Instance;

            //VM里的OMR订阅信息模块的OMR，即VM给信息模块提供了一个按钮，当信息模块的OMR触发，同时触发VM的OMR
            _messageManager.OnMessageReceived += OnMessageReceived;

            SendCommand = new RelayCommand(o =>
            {
                _messageManager.SendMsg(Message, SelectedContact.id);
                SelectedContact.Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false
                });

                Message = "";
            });

            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Alex{i}",
                    id = i,
                    ImageSource = "pack://application:,,,/Icons/picture.png",
                    Messages = new ObservableCollection<MessageModel> { new MessageModel
                    {
                        Username = $"Alex{i}",
                        UsernameColor = "#409aff",
                        ImageSource = "pack://application:,,,/Icons/picture.png",
                        Message = "first",
                        Time = DateTime.Now,
                        IsNativeOrigin = true,
                    } }
                });
            }

        }

        public void OnMessageReceived(int id, string message)
        {
            foreach (var contact in Contacts)
            {
                if (id == contact.id)
                {

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        contact.Messages.Add(new MessageModel
                        {
                            Username = contact.Username,
                            ImageSource = contact.ImageSource,
                            UsernameColor = "#409aff",
                            Message = message,
                            Time = DateTime.Now,
                            IsNativeOrigin = false,
                        });
                    });

                }
            }
        }
    }
}
