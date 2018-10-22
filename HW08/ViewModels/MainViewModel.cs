using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW08.ViewModels
{
    class MainViewModel
    {
        public AddContactCommand AddContactCommand { get; set; }

        public MainViewModel()
        {
            AddContactCommand = new AddContactCommand();
        }
    }
}
