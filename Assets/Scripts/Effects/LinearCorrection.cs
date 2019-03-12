using UnityEngine;
using UnityEngine.UI;

public class LinearCorrection : MonoBehaviour {

    [SerializeField]
    public RawImage proceedImage;

    public Slider maxSlider;
    public Slider minSlider;

    [SerializeField]
    float maxV = 1f;
    [SerializeField]
    float minV = 0f;

    public void SetMaxValue() {
        maxV = maxSlider.value;
    }

    public void SetMinValue() {
        minV = minSlider.value;
    }

    public void ApplyEffect(Texture2D image) {
        Texture2D newImage = new Texture2D(image.width, image.height);

        float[,] hue = new float[image.width, image.height];
        float[,] saturation = new float[image.width, image.height];
        float[,] value = new float[image.width, image.height];

        FindAllHSV(image, hue, saturation, value);
        if (maxV == 1f && minV == 0f) FindValueLimits(image, value);  
        SetNewValue(image, value);
        SetNewColor(image, newImage, hue, saturation, value);
    }

    private void FindAllHSV(Texture2D image, float[,] hue, float[,] saturation, float[,] value) {
        for (int x = 0; x < image.width; x++) {
            for (int y = 0; y < image.height; y++) {
                float H = 0, S = 0, V = 0;
                Color.RGBToHSV(image.GetPixel(x, y), out H, out S, out V);

                hue[x, y] = H;
                saturation[x, y] = S;
                value[x, y] = V;
            }
        }
    }

    private void FindValueLimits(Texture2D image, float[,] value) {
        for (int x = 0; x < image.width; x++) {
            for (int y = 0; y < image.height; y++) {
                if (value[x, y] > maxV) {
                    maxV = value[x, y];
                }
                else if (value[x, y] < minV) {
                    minV = value[x, y];
                }
            }
        }
    }

    private void SetNewValue(Texture2D image, float[,] value) {
        for (int x = 0; x < image.width; x++) {
            for (int y = 0; y < image.height; y++) {
                float buf = (value[x,y] - minV) * ((1 - 0) / (maxV - minV));

                value[x, y] = buf;
            }
        }
    }

    private void SetNewColor(Texture2D image,Texture2D newImage, float[,] hue, float[,] saturation, float[,] value) {

        Color[,] newColor = new Color[image.width, image.height];

        for (int x = 0; x < image.width; x++) {
            for (int y = 0; y < image.height; y++) {
                newImage.SetPixel(x, y, Color.HSVToRGB(hue[x, y], saturation[x, y], value[x, y]));
            }
        }

        newImage.Apply();
        proceedImage.texture = newImage;
    }
}
