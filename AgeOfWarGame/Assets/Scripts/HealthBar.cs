using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Gradient gradient;
    private Slider slider;
    private Image fillImage;


    // Start is called before the first frame update
    void Awake()
    {
        this.slider = GetComponent<Slider>();
        this.fillImage = this.transform.Find("Fill").GetComponent<Image>();
    }

    public void SetHealth(float health)
    {
        this.slider.value = health;
        this.fillImage.color = gradient.Evaluate(this.slider.normalizedValue);
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.slider.maxValue = maxHealth;
        SetHealth(maxHealth);
    }
}
