using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PokemonGenerator.Interfaces
{
    public interface SQLiteInterface
    {
        SQLiteAsyncConnection GetConnection();
    }
}
