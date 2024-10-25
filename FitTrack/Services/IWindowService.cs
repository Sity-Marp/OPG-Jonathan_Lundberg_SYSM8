using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Services
{
    public interface IWindowService
    {
        void OpenWindow<T>() where T : System.Windows.Window, new();
        void CloseWindow();
        void OpenAndCloseWindow<T>() where T : System.Windows.Window, new();
    }
}
