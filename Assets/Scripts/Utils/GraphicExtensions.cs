using UnityEngine.UI;

namespace Utils
{
    public static class GraphicExtensions
    {
        public static void SetAlpha(this Graphic graphic, float a)
        {
            var color = graphic.color;
            color.a = a;
            graphic.color = color;
        }
    }
}