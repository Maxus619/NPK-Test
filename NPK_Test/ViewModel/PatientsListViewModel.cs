using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NPK_Test.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NPK_Test.ViewModel
{
    class PatientsListViewModel : ViewModelBase
    {
        DatabaseEntities databaseEntities;

        private string _selectedPatient;
        public string SelectedPatient { get => _selectedPatient; set { Set(ref _selectedPatient, value); LoadPatients(); } }

        private ObservableCollection<Patients> _lstAllPatients;
        public ObservableCollection<Patients> LstAllPatients { get => _lstAllPatients; set => Set(ref _lstAllPatients, value); }
        
        private List<Patients> _lstPatients;        
        public List<Patients> LstPatients { get => _lstPatients; set => Set(ref _lstPatients, value); }


        public PatientsListViewModel()
        {
            databaseEntities = new DatabaseEntities();
            SelectedPatient = "";
            LoadPatients();
        }

        public void LoadPatients()
        {
            LstAllPatients = new ObservableCollection<Patients>(databaseEntities.Patients);
            LstPatients = new List<Patients>(LstAllPatients).FindAll(FindByLastName);
        }

        public bool FindByLastName(Patients pt)
        {
            if (pt.LastName.ToLower().Contains(SelectedPatient.ToLower()))
                return true;
            else
                return false;
        }

        public ICommand Refresh { get { return new RelayCommand(() => LoadPatients()); } }
    }
}
