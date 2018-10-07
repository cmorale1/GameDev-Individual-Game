using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSliderController : MonoBehaviour {

    public Slider redSlider, greenSlider, blueSlider;
    private Color squareColor;
    private Image img;
    public string imgName;
    private bool pastStart = false;

    // Use this for initialization
    void Start()
    {
        img = GameObject.Find(imgName).GetComponent<Image>();
        squareColor = img.color;
        if(imgName == "TeamA")
        {
            redSlider.value = 0;
            greenSlider.value = 0.4857143f;
            blueSlider.value = 1f;
        } else if (imgName == "TeamB")
        {
            redSlider.value = 1f;
            greenSlider.value = 0.5f;
            blueSlider.value = 0f;
        }
        pastStart = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(pastStart)
        {
            squareColor.r = redSlider.value;
            squareColor.g = greenSlider.value;
            squareColor.b = blueSlider.value;
            img.color = squareColor;
            PlayerPrefs.SetFloat(imgName + "_RedValue", img.color.r);
            PlayerPrefs.SetFloat(imgName + "_GreenValue", img.color.g);
            PlayerPrefs.SetFloat(imgName + "_BlueValue", img.color.b);
        }
	}
}
