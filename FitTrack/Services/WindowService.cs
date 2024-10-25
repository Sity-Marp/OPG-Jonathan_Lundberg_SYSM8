using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Services
{
    public class WindowService : IWindowService
    {
        public void OpenWindow()
        {
            // create an instance of the new window
            var window = new RegisterWindow();


            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }


            //show new window

            window.ShowDialog();
        }

        public void CloseWindow()
        {
            //get reference to current window
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            //close window
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
