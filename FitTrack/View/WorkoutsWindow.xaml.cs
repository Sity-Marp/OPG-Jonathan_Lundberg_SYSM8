﻿using FitTrack.Model;
using FitTrack.Services;
using FitTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitTrack.View
{
    /// <summary>
    /// Interaction logic for WorkoutsWindow.xaml
    /// </summary>
    public partial class WorkoutsWindow : Window
    {
        public WorkoutsWindow()
        {
            InitializeComponent();

            var windowService = new WindowService();
            var workoutManager = new WorkoutManager(); // Create an instance of WorkoutManager
            DataContext = new WorkoutsWindowViewModel(windowService, workoutManager);
        }
    }
}
