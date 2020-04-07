using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer/New Customer")]
public class Customer : ScriptableObject
{
    [Range(1, 7)] public int currentOpinion = 4;
    [Header("Positive Responses")]
    [Tooltip("These are generated when answering a question correctly")]
    [SerializeField] public CustomerDialog[] positiveResponses;
    [Tooltip("These are generated when replacing a part correctly")]
    [SerializeField] public CustomerDialog[] correctParts;
    [Header("Negative Responses")]
    [Tooltip("These are generated when answering a question incorrectly")]
    [SerializeField] public CustomerDialog[] negativeResponses;
    [Tooltip("These are generated when replacing a part incorrectly")]
    [SerializeField] public CustomerDialog[] wrongParts;
    [Header("Ambient Dialogue")]
    [Tooltip("These will be said in order as an ongoing dialog")]
    [SerializeField] public CustomerDialog[] progressiveDialog;
    [Tooltip("These will be randomly selected from after progressive dialog is exhausted")]
    [SerializeField] public CustomerDialog[] randomDialog;

    [Header("Image Settings")]
    public Sprite defaultFace;
    public Sprite angryFace;
    public Sprite happyFace;
}
