using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;
using Xamarin.Forms;
using System.IO;
using PokemonGenerator.Interfaces;

[assembly: Dependency(typeof(PokemonGenerator.Droid.SQLiteDatabase))]

namespace PokemonGenerator.Droid
{
    class SQLiteDatabase : SQLiteInterface
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "database.db2");
            return new SQLiteAsyncConnection(path);
        }
    }
}