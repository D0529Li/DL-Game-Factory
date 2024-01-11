using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DL_Game_Project
{
    public class SnakeGameViewModel
    {
        public int Score { get; set; }
        public bool ResumeButtonVisibility { get; set; }
        public bool PauseButtonVisibility { get; set; }
        public bool NewGameButtonVisibility { get; set; }
        public bool RecordsButtonVisibility { get; set; }
        public bool ExitButtonVisibility { get; set; }



        public SnakeGameViewModel()
        {
            SetVisibility_Options(true);
        }

        private void SetVisibility_Options(bool visibilityOption)
        {
            NewGameButtonVisibility = visibilityOption;
            RecordsButtonVisibility = visibilityOption;
            ExitButtonVisibility = visibilityOption;
        }
    }
}
