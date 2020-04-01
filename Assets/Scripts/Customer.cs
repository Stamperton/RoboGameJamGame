using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer/New Customer")]
public class Customer : ScriptableObject
{
    [Range(1, 7)] public int currentOpinion = 4;
    [Header("Positive Responses")]
    [SerializeField] public CustomerDialog[] positiveResponses;
    [Header("Negative Responses")]
    [SerializeField] public CustomerDialog[] negativeResponses;
    [Header("Ambient Dialogue")]
    [SerializeField] public CustomerDialog[] ambientDialog;

    public Sprite defaultFace;
    public Sprite angryFace;
    public Sprite happyFace;
}
