using System;
using System.Collections.Generic;

namespace KeepFarming.Util
{
    public class CookingIngredientComparer : IEqualityComparer<CookingIngredientCD>
    {
        public bool Equals(CookingIngredientCD x, CookingIngredientCD y)
        {
            return x.brightestColor.Equals(y.brightestColor) &&
                   x.brightColor.Equals(y.brightColor) &&
                   x.darkColor.Equals(y.darkColor) &&
                   x.darkestColor.Equals(y.darkestColor);
        }

        public int GetHashCode(CookingIngredientCD obj)
        {
            return HashCode.Combine(obj.brightestColor, obj.brightColor, obj.darkColor, obj.darkestColor);
        }
    }
}