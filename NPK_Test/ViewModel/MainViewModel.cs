using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPK_Test.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private Page AddPatient;
        private Page AddVisit;
        private Page PatientsList;
        private Page VisitsList;

        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged(() => CurrentPage);
            }
        }

        public MainViewModel()
        {
            AddPatient = new View.AddPatientView();
            AddVisit = new View.AddVisitView();
            PatientsList = new View.PatientsListView();
            VisitsList = new View.VisitsListView();
        }

        public ICommand btnAddPatient_Click { get { return new RelayCommand(() => CurrentPage = AddPatient); } }
        public ICommand btnAddVisit_Click { get { return new RelayCommand(() => CurrentPage = AddVisit); } }
        public ICommand btnPatients_Click { get { return new RelayCommand(() => CurrentPage = PatientsList); } }
        public ICommand btnVisits_Click { get { return new RelayCommand(() => CurrentPage = VisitsList); } }
    }
}