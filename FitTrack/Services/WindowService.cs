using FitTrack;
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
    
        //  method to open any window
        public void OpenWindow<T>() where T : Window, new()
        {
            // Create and open the new window
            var window = new T();
            window.Show();

        }

        public void OpenWindow<T, TParameter>(TParameter parameter) where T : Window
        {
            var window = (T)Activator.CreateInstance(typeof(T), parameter);
            window.Show();
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

        //Opens a new window and closes the old one.
        public void OpenAndCloseWindow<T>() where T : Window, new()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            // Create and open the new window
            var window = new T();
            window.Show();

            // Close the current window (if needed)

            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }




    }
}


