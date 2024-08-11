using UnityEngine;

public class Movement : MonoBehaviour
{
    //variables////////////////////////////////

    //PARAMETERS - for tuning, typically set in the editor

    //CACHE - e.g. references for readability or speed

    //STATE - private instance (member) variables

    [SerializeField] float maxFuel = 100;
    [SerializeField] float thrustForce = 1250.0f;
    [SerializeField] float glideSpeed = 150.0f;
    [SerializeField] float amountOfFuel = 100;
    [SerializeField] float amountOfDecaying = 25;
    [SerializeField] float fuelBoost = 100;

    [SerializeField] AudioClip boostEngineAudio;
    [SerializeField] AudioClip fuelAudio;
    [SerializeField] FuelBar fuelBar;
    AudioSource componentAudioSource;
    AudioSource fuelAudioSource;
    Rigidbody rb;

    bool boostInput;
    float glideInput;
    class InputString {
        public static string engineBoost = "EngineBoost";
        public static string horizontal = "Horizontal";
    }
    
    void Start(){
        rb = GetComponent<Rigidbody>();
        componentAudioSource = GetComponent<AudioSource>();
        fuelAudioSource = gameObject.AddComponent<AudioSource>();
        amountOfFuel = maxFuel;
        fuelBar.SetMaxFuel(maxFuel);
    }
    void FixedUpdate() {
        ProcessBoost();
        ProcessGlide();
        fuelBar.SetFuel(amountOfFuel);
    }
    void Update() {
        ReadInput();
        CheckFuel();
        PlayBoostAudio();
    }
    /////////////////////////////////////////
    private void ReadInput() {
        boostInput = Input.GetButton(InputString.engineBoost);
        glideInput = Input.GetAxisRaw(InputString.horizontal);
    }
    void ProcessBoost() {
        if (amountOfFuel <= 0) return;
        if (!boostInput) {
            return;
        }
        rb.AddRelativeForce(0, thrustForce, 0);
        DecayingFuel();
    }
    void ProcessGlide() {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        float glideValue = glideInput * glideSpeed;
        transform.Rotate(0, 0, -glideValue);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
    void PlayBoostAudio() {
        if (amountOfFuel <= 0) {
            componentAudioSource.Stop();
            return;
        }
        if (!(Input.GetButton(InputString.engineBoost))) {
            componentAudioSource.Stop();
            return;
        }
        if (componentAudioSource.isPlaying) return;
        componentAudioSource.loop = true;
        componentAudioSource.PlayOneShot(boostEngineAudio);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Fuel")) {
            fuelAudioSource.PlayOneShot(fuelAudio);
            amountOfFuel += fuelBoost;
        }
    }
    private void CheckFuel() {
        if (amountOfFuel > 100) {
          amountOfFuel = 100;
        }
        if (amountOfFuel <= 0){
          amountOfFuel = 0;
        } 
    }
    void DecayingFuel() {
        amountOfFuel -= amountOfDecaying * Time.deltaTime;
    }
    public float GetAmountOfFuel() {
        return amountOfFuel;
    }
}
