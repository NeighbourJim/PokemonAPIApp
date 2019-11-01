using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PokemonGenerator;
using PokemonGenerator.Models;
using Xamarin.Forms;

namespace UnitTests
{
    [TestClass]
    public class PokemonUnitTests
    {
        Pokemon pagePokemon;

        [TestMethod]
        public void TestCreate()
        {
            pagePokemon = new Pokemon();
            Assert.AreEqual(pagePokemon.DexNum, -1);
            Assert.AreEqual(pagePokemon.Name, "?NAME?");

            Assert.AreEqual(pagePokemon.PrimaryType, MonType.none);
            Assert.AreEqual(pagePokemon.SecondaryType, MonType.none);

            Assert.AreEqual(pagePokemon.HP, 0);
            Assert.AreEqual(pagePokemon.Atk, 0);
            Assert.AreEqual(pagePokemon.Def, 0);
            Assert.AreEqual(pagePokemon.SpAtk, 0);
            Assert.AreEqual(pagePokemon.SpDef, 0);
            Assert.AreEqual(pagePokemon.Speed, 0);
        }

        [TestMethod]
        public void TestAPI()
        {
            pagePokemon = new Pokemon();
            Assert.IsNotNull(GetRandomPokemon().Result, "API returned no data.");
            Assert.IsNotNull(GetRandomPokemon().Result.id, "ID is null.");
            Assert.IsInstanceOfType((int)GetRandomPokemon().Result.id, typeof(int), "ID not integer, malformed data received.");
            Assert.IsTrue(GetRandomPokemon().Result.id > 0, "ID not greater than 0.");
            Assert.IsTrue(GetRandomPokemon().Result.id < 800, "ID not less than 800.");

            pagePokemon.PrimaryType = GetRandomPokemon().Result.types[0].type.name;
            Assert.IsTrue(pagePokemon.PrimaryType != MonType.none, "Type value is none");
        }

        [TestMethod]
        public void TestPokemonSet()
        {
            SetPokemonData(GetRandomPokemon().Result);

            Assert.AreNotEqual(pagePokemon.DexNum, -1);
            Assert.AreNotEqual(pagePokemon.Name, "?NAME?");

            Assert.AreNotEqual(pagePokemon.PrimaryType, MonType.none);
            Assert.AreNotEqual(pagePokemon.SecondaryType, MonType.none);

            Assert.AreNotEqual(pagePokemon.HP, 0);
            Assert.AreNotEqual(pagePokemon.Atk, 0);
            Assert.AreNotEqual(pagePokemon.Def, 0);
            Assert.AreNotEqual(pagePokemon.SpAtk, 0);
            Assert.AreNotEqual(pagePokemon.SpDef, 0);
            Assert.AreNotEqual(pagePokemon.Speed, 0);
        }

        [TestMethod]
        public void TestLabelHelper()
        {
            Assert.AreEqual(LabelHelper.TypeToColour(MonType.bug), Color.GreenYellow);
            Assert.AreEqual(LabelHelper.TypeToColour(MonType.fire), Color.OrangeRed);
            Assert.AreEqual(LabelHelper.TypeToColour(MonType.water), Color.RoyalBlue);
            Assert.AreEqual(LabelHelper.TypeToColour(MonType.electric), Color.Goldenrod);
        }

        #region Non-Test Methods
        public async Task<dynamic> GetRandomPokemon()
        {
            dynamic result = null;
            HttpClient client = new HttpClient();
            string request = string.Format("https://pokeapi.co/api/v2/pokemon/{0}/", RandomMonNumber());
            var response = await client.GetAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<dynamic>(responseString);
            }
            else
            {
                return result;
            }
        }

        private int RandomMonNumber()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            return r.Next(1, 800);
        }

        private void SetPokemonData(dynamic result)
        {
            pagePokemon = new Pokemon();
            pagePokemon.Name = result.name;
            pagePokemon.DexNum = result.id;
            pagePokemon.SpriteURL = result.sprites.front_default;
            pagePokemon.ShinySpriteURL = result.sprites.front_shiny;
            pagePokemon.DisplayingShiny = false;

            if (result.types[0].slot > 1)
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
        }
        #endregion
    }
}
