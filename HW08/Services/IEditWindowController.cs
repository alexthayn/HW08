using HW08.EventArgs;

namespace HW08.Services
{
    public interface IEditWindowController
    {
        /// <summary>
        /// Show the edit window
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool? ShowDialog(OpenEditWindowArgs args);
    }
}
