using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerNew : MonoBehaviour
{
	// Player attributes
	public FloatReference Health;
	public FloatReference JumpForce;
    public FloatReference JumpHeightMax;
    public FloatReference JumpHeightMin;
    public FloatReference MaxMoveSpeed;

    // Player influences
    public FloatReference Gravity;

    // Player events
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
        if (damage != null)
        {
            Health.ApplyChange(-damage.DamageAmount);
            DamageEvent.Invoke();
        }

        if (Health.Value <= 0.0f)
        {
            DeathEvent.Invoke();
        }
    }
}
