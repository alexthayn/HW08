using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW08.EventArgs;
using HW08.Views;

namespace HW08.Services
{
    public class EditWindowController : IEditWindowController
    {
        public bool? ShowDialog(OpenEditWindowArgs args)
        {
            AddContact editWindow = new AddContact();
            return editWindow.ShowDialog();
        }
    }
}
