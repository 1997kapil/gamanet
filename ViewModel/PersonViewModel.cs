using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gamanetApp.Model;

namespace gamanetApp.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private List<Person> _persons;
        private string _filterCountry;
        private string _sortBy;
        public List<Person> Persons
        {
            get { return _persons; }
            set { 
                _persons = value;
                NotifyPropertyChanged("Persons");

            }
        }
        public string FilterCountry
        {
            get { return _filterCountry; }
            set
            {
                _filterCountry = value;
                FilterPersons();
            }
        }
        public string SortBy
        {
            get { return _sortBy; }
            set
            {
                _sortBy = value;
                SortPersons();
            }
        }
        public PersonViewModel()
        {
            Persons = LoadPersons(persons);
        }
        private void FilterPersons()
        {
            if (FilterCountry.Length == 0)
            {
                Persons = _persons;
            }
            else
            {
                Persons = _persons.Where( p => p.Country == FilterCountry ).ToList();
            }
        }
        private void SortPersons()
        {

        if (SortBy == "Name")
            {
                Persons.Sort((p1,p2) =>p1.Name.CompareTo(p2.Name )) ;
            }
        else if (SortBy =="Country")
            {
                Persons.Sort((p1,p2) => p1.Country.CompareTo(p2.Country )) ;
            }
        }    
        private List<Person> LoadPersons(List<Person> persons)
        {
            //Read the data from csv files
            string filePath = @"persons.csv";
            var reader = new StreamReader(filePath);
            var lines = reader.ReadToEnd().Split('\n');

            //create a list of person objects
            var persons = new List<Person>();
            for (int i = 0;i < lines.Length; i++)
            {
                var line = lines[i].Split(',');
                persons.Add(new Person
                {
                    Name = line[0],
                    Country = line[1],
                    Phone = line[2]
                });
            }
            return persons;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        { if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
                
        }
    }
}
