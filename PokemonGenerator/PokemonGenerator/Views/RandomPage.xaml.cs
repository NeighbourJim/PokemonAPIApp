using PokemonGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using SQLite;

namespace PokemonGenerator.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RandomPage : ContentPage
	{
        Pokemon pagePokemon;

		public RandomPage ()
		{
			InitializeComponent ();
            GetRandomPokemon();
            try
            {
                DatabaseManager.ConnectToDatabase();
                DatabaseManager.Create();
            }
            catch(SQLiteException ex)
            {
                DisplayMessage(ex.Message, true);
            }
		}

        private int RandomMonNumber()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            return r.Next(1, 800);
        }

        private string Capitalise(string str)
        {
            if(!string.IsNullOrEmpty(str))
            {
                string workString = str.Replace('-', ' ');
                workString = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(workString.ToLower());
                return workString.Replace(' ', '-');
            }
            return string.Empty;
        }

        private async void GetRandomPokemon()
        {
            if (!actIndicator.IsRunning)
            {
                ClearUI();
                actIndicator.IsRunning = true;
                HttpClient client = new HttpClient();
                string reqest = string.Format("https://pokeapi.co/api/v2/pokemon/{0}/", RandomMonNumber());
                var response = await client.GetAsync(reqest);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(responseString);
                    SetPokemonData(result);
                }
                else
                {
                    pagePokemon = null;
                }
                actIndicator.IsRunning = false;
                DisplayMessage("", false);
            }
            else
            {
                DisplayMessage("Still loading data...", true);
            }
        }

        private void SetPokemonData(dynamic result)
        {
            pagePokemon = new Pokemon();
            pagePokemon.Name = Capitalise((string)result.name);
            pagePokemon.DexNum = result.id;
            pagePokemon.SpriteURL = result.sprites.front_default;
            pagePokemon.ShinySpriteURL = result.sprites.front_shiny;
            pagePokemon.DisplayingShiny = false;

            if(result.types[0].slot > 1)
            {
                pagePokemon.PrimaryType = result.types[1].type.name;
                pagePokemon.SecondaryType = result.types[0].type.name;
            }
            else
            {
                pagePokemon.PrimaryType = result.types[0].type.name;
            }
            pagePokemon.HP = result.stats[5].base_stat;
            pagePokemon.Atk = result.stats[4].base_stat;
            pagePokemon.Def = result.stats[3].base_stat;
            pagePokemon.SpAtk = result.stats[2].base_stat;
            pagePokemon.SpDef = result.stats[1].base_stat;
            pagePokemon.Speed = result.stats[0].base_stat;

            UpdateUI();
        }

        private void ClearUI()
        {
            nameLabel.Text = "Loading Pokemon...";

            pTypeSpan.Text = "";
            typeSeparator.Text = "";
            sTypeSpan.Text = "";

            spriteImage.Source = ImageSource.FromResource("PokemonGenerator.Images.placeholderLoading.png");

            #region Set Stat Labels
            hpTitleSpan.Text = "";
            hpValueSpan.Text = "";
            atkTitleSpan.Text = "";
            atkValueSpan.Text = "";
            defTitleSpan.Text = "";
            defValueSpan.Text = "";
            spAtkTitleSpan.Text = "";
            spAtkValueSpan.Text = "";
            spDefTitleSpan.Text = "";
            spDefValueSpan.Text = "";
            speedTitleSpan.Text = "";
            speedValueSpan.Text = "";
            #endregion
        }

        private void UpdateUI()
        {
            nameLabel.Text = string.Format("#{0} - {1}", pagePokemon.DexNum, Capitalise(pagePokemon.Name));

            pTypeSpan.Text = Capitalise(pagePokemon.PrimaryType.ToString());
            spriteImage.Source = pagePokemon.DisplayingShiny ? pagePokemon.ShinySpriteURL : pagePokemon.SpriteURL;
            

            if (pagePokemon.SecondaryType != MonType.none)
            {
                sTypeSpan.Text = Capitalise(pagePokemon.SecondaryType.ToString());
                typeSeparator.Text = " / ";
            }
            else
            {
                sTypeSpan.Text = string.Empty;
                typeSeparator.Text = string.Empty;
            }
            pTypeSpan.TextColor = LabelHelper.TypeToColour(pagePokemon.PrimaryType);
            sTypeSpan.TextColor = LabelHelper.TypeToColour(pagePokemon.SecondaryType);

            #region Set Stat Labels
            hpTitleSpan.Text = "HP: ";
            hpValueSpan.Text = pagePokemon.HP.ToString();
            atkTitleSpan.Text = "Attack: ";
            atkValueSpan.Text = pagePokemon.Atk.ToString();
            defTitleSpan.Text = "Defense: ";
            defValueSpan.Text = pagePokemon.Def.ToString();
            spAtkTitleSpan.Text = "Special Attack: ";
            spAtkValueSpan.Text = pagePokemon.SpAtk.ToString();
            spDefTitleSpan.Text = "Special Defense: ";
            spDefValueSpan.Text = pagePokemon.SpDef.ToString();
            speedTitleSpan.Text = "Speed: ";
            speedValueSpan.Text = pagePokemon.Speed.ToString();
            #endregion
        }

        private void FavouriteCurrentPokemon()
        {
            if(!actIndicator.IsRunning)
            {
                try
                {
                    DatabaseManager.Insert(pagePokemon);
                    List<Pokemon> t = DatabaseManager.Retrieve();
                    DisplayMessage("Pokemon added to favourites.", false);
                }
                catch(SQLiteException ex)
                {
                    DisplayMessage(ex.Message, true);
                }
            }
            else
            {
                DisplayMessage("Still loading data...", true);
            }
        }

        private void DisplayMessage(string message, bool isError)
        {
            messageLabel.Text = message;
            if(isError)
            {
                messageLabel.TextColor = Color.Red;
            }
            else
            {
                messageLabel.TextColor = Color.Green;
            }
        }

        private void FavButton_Clicked(object sender, EventArgs e)
        {
            FavouriteCurrentPokemon();
        }

        private void RandButton_Clicked(object sender, EventArgs e)
        {
            GetRandomPokemon();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            pagePokemon.DisplayingShiny = !pagePokemon.DisplayingShiny;
            UpdateUI();
        }
    }
}