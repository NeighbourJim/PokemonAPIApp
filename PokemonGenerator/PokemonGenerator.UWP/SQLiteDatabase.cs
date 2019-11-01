using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Xamarin.Forms;
using PokemonGenerator.Interfaces;

[assembly: Dependency(typeof(PokemonGenerator.UWP.SQLiteDatabase))]

namespace PokemonGenerator.UWP
{
    class SQLiteDatabase : SQLiteInterface
    {
        public SQLiteAsyncConnection GetConnection()
        {
            return new SQLiteAsyncConnection("database.db2");
        }
    }
}
