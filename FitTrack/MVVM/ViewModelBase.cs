using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FitTrack.Model;
using System.Collections.ObjectModel;

namespace FitTrack.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
