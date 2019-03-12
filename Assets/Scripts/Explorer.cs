using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;

public class Explorer : MonoBehaviour
{
    string openPath;
    string savePath;
    public RawImage[] images;
    public Text size;

    public BarChartController barChartController;
    public GameObject toolsPanel;

    [SerializeField]
    EffectsController effectsController;

    private void Start() {
        toolsPanel.SetActive(false);
    }

    public void OpenExplorer() {
        openPath = EditorUtility.OpenFilePanel("Open", "/", "jpg");
        GetImage();
    }

    public void OpenSave() {
        savePath = EditorUtility.SaveFilePanel("Save", "", "image", "jpg");
        if (savePath.Length != 0) {
            SaveImage();
        }
    }

    private void SaveImage() {
        Texture2D texture = (Texture2D)images[1].texture;
        var pngData = texture.EncodeToPNG();
        if (pngData != null) {
            File.WriteAllBytes(savePath, pngData);
        }
    }

    private void GetImage() {
        if(openPath != null) {
            UpdateImage();
            toolsPanel.SetActive(true);
        }
    }

    private void UpdateImage() {
        WWW www = new WWW("file:///" + openPath);
        foreach (RawImage image in images) {
            image.rectTransform.sizeDelta = new Vector2(www.texture.width, www.texture.height);
            image.texture = www.texture;
            SizeLimit(images);
        }
        ShowSize(www.texture.width, www.texture.height);
        effectsController.image = www.texture;

        barChartController.image = www.texture;
        barChartController.DrawBarChart(www.texture);
    }

    private void SizeLimit(RawImage[] images) {
        foreach (RawImage image in images) {
            if (image.rectTransform.sizeDelta.x / 2 > 1280 || image.rectTransform.sizeDelta.y / 2 > 720) {
                image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x / 3, image.rectTransform.sizeDelta.y / 3);
            }
        }
    }

    private void ShowSize(int width, int height) {
        size.text = width.ToString() + " x " + height.ToString();
    }
}
