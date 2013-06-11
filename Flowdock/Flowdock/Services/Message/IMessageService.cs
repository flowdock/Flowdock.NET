using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Services.Message {
    public interface IMessageService {
        void ShowError(Exception e);
    }
}
