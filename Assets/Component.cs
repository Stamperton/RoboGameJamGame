using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component : MonoBehaviour
{
    public PartNumber partNumber;

    public Sprite componentImage;
    SpriteRenderer spriteRenderer;

    bool isSelectable = true;
    public bool isUsed = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        componentImage = spriteRenderer.sprite;
    }

    public void SelectComponent()
    {
        isSelectable = false;
        spriteRenderer.enabled = false;
        ComponentManager.instance.ChangeCurrentComponent(this);
    }

    public void ReturnComponent()
    {
        if (isUsed)
            return;

        isSelectable = true;
        spriteRenderer.enabled = true;
    }

    private void OnMouseUpAsButton()
    {
        if (isSelectable)
            SelectComponent();
    }
}
