using System;

namespace CustomColors
{
    public enum Color
    {
        None = 0,
        Yellow = 1,
        Red = 2,
        Blue = 3,
        Orange = 4,
        Green = 5,
        Violet = 6
    }

    public class CustomColor
    {
        private static readonly UnityEngine.Color[] Colors =
        {
            UnityEngine.Color.white,
            UnityEngine.Color.yellow,
            UnityEngine.Color.red,
            UnityEngine.Color.blue,
            new(255, 165, 0, 255),
            UnityEngine.Color.green,
            new(238, 130, 238, 255),
        };
        
        private int _colorId;

        public static CustomColor operator +(CustomColor a, CustomColor b)
        {
            if (a._colorId == 0)
            {
                return b._colorId == 0 ? a : b;
            }

            if (b._colorId == 0)
            {
                return a._colorId == 0 ? b : a;
            }
            
            var customColor = 1 + a._colorId + b._colorId;

            return customColor > 6 || a._colorId == b._colorId? a : new CustomColor(customColor);
        }

        public static bool operator ==(CustomColor a, CustomColor b)
        {
            if (ReferenceEquals(a, null))
            {
                throw new Exception("Null reference On First Object");
            } 
            
            if (ReferenceEquals(b, null))
            {
                throw new Exception("Null reference On Seconda Object");
            }
            
            return a._colorId == b._colorId;
        }
        
        public static bool operator !=(CustomColor a, CustomColor b)
        {
            if (ReferenceEquals(a, null))
            {
                throw new Exception("Null reference On First Object");
            } 
            
            if (ReferenceEquals(b, null))
            {
                throw new Exception("Null reference On Second Object");
            }
            
            return a._colorId != b._colorId;
        }
        
        public static explicit operator UnityEngine.Color(CustomColor a)
        {
            return Colors[a._colorId];
        }

        private CustomColor(int colorIdId)
        {
            _colorId = colorIdId;
        }

        public CustomColor(Color color)
        {
            _colorId = (int)color;
        }
    }
}

