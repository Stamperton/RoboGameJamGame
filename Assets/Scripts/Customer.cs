using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName =  "Customer/New Customer")]
public class Customer : ScriptableObject
{
    [Range(1, 5)] public int currentOpinion = 3;

    [TextArea] public string negativeResponse1;
    [TextArea] public string negativeResponse2;
    [TextArea] public string negativeResponse3;

    [TextArea] public string positiveResponse1;
    [TextArea] public string positiveResponse2;
    [TextArea] public string positiveResponse3;

    [SerializeField]
    public CustomerDialog[] ambientDialog;

    public Sprite defaultFace;
    public Sprite angryFace;
    public Sprite happyFace;
}
