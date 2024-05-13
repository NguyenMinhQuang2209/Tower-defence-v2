using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    [SerializeField] private Camera mCamera;
    [SerializeField] private float scrollMin = 1f;
    [SerializeField] private float scrollMax = 1f;
    [SerializeField] private float defaultScroll = 1f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float mouseScrollSensitivity = 1f;
    Vector3 dragPos;
    float currentScrollSize = 0f;
    float mapHeight = 0f;
    float mapWidth = 0f;
    private void Start()
    {
        mCamera.orthographicSize = defaultScroll;
        currentScrollSize = defaultScroll;
    }
    private void OnEnable()
    {
        MapGenerator.onGenerateMapDoneAction += OnGenerateMapDoneEvent;
    }
    private void OnDisable()
    {
        MapGenerator.onGenerateMapDoneAction -= OnGenerateMapDoneEvent;
    }

    private void OnGenerateMapDoneEvent(Vector2 vector, Vector2 mapSize)
    {
        mCamera.transform.position = new(vector.x, vector.y, mCamera.transform.position.z);
        mapWidth = mapSize.x;
        mapHeight = mapSize.y;
    }

    private void Update()
    {
        MouseMove();
        MouseScroll();
    }
    public float ClampSize(float size, float min, float max)
    {
        return Mathf.Clamp(size, min, max);
    }
    private void MouseMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 differ = dragPos - mCamera.ScreenToWorldPoint(Input.mousePosition);
            differ.x *= mouseSensitivity;
            differ.y *= mouseSensitivity;
            differ.z = 0;
            ClampMouseMove(differ);
        }
    }
    private void MouseScroll()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll > 0f)
        {
            currentScrollSize = ClampSize(currentScrollSize - Time.deltaTime * mouseScrollSensitivity, scrollMin, scrollMax);
            mCamera.orthographicSize = currentScrollSize;
        }
        else if (mouseScroll < 0f)
        {
            currentScrollSize = ClampSize(currentScrollSize + Time.deltaTime * mouseScrollSensitivity, scrollMin, scrollMax);
            mCamera.orthographicSize = currentScrollSize;
        }

        if (mouseScroll != 0)
        {
            ClampMouseMove(Vector3.zero);
        }
    }
    private void ClampMouseMove(Vector3 differ)
    {
        Vector3 targetPos = mCamera.transform.position + differ;

        float cHeight = mCamera.orthographicSize;
        float cWidth = mCamera.orthographicSize * mCamera.aspect;

        targetPos.x = Mathf.Clamp(targetPos.x, 0f + cWidth, mapWidth - cWidth);
        targetPos.y = Mathf.Clamp(targetPos.y, 0f + cHeight, mapHeight - cHeight);

        mCamera.transform.position = targetPos;
    }
}
