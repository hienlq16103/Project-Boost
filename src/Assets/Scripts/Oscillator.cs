using UnityEngine;

public class Oscillator : MonoBehaviour
{

  [SerializeField] Vector3 movementDirection = new Vector3(0, 5, 0);
  [SerializeField] float period = 2f;

  const float tau = Mathf.PI * 2;

  Vector3 startingPosition;
  Vector3 offset;
  float angularFrequency;
  float sineWave;

  private void Start()
  {
    startingPosition = transform.position;
  }
  private void Update()
  {
    Oscilliate();
  }
  private void Oscilliate()
  {
    // Protect against dividing by zero because angularFrequency = tau / period
    if (period <= Mathf.Epsilon)
    {
      return;
    }
    angularFrequency = tau / period;
    sineWave = Mathf.Sin(angularFrequency * Time.time);
    offset = movementDirection * sineWave;
    transform.position = startingPosition + offset;
  }
}
