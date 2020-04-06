using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentManager : MonoBehaviour
{
    #region Singleton
    public static ComponentManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("Multiple ComponentManagers In Scene");
            Destroy(gameObject);
        }
    }
    #endregion

    public ComponentPlacement[] partsToFix;
    int partsFixed = 0;

    [HideInInspector] public Component currentComponent;
    [HideInInspector] public Image currentComponentImage;

    public void ChangeCurrentComponent(Component newComponent)
    {
        if (currentComponent != null)
        {
            currentComponent.ReturnComponent();
        }

        currentComponent = newComponent;

        currentComponentImage.sprite = currentComponent.componentImage;
    }

    public void UseCurrentComponent()
    {
        currentComponent.isUsed = true;
        currentComponent = null;
        currentComponentImage.sprite = null;

        partsFixed++;

        if (partsFixed == partsToFix.Length)
        {
            //All Repaired, Win!
        }
   
    }
}

