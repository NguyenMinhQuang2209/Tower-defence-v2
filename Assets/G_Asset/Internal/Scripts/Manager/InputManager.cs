using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static Action leftMouseClickAction;
    public static Action rightMouseClickAction;
    public static Action middleMouseClickAction;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseClickAction?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightMouseClickAction?.Invoke();
        }

        if (Input.GetMouseButtonDown(2))
        {
            middleMouseClickAction?.Invoke();
        }
    }
}
