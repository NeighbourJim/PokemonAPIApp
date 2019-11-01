using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PokemonGenerator.Models
{
    public static class LabelHelper
    {
        public static Color TypeToColour(MonType t)
        {
            switch (t)
            {
                case MonType.bug:
                    return Color.GreenYellow;
                case MonType.dark:
                    return Color.DarkViolet;
                case MonType.dragon:
                    return Color.Indigo;
                case MonType.electric:
                    return Color.Goldenrod;
                case MonType.fairy:
                    return Color.LightPink;
                case MonType.fighting:
                    return Color.SaddleBrown;
                case MonType.fire:
                    return Color.OrangeRed;
                case MonType.flying:
                    return Color.LightSkyBlue;
                case MonType.ghost:
                    return Color.BlueViolet;
                case MonType.grass:
                    return Color.ForestGreen;
                case MonType.ground:
                    return Color.SandyBrown;
                case MonType.ice:
                    return Color.LightBlue;
                case MonType.normal:
                    return Color.Black;
                case MonType.poison:
                    return Color.MediumPurple;
                case MonType.psychic:
                    return Color.DeepPink;
                case MonType.rock:
                    return Color.SlateGray;
                case MonType.steel:
                    return Color.Gray;
                case MonType.water:
                    return Color.RoyalBlue;
                default:
                    return Color.Black;
            }
        }
    }
}
