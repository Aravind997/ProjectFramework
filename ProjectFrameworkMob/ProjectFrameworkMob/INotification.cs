using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFrameworkMob
{
    public interface INotification
    {
        void CreateNotification(String title, String message, string Data);
    }
}
