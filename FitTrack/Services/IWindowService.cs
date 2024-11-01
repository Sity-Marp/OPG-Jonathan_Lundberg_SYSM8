using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Services
{
    public interface IWindowService
    {
        //idea was to used this to deal with opening windows but time restraints.
        void OpenWindow<T>() where T : System.Windows.Window, new();

        //overload the above method so we can take both window type and a parameter. (for windows that requires a parameter like WorkoutDetails)
        void OpenWindow<T, TParameter>(TParameter parameter) where T : System.Windows.Window;

        void CloseWindow();
        void OpenAndCloseWindow<T>() where T : System.Windows.Window, new();

    }
}
