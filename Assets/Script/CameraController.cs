using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject m_Player;
    Vector3 m_offset;

	// Use this for initialization
	void Start () {
        m_offset = transform.position - m_Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = m_Player.transform.position + m_offset;

    }
}
