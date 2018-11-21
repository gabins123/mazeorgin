using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bakanas : MonoBehaviour {

    public GameObject GameplayController;
    bool getBool() { return GameplayController.GetComponent<GameplayController>().isSaved; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!getBool())
            {
                Debug.Log("U have saved a bakana!");
                GameplayController.GetComponent<GameplayController>().isSaved = true;
                Destroy(gameObject, 3f);
            }
        }
    }
}
