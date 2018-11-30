using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoor : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("wall"))
        {
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
