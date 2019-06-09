using DevExpress.Mvvm;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            _matrixPageVm = new MatrixPageVM();
            _efficiencyPageVm = new EfficiencyPageVM();  
        }

        

        #region Backing Fields
        private readonly MatrixPageVM _matrixPageVm;
        private readonly EfficiencyPageVM _efficiencyPageVm;
        

        #endregion

        

    }
}
