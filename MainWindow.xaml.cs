using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using gamanetApp.Model;
using gamanetApp.ViewModel;

namespace gamanetApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PersonViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new PersonViewModel();
            _viewModel.Persons = LoadPersons();
            DataContext = _viewModel;

        }
        private void OnFilterClick(object sender, RoutedEventArgs e)
        {
            _viewModel.FilterCountry = (string)this.FilterCountryBox.SelectedItem;
        }
        private void OnSortClick(object sender, RoutedEventArgs e) 
        {
            _viewModel.SortBy = (string)this.SortByBox.SelectedItem;
        }
        private List<Person> LoadPersons()
        {
            //Read the data from csv file
            var reader = new StreamReader("persons.csv");
            var lines = reader.ReadToEnd().Split('\n');

            //Create a list of person objects
            var persons = new List<Person>();
            for(int i =0; i < lines.Length; i++)
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
    }
}
