using PokemonGenerator.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokemonGenerator.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavouritePage : ContentPage
	{
        private List<Pokemon> monList = new List<Pokemon>();
        public ObservableCollection<Pokemon> monCollection = new ObservableCollection<Pokemon>();

        public FavouritePage ()
		{
			InitializeComponent ();
		}

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            lvMain.ItemsSource = monCollection;
            DatabaseManager.ConnectToDatabase();
            DatabaseManager.Create();
            UpdateList();
        }

        private void UpdateList()
        {
            monList = DatabaseManager.Retrieve();
            monCollection.Clear();
            foreach(Pokemon mon in monList)
            {
                monCollection.Add(mon);
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem menu = (MenuItem)sender;
                Pokemon mon = (Pokemon)menu.CommandParameter;
                DatabaseManager.Delete(mon);
                DisplayMessage("Item deleted successfully.", false);
            }
            catch (SQLiteException ex)
            {
                DisplayMessage(ex.Message, true);
            }

            UpdateList();
        }

        private void DisplayMessage(string message, bool isError)
        {
            messageLabel.Text = message;
            if (isError)
            {
                messageLabel.TextColor = Color.Red;
            }
            else
            {
                messageLabel.TextColor = Color.Green;
            }
        }
    }
}