using UnityEngine;
using UnityEngine.UI;

public class BarChartController : MonoBehaviour
{
    public Texture2D image;
    [SerializeField]
    RawImage barChart;
    [SerializeField]
    GameObject barChartGO;
    Texture2D barTexture;

    float[] quantity = new float[256];
    int index;

    private void Start() {
        barChartGO.SetActive(false);
        index = 0;
    }

    public void DrawBarChart(Texture2D image) {

        barTexture = new Texture2D(256, 128);

        for (int i = 0; i < image.width; i++) {
            for(int j = 0; j < image.height; j++) {
                float H = 0, S = 0, V = 0;
                index = 0;
                Color.RGBToHSV(image.GetPixel(i, j), out H, out S, out V);
                index = (int)(H * 255);
                quantity[index]++;
            }
        }

        for (int i = 0; i < quantity.Length; i++) {
            int x = (int) (quantity[i] / 128);
            for (int j = 0; j < x; j++) {
                barTexture.SetPixel(i, j, Color.black);
            }
        }

        barChartGO.SetActive(true);
        barTexture.Apply();
        barChart.texture = barTexture;

    }
}
