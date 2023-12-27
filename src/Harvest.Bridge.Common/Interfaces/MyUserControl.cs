using Harvest.Bridge.Common.Models;
using System.Windows.Forms;

namespace Harvest.Bridge.Common.Interfaces
{
    public interface MyUserControl
    {
        void InitControl(object obj, Form prntForm);

        void Save();
    }
}
