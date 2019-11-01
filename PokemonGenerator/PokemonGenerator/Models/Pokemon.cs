using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace PokemonGenerator.Models
{
    public class Pokemon 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        #region Getters and Setters
        public int DexNum { get; set; }
        public string Name { get; set; }

        public string SpriteURL { get; set; }
        public string ShinySpriteURL { get; set; }

        public bool DisplayingShiny { get; set; }

        private MonType primaryType = MonType.none;
        public MonType PrimaryType
        {
            get
            {
                return primaryType;
            }
            set
            {
                primaryType = (MonType)Enum.Parse(typeof(MonType), value.ToString());
            }
        }
        private MonType secondaryType = MonType.none;
        public MonType SecondaryType
        {
            get
            {
                return secondaryType;
            }
            set
            {
                secondaryType = (MonType)Enum.Parse(typeof(MonType), value.ToString());
            }
        }

        public int HP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int SpAtk { get; set; }
        public int SpDef { get; set; }
        public int Speed { get; set; }
        #endregion

        #region Constructor

        public Pokemon()
        {
            DexNum = -1;
            Name = "?NAME?";
            PrimaryType = MonType.none;
            SecondaryType = MonType.none;

            HP = 0;
            Atk = 0;
            Def = 0;
            SpAtk = 0;
            SpDef = 0;
            Speed = 0;
        }

        #endregion
    }
}
