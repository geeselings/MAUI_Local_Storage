using System.Collections.ObjectModel;
using MauiLocalStorage.DataAccess;
using MauiLocalStorage.Models;

namespace MauiLocalStorage
{
    public partial class MainPage : ContentPage
    {
        PersonData personData;
        public ObservableCollection<Person> People { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            personData = new PersonData();
            BindingContext = this;
            UpdatePeopleList();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                await DisplayAlert("Error", "First name cannot be empty", "OK");
                return;
            }

            if(string.IsNullOrEmpty(txtLastName.Text))
            {
                await DisplayAlert("Error", "Last name cannot be empty", "OK");
                return;
            }

            if (dpDateOfBirth.Date > DateTime.Today)
            {
                await DisplayAlert("Error", "Date of birth cannot be in the future", "OK");
                return;
            }

            var person = new Person
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                DoB = dpDateOfBirth.Date
            };
            await personData.SavePersonAsync(person);
            UpdatePeopleList();
        }

        private async void UpdatePeopleList()
        {
            var people = await personData.GetPeopleAsync();
            People.Clear();
            foreach (var person in people)
            {
                People.Add(person);
            }
        }
    }
}
