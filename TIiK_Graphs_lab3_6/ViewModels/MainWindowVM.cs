using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class MainWindowVM : BindableBase
    {
        public MainWindowVM()
        {
            _matrixPageVm = new MatrixPageVM();
        }

        private readonly MatrixPageVM _matrixPageVm;

        public MatrixPageVM MatrixPageVm { get; }
    }
}
