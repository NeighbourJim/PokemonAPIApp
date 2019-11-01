using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using PokemonGenerator.Interfaces;

namespace PokemonGenerator.Models
{
    public static class DatabaseManager
    {
        static SQLiteAsyncConnection connection = null;

        public static void ConnectToDatabase()
        {
            connection = DependencyService.Get<SQLiteInterface>().GetConnection();
        }

        public static void Create()
        {
            connection.CreateTableAsync<Pokemon>();
        }

        public static List<Pokemon> Retrieve()
        {
            CreateTableIfNotExist();
            return connection.Table<Pokemon>().ToListAsync().Result;
        }

        public static void Update(Pokemon pokemon)
        {
            CreateTableIfNotExist();
            connection.UpdateAsync(pokemon);
        }

        public static void Delete(Pokemon pokemon)
        {
            CreateTableIfNotExist();
            connection.DeleteAsync(pokemon);
        }

        public static void Insert(Pokemon pokemon)
        {
            CreateTableIfNotExist();
            connection.InsertAsync(pokemon);
        }

        private static bool DoesTableExist()
        {
            return connection.Table<Pokemon>() != null;
        }

        private static void CreateTableIfNotExist()
        {
            if(!DoesTableExist())
            {
                Create();
            }
        }
    }
}
