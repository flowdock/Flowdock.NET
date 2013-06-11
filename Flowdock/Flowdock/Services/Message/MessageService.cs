using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Flowdock.Services.Message {
    public class MessageService : IMessageService {
        
        public void ShowError(Exception e) {
            MessageBox.Show(e.Message);
        }
    }
}
