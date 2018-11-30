using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject m_Player;
    public GameObject GameplayController;
    public GameObject LevelController;
    Vector3 m_offset;

	// Use this for initialization
	void Start () {
        m_offset = transform.position - m_Player.transform.position;
        m_offset.y += LevelController.GetComponent<LevelController>().m_Level * 0.3f;
    }
	
	// Update is called once per frame
	void Update () {
        if(GameplayController.GetComponent<GameplayController>().isWon)
        {
            m_offset.y +=  LevelController.GetComponent<LevelController>().m_Level*0.3f;
        }
        transform.position = m_Player.transform.position + m_offset;
    }
}
