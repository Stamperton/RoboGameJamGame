using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer/New Customer")]
public class Customer : ScriptableObject
{
    [Range(1, 7)] public int currentOpinion = 4;
    [Header("Positive Responses")]
    [SerializeField] public CustomerDialog[] positiveResponses;
    [SerializeField] public CustomerDialog[] correctParts;
    [Header("Negative Responses")]
    [SerializeField] public CustomerDialog[] negativeResponses;
    [SerializeField] public CustomerDialog[] wrongParts;
    [Header("Ambient Dialogue")]
    [SerializeField] public CustomerDialog[] progressiveDialog;
    [SerializeField] public CustomerDialog[] randomDialog;

    public Sprite defaultFace;
    public Sprite angryFace;
    public Sprite happyFace;
}
