using UnityEngine;
using UnityEngine.UI;

public class QuestMarks : MonoBehaviour
{
    public Sprite questMarkIcon;
    [HideInInspector] public Image image;

    public GameObject nextPoint;
    public bool isActive;

    public bool isCompleted = false;
    
    public Vector2 position {
        get {return new Vector2(transform.position.x, transform.position.z);}
    }

    private void OnTriggerEnter(Collider other) {
        isActive = false;
        isCompleted = true;
        gameObject.SetActive(false);
        nextPoint.GetComponent<QuestMarks>().isActive = true;
        nextPoint.GetComponent<QuestMarks>().gameObject.SetActive(true);
    }
}
