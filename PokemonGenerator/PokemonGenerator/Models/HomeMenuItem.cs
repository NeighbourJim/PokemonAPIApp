using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGenerator.Models
{
    public enum MenuItemType
    {
        Random,
        Favourite,
        Generator
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
