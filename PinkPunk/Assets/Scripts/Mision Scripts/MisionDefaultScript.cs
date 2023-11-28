using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionDefaultScript : MonoBehaviour
{
    public int misionIsCompletedIndex = 1;
    public string nameMision;
    public string descriptionMision;
    public string locationMision;
    public bool misionStatus;
    public bool misionIsCompleted;
    public GameObject misionQuestMarkStart;
    public GameObject misionQuestMarkEnd;


    void Update()
    {
        if (misionQuestMarkStart.GetComponent<QuestMarks>().isCompleted == false)
        {
            misionStatus = false;
        }
        else if (misionQuestMarkStart.GetComponent<QuestMarks>().isCompleted == true && misionQuestMarkEnd.GetComponent<QuestMarks>().isCompleted == false)
        {
            misionStatus = true;
        }
        else if (misionQuestMarkStart.GetComponent<QuestMarks>().isCompleted == true && misionQuestMarkEnd.GetComponent<QuestMarks>().isCompleted == true)
        {
            misionStatus = false;
            misionIsCompleted = true;

        }
    }
}
