using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FewzionReport.Utils
{
    public sealed class ColorUtil
    {
        public static double[] ToHsv(Color color, bool norm = false)
        {
            double h = 0, s = 0, v = 0;
            double r = (double)color.R, g = (double)color.G, b = (double)color.B;
            //Blue Is the dominant color
            if ((b > g) && (b > r))
            {
                //Value is set as the dominant color
                v = b;
                if (v != 0)
                {
                    double min;
                    if (r > g) min = g;
                    else min = r;
                    //Delta is the difference between the most dominant color and the least dominant color. This will be used to compute saturation.
                    double delta = v - min;
                    if (delta != 0)
                    { s = (delta / v); h = 4 + (r - g) / delta; }
                    else
                    { s = 0; h = 4 + (r - g); }
                    //Hue is just the difference between the two least dominant colors offset by the dominant color. That is, here 4 puts hue in the blue range. Then red and green just tug it one way or the other. Notice if red and green are equal, hue will stick squerely on blue
                    h *= 60; if (h < 0) h += 360;
                    if (!norm) v = (v / 255);
                    else s *= (100);
                }
                else
                { s = 0; h = 0; }
            }
            //Green is the dominant color
            else if (g > r)
            {
                v = g;
                if (v != 0)
                {
                    double min;
                    if (r > b) min = b;
                    else min = r;
                    double delta = v - min;
                    if (delta != 0)
                    { s = (delta / v); h = 2 + (b - r) / delta; }
                    else
                    { s = 0; h = 2 + (b - r); }
                    h *= 60; if (h < 0) h += 360;
                    if (!norm) v = (v / 255);
                    else s *= (100);
                }
                else
                { s = 0; h = 0; }
            }
            //Red is the dominant color
            else
            {
                v = r;
                if (v != 0)
                {
                    double min;
                    if (g > b) min = b;
                    else min = g;
                    double delta = v - min;
                    if (delta != 0)
                    { s = (delta / v); h = (g - b) / delta; }
                    else
                    { s = 0; h = (g - b); }
                    h *= 60; if (h < 0) h += 360;
                    if (!norm) v = (v / 255);
                    else s *= (100);
                }
                else
                { s = 0; h = 0; }
            }
            return new double[] { h, s, v };
        }

        public static Color FromHsv(double h, double s, double v)
        {
            double r, g, b;
            if (v == 0)
            { r = 0; g = 0; b = 0; }
            else if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                double hf = h / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = v * (1 - s);
                double qv = v * (1 - s * f);
                double tv = v * (1 - s * (1 - f));
                switch (i)
                {
                    //Red is the dominant color
                    case 0:
                        r = v;
                        g = tv;
                        b = pv;
                        break;
                    //Green is the dominant color
                    case 1:
                        r = qv;
                        g = v;
                        b = pv;
                        break;
                    case 2:
                        r = pv;
                        g = v;
                        b = tv;
                        break;
                    //Blue is the dominant color
                    case 3:
                        r = pv;
                        g = qv;
                        b = v;
                        break;
                    case 4:
                        r = tv;
                        g = pv;
                        b = v;
                        break;
                    //Red is the dominant color
                    case 5:
                        r = v;
                        g = pv;
                        b = qv;
                        break;
                    //Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.
                    case 6:
                        r = v;
                        g = tv;
                        b = pv;
                        break;
                    case -1:
                        r = v;
                        g = pv;
                        b = qv;
                        break;
                    //The color is not defined, we should throw an error.
                    default:
                        throw new InvalidOperationException(String.Format("i Value error in Pixel conversion, Value is {0:d}", i));
                }
            }
            return Color.FromArgb((int)(r * 255.0F), (int)(g * 255.0F), (int)(b * 255.0F));
        }

        public static Color ColorInBetween(Color start, Color end, int percentage)
        {
            percentage = Math.Min(100, percentage);
            percentage = Math.Max(0, percentage);

            int steps = 101;

            var difference_R = (end.R - start.R) / (double)steps;
            var difference_G = (end.G - start.G) / (double)steps;
            var difference_B = (end.B - start.B) / (double)steps;

            var R = (int)Math.Round((percentage * difference_R) + start.R);
            var G = (int)Math.Round((percentage * difference_G) + start.G);
            var B = (int)Math.Round((percentage * difference_B) + start.B);

            return Color.FromArgb(R, G, B);
        }

        public static Color ContrastColor(Color color)
        {
            var r = color.R / 255.0;
            var g = color.G / 255.0;
            var b = color.B / 255.0;
            if (r <= 0.03928) r = r / 12.92;
            else r = Math.Pow((r + 0.055) / 1.055, 2.4);
            if (g <= 0.03928) g = g / 12.92;
            else g = Math.Pow((g + 0.055) / 1.055, 2.4);
            if (b <= 0.03928) b = b / 12.92;
            else b = Math.Pow((b + 0.055) / 1.055, 2.4);
            // ReSharper disable once InconsistentNaming
            var L = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            //return L > 0.179 ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);
            return L > 0.3 ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);
        }

        public static Color ContrastColor(Color color, double threshhold)
        {
            var r = color.R / 255.0;
            var g = color.G / 255.0;
            var b = color.B / 255.0;
            if (r <= 0.03928) r = r / 12.92;
            else r = Math.Pow((r + 0.055) / 1.055, 2.4);
            if (g <= 0.03928) g = g / 12.92;
            else g = Math.Pow((g + 0.055) / 1.055, 2.4);
            if (b <= 0.03928) b = b / 12.92;
            else b = Math.Pow((b + 0.055) / 1.055, 2.4);
            // ReSharper disable once InconsistentNaming
            var L = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            return L > threshhold ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);
        }
    }
}
