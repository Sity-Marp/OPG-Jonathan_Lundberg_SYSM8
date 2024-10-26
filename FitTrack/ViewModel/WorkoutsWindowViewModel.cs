using FitTrack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    internal class WorkoutsWindowViewModel
    {
        public string LoggedInUsername => User.CurrentUser?.Username ?? "Guest";
    }
}
