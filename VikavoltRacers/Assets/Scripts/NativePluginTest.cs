using UnityEngine;

public class NativePluginTest : MonoBehaviour
{
    private System.IntPtr sim;
    public PS4Controller gamepad;
    public float thrustGain, rollGain, pitchGain, yawGain;
    private Vector3 startPosition;
    public int numLaps = 1;
    private int gateIndex = 0;

    void Start()
    {
        sim = NativeSim.sim_create();
        startPosition = this.transform.position;
        NativeSim.sim_reset(sim, startPosition.x, startPosition.z, startPosition.y);
    }

    void FixedUpdate()
    {
    	if (!gamepad.active)
    	{
    	    this.transform.Translate(gamepad.move * 20 * Time.deltaTime, Space.World);
    	}
    	else 
    	{
    	    Vector2 left = gamepad.leftStick;
    	    Vector2 right = gamepad.rightStick;
    	    float thrust = left.y;
    	    float roll = left.x;
       	    float pitch = right.y; 
       	    float yaw = right.x;
       	    Debug.Log($"Controls: ({thrust*thrustGain:0.00}, {roll:0.00}, {pitch:0.00}, {yaw:0.00})");
       	            // simple constant action
        NativeSim.sim_step(sim,thrustGain*thrust, rollGain* roll, pitchGain*pitch, yawGain*yaw, Time.fixedDeltaTime);
        //NativeSim.sim_step(sim, 10f, 0.01f, 0f, 0f, Time.fixedDeltaTime);
        SimplifiedState state = NativeSim.sim_get_simplified_state(sim);
        
        this.transform.position = new Vector3(state.px, state.pz, state.py);
        this.transform.rotation = new Quaternion(state.qx, state.qz, state.qy, state.qw);
                Debug.Log($"Drone position: ({state.px:0.00}, {state.py:0.00}, {state.pz:0.00})");
   	}
    

    }
    
	
    void OnTriggerEnter(Collider other)
    {
    	Debug.Log("On Trigger Enter called for: " + other.name);
    	Transform childTrigger = Track.Instance.Gates[gateIndex].Find("Trigger");
    	if (childTrigger == other.transform)
    	{
    	    Debug.Log("Passed gate: " + other.name);
    	    gateIndex += 1;
    	    if (gateIndex >= Track.Instance.Gates.Count)
    	    {
    	    	gateIndex = 0;
    	        Debug.Log("Reached final gate.");
    	        numLaps -= 1;
    	        if (numLaps == 0)
    	        {
    	            Debug.Log("Race has been finished");
    	        }
    	    }
    	} 
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        NativeSim.sim_reset(sim, startPosition.x, startPosition.z, startPosition.y);
        // Example: check tag
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Debug.Log("Hit an enemy!");
        //}
    }

    void OnDestroy()
    {
        NativeSim.sim_destroy(sim);
    }
}

