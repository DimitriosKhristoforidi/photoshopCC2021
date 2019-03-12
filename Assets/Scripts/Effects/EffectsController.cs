using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsController : MonoBehaviour {
    [SerializeField]
    Dropdown effects;

    public Texture2D image;

    public MatrixFilter sharpness;
    public LinearCorrection linearCorrection;
    public BlackWhite blackWhite;
    public MedianFilter medianFilter;

    float[] sharpnesColorMul = {0f,-1f,0f,
                                -1f,5f,-1f,
                                0f,-1f,0f};

    float[] blurColorMul = {0.0625f,0.125f,0.0625f,
                            0.125f,0.25f,0.125f,
                            0.0625f,0.125f,0.0625f};

    private void Start() {
        sharpness = FindObjectOfType<MatrixFilter>();
        linearCorrection = FindObjectOfType<LinearCorrection>();
        blackWhite = FindObjectOfType<BlackWhite>();
        medianFilter = FindObjectOfType<MedianFilter>();
    }

    public void ApplyEffect() {
        if(effects.value == 0) {
            sharpness.ApplyEffect(image, sharpnesColorMul);
        } else if(effects.value == 1) {
            medianFilter.ApplyEffect(image);
        } else if(effects.value == 2) {
            sharpness.ApplyEffect(image, blurColorMul);
        } else if(effects.value == 3) {
            linearCorrection.ApplyEffect(image);
        } else if(effects.value == 4) {
            blackWhite.ApplyEffect(image);
        }
    }
}
