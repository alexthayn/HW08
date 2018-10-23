using HW08.Models;

namespace HW08.EventArgs
{
    public enum ActionType
    {
        /// <summary>
        /// Add a new contact
        /// </summary>
        Add,

        /// <summary>
        /// Edit a contact
        /// </summary>
        Edit
    }

    public class OpenEditWindowArgs
    {
        /// <summary>
        /// If <see cref="ActionType"/> is Edit, then the value for this property needs to be provided
        /// </summary>
        public Contact Contact { get; set; }

        public ActionType Type { get; set; }
    }
}
