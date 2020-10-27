using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car")]
    [SerializeField] private float speed = 5;
    [SerializeField, Range(0, 1)] private float slowTimeScale = 0.2f;

    [Header("Input")]
    [SerializeField] private GameObject arrow = null;
    [SerializeField, Range(0, 1)] private float sensitivity = 1;
    [SerializeField] private KeyCode key = KeyCode.Mouse1;

    private bool isDragging = false;
    private Vector3 clickedPosition = Vector3.zero;

    private float angle = 0;
    private float carAngle = 0;

    private void Start()
    {
        arrow.SetActive(isDragging);
    }

    private void Update()
    {
        Vector3 translation = transform.forward * speed * Time.deltaTime;
        transform.Translate(translation, Space.World);

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(key))
        {
            isDragging = true;
            arrow.SetActive(isDragging);

            clickedPosition = Input.mousePosition;
            carAngle = transform.eulerAngles.y;

            Time.timeScale = slowTimeScale;
        }
        else if (Input.GetKeyUp(key))
        {
            isDragging = false;
            arrow.SetActive(isDragging);

            Time.timeScale = 1;
        }

        if (isDragging)
        {
            Vector3 pos = Input.mousePosition;
            angle = (clickedPosition - pos).x * sensitivity;
            angle = -angle;

            transform.rotation = Quaternion.AngleAxis(carAngle + angle, Vector3.up);
        }
    }

    private void OnGUI()
    {
        var r = new Rect(10, 10, 100, 50);
        string text = $"{Time.timeScale}";
        GUI.Label(r, text);
    }
}
