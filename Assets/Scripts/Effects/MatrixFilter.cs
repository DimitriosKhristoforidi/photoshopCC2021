using UnityEngine;
using UnityEngine.UI;

public class MatrixFilter : MonoBehaviour {

    [SerializeField]
    public RawImage proceedImage;

    public void ApplyEffect(Texture2D image, float[] colorMul) {
        Texture2D newImage = new Texture2D(image.width, image.height);
        float colorSum = 0;

        for (int i = 0; i < colorMul.Length; i++) {
            colorSum += colorMul[i];
        }

        for (int x = 0; x < image.width; x++) {
            for (int y = 0; y < image.height; y++) {

                Color color1 = image.GetPixel(x - 1, y - 1);
                Color color2 = image.GetPixel(x, y - 1);
                Color color3 = image.GetPixel(x + 1, y - 1);
                Color color4 = image.GetPixel(x - 1, y);
                Color color5 = image.GetPixel(x, y);
                Color color6 = image.GetPixel(x + 1, y);
                Color color7 = image.GetPixel(x - 1, y + 1);
                Color color8 = image.GetPixel(x, y + 1);
                Color color9 = image.GetPixel(x + 1, y + 1);

                CheckBorders(color1, color2, color3, color4, color5, color6, color7, color8, color9);

                int red = (int)((color1.r * 255 * colorMul[0] + color2.r * 255 * colorMul[1] + color3.r * 255 * colorMul[2] + color4.r * 255 * colorMul[3] + color5.r * 255 * colorMul[4] + color6.r * 255 * colorMul[5] + color7.r * 255 * colorMul[6] + color8.r * 255 * colorMul[7] + color9.r * 255 * colorMul[8]) / colorSum);
                int green = (int)((color1.g * 255 * colorMul[0] + color2.g * 255 * colorMul[1] + color3.g * 255 * colorMul[2] + color4.g * 255 * colorMul[3] + color5.g * 255 * colorMul[4] + color6.g * 255 * colorMul[5] + color7.g * 255 * colorMul[6] + color8.g * 255 * colorMul[7] + color9.g * 255 * colorMul[8]) / colorSum);
                int blue = (int)((color1.b * 255 * colorMul[0] + color2.b * 255 * colorMul[1] + color3.b * 255 * colorMul[2] + color4.b * 255 * colorMul[3] + color5.b * 255 * colorMul[4] + color6.b * 255 * colorMul[5] + color7.b * 255 * colorMul[6] + color8.b * 255 * colorMul[7] + color9.b * 255 * colorMul[8]) / colorSum);

                Color newColor = new Color(red / 255f, green / 255f, blue / 255f);

                newImage.SetPixel(x, y, newColor);
            }
        }

        newImage.Apply();
        proceedImage.texture = newImage;
    }

    public void CheckBorders(Color color1, Color color2, Color color3, Color color4, Color color5, Color color6, Color color7, Color color8, Color color9) {
        if (color1 == null) {
            if (color2 == null) {
                color1 = color5;
            } else {
                color1 = color2;
            }
        }
        if (color2 == null) {
            color2 = color5;
        }
        if (color3 == null) {
            if (color2 == null) {
                color3 = color5;
            } else {
                color3 = color2;
            }
        }
        if (color4 == null) {
            color4 = color5;
        }
        if (color6 == null) {
            color6 = color5;
        }
        if (color7 == null) {
            if (color8 == null) {
                color7 = color5;
            } else {
                color7 = color8;
            }
        }
        if (color8 == null) {
            color8 = color5;
        }
        if (color9 == null) {
            if (color8 == null) {
                color9 = color5;
            } else {
                color9 = color8;
            }
        }
    }

    private void LimitColor(int red, int green, int blue) {
        if (red > 255) {
            red = 255;
        }
        else if (red < 0) {
            red = 0;
        }
        if (green > 255) {
            green = 255;
        }
        else if (green < 0) {
            green = 0;
        }
        if (blue > 255) {
            blue = 255;
        }
        else if (blue < 0) {
            blue = 0;
        }
    }


}
