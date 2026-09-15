using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private Transform window;
    [SerializeField]
    private Transform openPos;
    [SerializeField]
    private Transform closePos;

    private bool isOpen;

    private bool mouseOver;

    public bool MouseOver
    {
        get;
    }

    private void Start()
    {
        isOpen = false;
    }

    private void Update()
    {
        if (isOpen)
        {
            window.localPosition = Vector2.Lerp(window.localPosition, openPos.localPosition, 0.1f);
        }
        else
        {
            window.localPosition = Vector2.Lerp(window.localPosition, closePos.localPosition, 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchIsOpen();
        }
    }

    public void SwitchIsOpen()
    {
        isOpen = !isOpen;
    }
}
