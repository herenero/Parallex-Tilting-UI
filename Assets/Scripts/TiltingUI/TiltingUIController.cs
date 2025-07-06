using UnityEngine;

public class TiltingUIController : MonoBehaviour
{
    public RectTransform uiElement;
    public float maxTiltAngle = 15f; //+-maxTiltAngle 만큼 기울어짐
    public float maxMoveOffset = 20f; //기울기와 함께 약간의 위치 이동, +-maxMoveOffset
    public float smoothing = 5f;
    public bool reverseTilt = false; // 기울기 방향을 반대로 할지 여부

    private Vector2 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {

        if (uiElement == null)
        {
            Debug.LogError("UI Element가 할당되지 않았습니다");
            this.enabled = false;
            return;
        }
        initialPosition = uiElement.anchoredPosition;
        initialRotation = uiElement.localRotation;
    }

    void Update()
    {
        float mouseX = (Input.mousePosition.x / Screen.width) * 2 - 1;
        float mouseY = (Input.mousePosition.y / Screen.height) * 2 - 1;

        float directionmultiplier = reverseTilt ? -1f : 1f;

        Quaternion targetRotation = Quaternion.Euler(-mouseY * maxTiltAngle * directionmultiplier, mouseX * maxTiltAngle * directionmultiplier, 0f);

        Vector2 targetPosition = new Vector2(mouseX * maxMoveOffset * directionmultiplier, mouseY * maxMoveOffset * directionmultiplier);

        uiElement.localRotation = Quaternion.Lerp(uiElement.localRotation, initialRotation * targetRotation, smoothing * Time.deltaTime);
        uiElement.anchoredPosition = Vector2.Lerp(uiElement.anchoredPosition, initialPosition + targetPosition, smoothing * Time.deltaTime);
    }
}