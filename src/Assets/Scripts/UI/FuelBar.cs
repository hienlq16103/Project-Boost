using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour {
    Slider slider;
    void Awake() {
        slider = GetComponent<Slider>();
    }
    /// <summary>
    /// Set the current fuel to a specific value.
    /// </summary>
    /// <param name="fuel">The latest value of the current fuel.</param>
    public void SetFuel (float fuel) {
        slider.value = fuel;
    }
    public void SetMaxFuel(float maxFuel) {
        slider.maxValue = maxFuel;
        slider.value = maxFuel;
    }
}
