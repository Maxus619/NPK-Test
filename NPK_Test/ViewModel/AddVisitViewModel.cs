using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NPK_Test.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace NPK_Test.ViewModel
{
    class AddVisitViewModel : ViewModelBase
    {
        private string _dateVisit;
        private string _diagnose;
        private string _selectedPatient;

        public string DateVisit { get => _dateVisit; set => Set(ref _dateVisit, value); }
        public string Diagnose { get => _diagnose; set => Set(ref _diagnose, value); }
        public string SelectedPatient { get => _selectedPatient; set => Set(ref _selectedPatient, value); }

        DatabaseEntities databaseEntities;

        private ObservableCollection<Patients> _lstPatients;
        public ObservableCollection<Patients> LstPatients { get => _lstPatients; set => Set(ref _lstPatients, value); }

        public AddVisitViewModel()
        {
            databaseEntities = new DatabaseEntities();
            LoadPatients();
        }

        public void LoadPatients()
        {
            LstPatients = new ObservableCollection<Patients>(databaseEntities.Patients);
        }

        private void AddVisit()
        {
            try
            {
                databaseEntities.Visits.Add(new Visits { VisitID = Guid.NewGuid(),
                                                         DateVisit = DateTime.Parse(_dateVisit),
                                                         Diagnose = _diagnose,
                                                         PatientID = Guid.Parse(SelectedPatient) });
                databaseEntities.SaveChanges();
                MessageBox.Show("Посещение добавлено");

                DateVisit = null;
                Diagnose = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка добавления в базу");
            }
        }

        public ICommand Submit { get { return new RelayCommand(() => AddVisit()); } }
    }
}
