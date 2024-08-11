using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [SerializeField] float delayTime = 1f;

    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip victoryAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;
    AudioSource componentAudioSource;

    bool collisionDisabled = false;
    bool isTransitioning = false;

    void Start() {
        componentAudioSource = GetComponent<AudioSource>();
    }
    void Update() {
        DebugKeys();
    }
    void DebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled; //toggle collisionDisalbe
        }
    }
    void OnCollisionEnter(Collision collision) {
        if (isTransitioning || collisionDisabled){
            return;
        } 
        if (collision.gameObject.CompareTag("Finish")) {
            NextLevelSequence();
        }else if (collision.gameObject.CompareTag("Friendly")) {
            return;
        } else {
            CrashSequence();
        }
    }
    void CrashSequence() {
        GetComponent<Movement>().enabled = false;
        componentAudioSource.Stop();
        componentAudioSource.loop = false;
        componentAudioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        isTransitioning = true;
        Invoke(nameof(TriggerDeath), delayTime);
    }
    void NextLevelSequence() {
        GetComponent<Movement>().enabled = false;
        componentAudioSource.Stop();
        componentAudioSource.loop = false;
        componentAudioSource.PlayOneShot(victoryAudio);
        victoryParticles.Play();
        isTransitioning = true;
        Invoke(nameof(LoadNextLevel), delayTime);
    }
    void TriggerDeath() {
        int indexOfCurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(indexOfCurrentScene);
    }
    void LoadNextLevel() {
        int indexOfCurrentScene = SceneManager.GetActiveScene().buildIndex;
        int indexOfNextScene = indexOfCurrentScene + 1;
        if (indexOfNextScene == SceneManager.sceneCountInBuildSettings) {
            indexOfNextScene = 0;
        }
        SceneManager.LoadScene(indexOfNextScene);
    }
    public bool IsTransitioning() {
        return isTransitioning;
    }
}

