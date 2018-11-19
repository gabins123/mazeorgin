using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public GameObject GameplayController;

    public int m_Level=0;

	
	// Update is called once per frame
	void Update () {
		if(GameplayController.GetComponent<GameplayController>().isWon)
        {
            Debug.Log("Level Tăng!");
            m_Level++;
            GameplayController.GetComponent<GameplayController>().m_BasicColumns++;
            GameplayController.GetComponent<GameplayController>().m_BasicRows++;
            GameplayController.GetComponent<GameplayController>().isWon = false;
        }
	}
}
