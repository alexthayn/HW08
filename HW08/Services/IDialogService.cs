using System;

namespace HW08.Services
{
    public interface IDialogService
    {
        void Warning(string message);

        void Exception(Exception ex);
    }
}
