using UnityEngine;
using UnityEngine.UI;

public class BlackWhite : MonoBehaviour {

    [SerializeField]
    public RawImage proceedImage;

    public void ApplyEffect(Texture2D image) {
        Texture2D newImage = new Texture2D(image.width, image.height);
        for (int i = 0; i < image.width; i++) {
            for (int j = 0; j < image.height; j++) {
                Color color = image.GetPixel(i, j);
                
                float blue = color.r * 255;
                float red = color.g * 255;
                float green = color.b * 255;
                
                float wb = (red * 0.299f + green * 0.587f + blue * 0.114f) / 255;

                float newRed = wb;
                float newGreen = wb;
                float newBlue = wb;

                Color newColor = new Color(newRed, newGreen, newBlue);
                newImage.SetPixel(i, j, newColor);
            }
        }
        newImage.Apply();
        proceedImage.texture = newImage;
    }
}
