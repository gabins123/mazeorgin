using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public GameObject GameplayController;
    public Text m_Timer;
    public Text TimerPrefabs;
    float SecondCounter = 0;
    float MinCounter = 0;
    float HourCounter = 0;
    int m_Level = 1;

    private void Start()
    {
        m_Timer = Instantiate(TimerPrefabs);
        m_Timer.transform.parent = gameObject.transform;
        m_Timer.transform.localScale = new Vector3(1, 1, 1);
        m_Timer.transform.localPosition = Vector3.zero;
    }
    // Update is called once per frame
    void Update () {
        if (!GameplayController.GetComponent<GameplayController>().isSaved)
        {
            TimeCounter(m_Timer,m_Level);
        }
        if(GameplayController.GetComponent<GameplayController>().isWon)
        {
            m_Level++;
            m_Timer = Instantiate(TimerPrefabs);
            setTimerPosition(m_Timer);
            Debug.Log("U saved the bakana in " + HourCounter + " hour(s) " + MinCounter + " min(s) " + Mathf.RoundToInt(SecondCounter) + " second(s)!!!");      
            SecondCounter = 0;
            MinCounter = 0;
            HourCounter = 0;
            GameplayController.GetComponent<GameplayController>().isWon = false;
        }
    }

    void TimeCounter(Text m_Timer,int Level)
    {
        m_Timer.text = "Level "+ m_Level+ ": " + HourCounter + "." + MinCounter + "." + Mathf.RoundToInt(SecondCounter);
        SecondCounter += Time.deltaTime;
        if(SecondCounter>=60)
        {
            MinCounter++;
            SecondCounter = 0;
        }
        if(MinCounter >= 60)
        {
            HourCounter++;
            MinCounter = 0;
        }
    }
    void setTimerPosition(Text m_Timer)
    {
        m_Timer.transform.parent = gameObject.transform;
        m_Timer.transform.localScale = new Vector3(1,1,1);
        m_Timer.transform.localPosition = Vector3.zero;
        m_Timer.transform.localPosition -= new Vector3(0, 25*(m_Level-1), 0);
    }
}
