using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPlacement : MonoBehaviour
{
    public PartNumber thisPart;

    public Sprite componentImage;

    SpriteRenderer spriteRenderer;

    ComponentManager componentManager;

    bool isSelectable = true;

    private void Start()
    {
        componentManager = ComponentManager.instance;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        if (componentManager.currentComponent != null && isSelectable)
        {
            if (componentManager.currentComponent.partNumber == thisPart)
            {
                isSelectable = false;
                spriteRenderer.enabled = true;
                componentManager.UseCurrentComponent();

                DialogManager.instance.GoodResponse();
            }

            else
            {
                DialogManager.instance.BadResponse();
            }
        }
    }
}
