using UnityEngine;
using UnityEngine.UI;

public class MedianFilter : MonoBehaviour {

    [SerializeField]
    public RawImage proceedImage;

    public void ApplyEffect(Texture2D image) {

        Texture2D newImage = new Texture2D(image.width, image.height);

        for (int x = 1; x < image.width - 1; x++) {
            for (int y = 1; y < image.height - 1; y++) {
                float[] medianRGB = MedianValue(image, x, y);
                newImage.SetPixel(x, y, new Color(medianRGB[0], medianRGB[1], medianRGB[2]));
            }
        }

        newImage.Apply();
        proceedImage.texture = newImage;
    }

    private float[] MedianValue(Texture2D image, int x, int y) {

        Color color1 = image.GetPixel(x - 1, y - 1);
        Color color2 = image.GetPixel(x, y - 1);
        Color color3 = image.GetPixel(x + 1, y - 1);
        Color color4 = image.GetPixel(x - 1, y);
        Color color5 = image.GetPixel(x, y);
        Color color6 = image.GetPixel(x + 1, y);
        Color color7 = image.GetPixel(x - 1, y + 1);
        Color color8 = image.GetPixel(x, y + 1);
        Color color9 = image.GetPixel(x + 1, y + 1);

        float[] red = { color1.r, color2.r, color3.r, color4.r, color5.r, color6.r, color7.r, color8.r, color9.r };
        float[] green = { color1.g, color2.g, color3.g, color4.g, color5.g, color6.g, color7.g, color8.g, color9.g };
        float[] blue = { color1.b, color2.b, color3.b, color4.b, color5.b, color6.b, color7.b, color8.b, color9.b };

        float medianR = SortingMedian(red);
        float medianG = SortingMedian(green);
        float medianB = SortingMedian(blue);

        float[] median = { medianR, medianG, medianB };

        return median;
    }

    private float SortingMedian(float[] array) {
        float temp = 0;

        for (int i = 0; i < array.Length; i++) {
            for (int j = 0; j < array.Length - 1; j++) {
                if (array[j] > array[j + 1]) {
                    temp = array[j + 1];
                    array[j + 1] = array[j];
                    array[j] = temp;
                }
            }
        }

        float sortingMedian = array[4];

        return sortingMedian;
    }

}
