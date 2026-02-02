using UnityEngine;
using System.Collections.Generic;

public class Track : MonoBehaviour
{
    public static Track Instance { get; private set; }
    public List<Transform> Gates { get; private set; }
    
    void Awake()
    {
    	Instance = this;
    	Gates = new List<Transform>();
    	foreach (Transform child in transform)
    	{
    	    Debug.Log($"Gate position: ({child.transform.position.x}, {child.transform.position.y}, {child.transform.position.z})");
	    Gates.Add(child);    	
    	}
    }

}
