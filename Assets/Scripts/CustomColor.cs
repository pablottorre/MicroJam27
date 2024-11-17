using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
        Violet = 6,
        Rainbow = 7,
    }

    public class CustomColor
    {
        private static readonly UnityEngine.Color[] Colors =
        {
            UnityEngine.Color.white,
            UnityEngine.Color.yellow,
            UnityEngine.Color.red,
            UnityEngine.Color.blue,
            new(1.0f, 0.64f, 0.0f, 1f),
            UnityEngine.Color.green,
            new(0.47f, 0.16f, 0.55f, 1f),
            new(0f, 0f, 0f, 1f)
        };

        private int _colorId;
        
        public int ColorId => _colorId;

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

            if (a != b && a._colorId > 3 && b._colorId is < 4 and > 0)
            {
                return new CustomColor(Color.Rainbow);
            }

            var customColor = 1 + a._colorId + b._colorId;

            return customColor > 6 || a._colorId == b._colorId ? a : new CustomColor(customColor);
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

        public static CustomColor GetRandomColor(float chance)
        {
            var canSpawnMultiColor = Random.value < chance;
            
            var maxValue = Mathf.RoundToInt(chance * Colors.Length);
            maxValue = Mathf.Clamp(maxValue, 1, Colors.Length);
            
            maxValue = maxValue < 5 ? 4 : maxValue < 8 ? 7 : maxValue;

            if (maxValue < 5 && canSpawnMultiColor)
            {
                maxValue = 7;
            }
            
            return new CustomColor(Random.Range(1, maxValue));
        }
    }
}