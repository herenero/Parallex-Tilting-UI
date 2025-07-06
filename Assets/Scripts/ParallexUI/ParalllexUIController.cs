using System.Collections.Generic;
using UnityEngine;

public class ParalllexUIController : MonoBehaviour
{
    public List<ParallexLayer> parallexLayers = new();
    public float smoothing = 5f;

    private List<Vector2> initialPositions;

    private void Start()
    {
        initialPositions = new List<Vector2>();

        foreach (ParallexLayer layers in parallexLayers)
        {
            initialPositions.Add(layers.rectTransform.anchoredPosition);
        }
    }

    private void Update()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x - (Screen.width / 2), Input.mousePosition.y - (Screen.height / 2));

        for (int i = 0; i < parallexLayers.Count; i++)
        {
            if (parallexLayers[i].rectTransform != null)
            {
                Vector2 targetPosition = initialPositions[i] + mousePosition * parallexLayers[i].parallexStrength;
                parallexLayers[i].rectTransform.anchoredPosition = Vector2.Lerp(
                    parallexLayers[i].rectTransform.anchoredPosition,
                    targetPosition,
                    Time.deltaTime * smoothing
                    );
            }
        }
    }
}