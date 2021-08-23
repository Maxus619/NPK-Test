using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NPK_Test.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace NPK_Test.ViewModel
{
    class VisitsListViewModel : ViewModelBase
    {
        private string _selectedPatient;
        public string SelectedPatient { get => _selectedPatient; set { Set(ref _selectedPatient, value); LoadVisits(); } }

        DatabaseEntities databaseEntities;

        private ObservableCollection<Patients> _lstAllPatients;
        private ObservableCollection<Visits> _lstAllVisits;
        private List<Visits> _lstVisits;
        private List<Patients> _lstPatients;

        public ObservableCollection<Patients> LstAllPatients { get => _lstAllPatients; set => Set(ref _lstAllPatients, value); }
        public ObservableCollection<Visits> LstAllVisits { get => _lstAllVisits; set => Set(ref _lstAllVisits, value); }
        public List<Visits> LstVisits { get => _lstVisits; set => Set(ref _lstVisits, value); }
        public List<Patients> LstPatients { get => _lstPatients; set => Set(ref _lstPatients, value); }

        public VisitsListViewModel()
        {
            databaseEntities = new DatabaseEntities();
            LoadPatients();
        }

        public void LoadPatients()
        {
            LstAllPatients = new ObservableCollection<Patients>(databaseEntities.Patients);
        }

        public void LoadVisits()
        {
            LstAllVisits = new ObservableCollection<Visits>(databaseEntities.Visits);
            LstVisits = new List<Visits>(LstAllVisits).FindAll(FindByID);
            LstPatients = new List<Patients>(LstAllPatients).FindAll(FindByID);
        }

        private bool FindByID(Visits vs)
        {
            if (vs.PatientID == Guid.Parse(SelectedPatient))
                return true;
            else
                return false;
        }
        private bool FindByID(Patients pt)
        {
            if (pt.PatientID == Guid.Parse(SelectedPatient))
                return true;
            else
                return false;
        }

        public static void ExportToXML(string fileName, List<Visits> ListOfVisits, List<Patients> ListOfPatients)
        {
            string strBegin = $@"
                <Patient>
                    <Property Name='PatientID' Type='uniqueidentifier' Value='{ListOfPatients[0].PatientID}'/>
                    <Property Name='LastName' Type='nvarchar' MaxLength='50' Value='{ListOfPatients[0].LastName}'/>
                    <Property Name='FirstName' Type='nvarchar' MaxLength='50' Value='{ListOfPatients[0].FirstName}'/>
                    <Property Name='SurName' Type='nvarchar' MaxLength='50' Value='{ListOfPatients[0].SurName}'/>
                    <Property Name='Birthday' Type='date' Value='{ListOfPatients[0].Birthday}'/>
                    <Visits>
            ";
            
            string strMid = "";
            foreach (Visits visit in ListOfVisits)
            {
                strMid += $@"
                    <Visit>
                        <Property Name='VisitID' Type='uniqueidentifier' Value='{visit.VisitID}'/>
                        <Property Name='DateVisit' Type='date' Value='{visit.DateVisit}'/>
                        <Property Name='Diagnose' Type='nvarchar' MaxLength='36' Value='{visit.Diagnose}'/>
                        <Property Name='PatientID' Type='uniqueidentifier' Value='{visit.PatientID}'/>
                    </Visit>
                ";
            }

            string strEnd = "</Visits></Patient>";

            var xml = XElement.Parse(strBegin + strMid + strEnd);
            xml.Save(fileName);
            MessageBox.Show("XML файл создан");
        }

        public ICommand Refresh { get { return new RelayCommand(() => { LoadPatients(); LoadVisits(); }); } }

        public ICommand XML { get { return new RelayCommand(() => { ExportToXML("Visits.xml", LstVisits, LstPatients); }); } }
    }
}
