using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_ChatRoom.MVVM.Model;

namespace WPF_ChatRoom.MVVM.ViewModel
{
    public class ChatRoomViewModel
    {
        public ObservableCollection<ContactModel> Contacts { get; set; }

        public ChatRoomViewModel()
        {
            Contacts = new ObservableCollection<ContactModel>();

            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Alex{i}",
                    ImageSource = "https://tse1.explicit.bing.net/th/id/OIP.BzjY_OsxeGhvVgd-4uP1cAHaE7?rs=1&pid=ImgDetMain&o=7&rm=3",
                    Messages = new ObservableCollection<MessageModel> { new MessageModel
                    {
                        Username = $"Alex{i}",
                        UsernameColor = "#409aff",
                        ImageSource = "https://tse1.explicit.bing.net/th/id/OIP.BzjY_OsxeGhvVgd-4uP1cAHaE7?rs=1&pid=ImgDetMain&o=7&rm=3",
                        Message = "first",
                        Time = DateTime.Now,
                        IsNativeOrigin = true,
                    } }
                });
            }
        }
    }
}
