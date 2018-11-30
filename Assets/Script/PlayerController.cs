using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject m_GameplayController;
    public float moveSpeed = 8f;
    public Joystick joystick;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bakanas"))
        {
            m_GameplayController.GetComponent<GameplayController>().isSaved = true;
        }
        if(other.gameObject.CompareTag("OutDoor"))
        {
            m_GameplayController.GetComponent<GameplayController>().hamReset();
        
        }
    }

}
