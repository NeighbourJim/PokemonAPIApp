using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SQLite;
using Xamarin.Forms;
using System.IO;
using PokemonGenerator.Interfaces;


[assembly: Dependency(typeof(PokemonGenerator.iOS.SQLiteDatabase))]

namespace PokemonGenerator.iOS
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