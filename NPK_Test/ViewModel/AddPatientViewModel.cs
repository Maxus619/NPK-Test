using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NPK_Test.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace NPK_Test.ViewModel
{
    class AddPatientViewModel : ViewModelBase
    {
        private string _lastname;
        private string _firstname;
        private string _surname;
        private string _birthday;

        public string LastName { get => _lastname; set => Set(ref _lastname, value); }
        public string FirstName { get => _firstname; set => Set(ref _firstname, value); }
        public string SurName { get => _surname; set => Set(ref _surname, value); }
        public string Birthday { get => _birthday; set => Set(ref _birthday, value); }

        DatabaseEntities databaseEntities;

        public AddPatientViewModel()
        {
            databaseEntities = new DatabaseEntities();
        }

        private void AddPatient()
        {
            try
            {
                databaseEntities.Patients.Add(new Patients { PatientID = Guid.NewGuid(), 
                                                             LastName = _lastname,
                                                             FirstName = _firstname,
                                                             SurName = _surname, 
                                                             Birthday = DateTime.Parse(_birthday) });
                databaseEntities.SaveChanges();
                MessageBox.Show("Пациент добавлен");

                LastName = null;
                FirstName = null;
                SurName = null;
                Birthday = null;
            }
            catch(Exception e)
            {
                MessageBox.Show("Ошибка добавления в базу");
            }
        }

        public ICommand Submit { get { return new RelayCommand(() => AddPatient()); } }
    }
}
