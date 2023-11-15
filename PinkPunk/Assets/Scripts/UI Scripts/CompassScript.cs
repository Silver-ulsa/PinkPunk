using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour
{
    public GameObject iconPrefab;

    List<QuestMarks> questMarks = new List<QuestMarks>();
    public RawImage compassImage;
    public Transform player;

    float compassUnit;

    public QuestMarks[] questMarksArray;
    private void Start()
    {
        compassImage = GetComponent<RawImage>();
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        
        
        // recorrer el arreglo quesMarkArray agregando todos los marcadores a la lista questMarks
        foreach (QuestMarks marker in questMarksArray) {
            GameObject newMarker = Instantiate(iconPrefab, transform);
            marker.image = newMarker.GetComponent<Image>();
            marker.image.sprite = marker.questMarkIcon;
            questMarks.Add(marker);
        }
    }

    private void Update()
    {
        
    foreach (QuestMarks marker in questMarksArray) {
        if (marker.isActive) {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360, 0, 1, 1);
        marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker.position);
            // La misión está activa, por lo tanto, asegúrate de que su icono esté activado.
            marker.image.gameObject.SetActive(true);
        } else {
            // La misión no está activa, desactiva el icono.
            marker.image.gameObject.SetActive(false);
        }
    }
    }

    // Esta funcuón esta aquí porque para ver si jala, pero deberia de estar en el script que administra las misiones
    public void AddQuestMarker (QuestMarks marker) {
        GameObject newMarker = Instantiate(iconPrefab, transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.questMarkIcon;
        questMarks.Add(marker);
    }

    Vector2 GetPosOnCompass (Vector2 marker) {
        Vector2 playerPosition = new Vector2(player.position.x, player.position.z);
        Vector2 playerForward = new Vector2(player.forward.x, player.forward.z);

        float angle = Vector2.SignedAngle(marker - playerPosition, playerForward);
        
        return new Vector2(compassUnit * angle, 0f);
    }

}
