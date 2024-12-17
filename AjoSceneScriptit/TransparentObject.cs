using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    public float fadeSpeed, fadeAmount;
    //float originalOpacity;
    //Renderer renderer;
    Material[] Mats;
    public bool DoFade = false;
    private Dictionary<Material, float> originalOpacities = new Dictionary<Material, float>();

void Start()
{
    Mats = GetComponent<Renderer>().materials;
    foreach (Material mat in Mats) {
        originalOpacities[mat] = mat.color.a;
    }
}

    void Update()
    {
        if (DoFade) {
            FadeNow();
        }
        else {
            ResetFade();
        }
    }
    void FadeNow() {
    foreach (Material mat in Mats) {
        float originalAlpha = originalOpacities[mat]; // Haetaan materiaalin alkuperäinen alfa-arvo
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, 
            Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime)); // Fade-arvoa lähestytään
        mat.color = smoothColor;
    }
}

void ResetFade() {
    foreach (Material mat in Mats) {
        float originalAlpha = originalOpacities[mat];
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
            Mathf.Lerp(currentColor.a, originalAlpha, fadeSpeed * Time.deltaTime));
        mat.color = smoothColor;
    }
}

}
