using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    #region Singleton
    public static DialogManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("Two CustomerManagers in Scene.");
            Destroy(gameObject);
        }
    }
    #endregion

    //LERP Variables
    Color startcolor = Color.white;
    Color endColor = Color.yellow;
    float lerpTime;

    public Text dialogBox;
    public Slider reputationSlider;

    //Timer Variables
    float ambientTimer;
    float ambientTimerStart;

    int dialogPosition = 0;
    int answerPosition = 0;
    int partDialogPosition = 0;
    bool randomDialog = false;

    public Customer currentCustomer;

    CustomerDialog previousDialog;
    CustomerDialog currentDialog;
    DialogChoices playerChoice;

    private void Start()
    {
        currentCustomer.currentOpinion = 4;
        GetNewDialog(DialogType.Ambient);
        reputationSlider.value = currentCustomer.currentOpinion;
    }

    void GetNewDialog(DialogType dialogType)
    {
        ambientTimer = 0;

        dialogBox.color = Color.white;

        playerChoice = DialogChoices.Null;

        previousDialog = currentDialog;

        switch (dialogType)
        {
            case DialogType.Ambient:
                if (randomDialog == true)
                {
                    currentDialog = currentCustomer.randomDialog[Random.Range(0, currentCustomer.randomDialog.Length)];
                }
                else
                {
                    currentDialog = currentCustomer.progressiveDialog[dialogPosition];
                }
                break;
            case DialogType.Positive:
                dialogBox.color = Color.green;
                currentDialog = currentCustomer.positiveResponses[answerPosition];
                break;
            case DialogType.Negative:
                dialogBox.color = Color.red;
                currentDialog = currentCustomer.negativeResponses[answerPosition];
                break;
            case DialogType.CorrectPart:
                dialogBox.color = Color.green;
                currentDialog = currentCustomer.correctParts[partDialogPosition];
                break;
            case DialogType.WrongPart:
                dialogBox.color = Color.red;
                currentDialog = currentCustomer.wrongParts[partDialogPosition];
                break;
            default:
                break;
        }

        if (randomDialog)
        {
            while (previousDialog == currentDialog)
            {
                currentDialog = currentCustomer.randomDialog[Random.Range(0, currentCustomer.randomDialog.Length)];
            }

        }

        dialogBox.text = currentDialog.dialogText.ToString();

        ambientTimer = Time.time + currentDialog.dialogTimer;

        lerpTime = Time.time;
    }

    private void Update()
    {
        if (currentDialog == null)
        {
            Debug.LogError("No Dialog");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            playerChoice = DialogChoices.Agree;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            playerChoice = DialogChoices.Disagree;
        }

        if (Time.time > (ambientTimer + .5f))
        {
            if (currentDialog.dialogResponse == DialogChoices.Null)
            {
                dialogPosition++;

                if (dialogPosition == currentCustomer.progressiveDialog.Length)
                {
                    randomDialog = true;
                }

                GetNewDialog(DialogType.Ambient);
            }

            else if (playerChoice == currentDialog.dialogResponse)
            {
                ImproveReputation();
                Debug.Log("CORRECT CHOICE");
                GetNewDialog(DialogType.Positive);
                answerPosition++;
            }

            else if (playerChoice != currentDialog.dialogResponse)
            {
                DecreaseReputation();
                Debug.Log("WRONG CHOICE");
                GetNewDialog(DialogType.Negative);
                answerPosition++;
            }

        }

        //Lerp White to Red in case of a Question
        if (currentDialog.dialogResponse != DialogChoices.Null)
        {
            float t = (Time.time - lerpTime) / currentDialog.dialogTimer;
            dialogBox.color = Color.Lerp(startcolor, endColor, t);
        }

        if (Time.time > ambientTimer)
        {
            float t = .1f;
            dialogBox.color = Color.Lerp(dialogBox.color, Color.clear, t);
        }
    }

    public void GoodResponse()
    {
        GetNewDialog(DialogType.CorrectPart);
        partDialogPosition++;
        dialogPosition--;
        ImproveReputation();
    }

    public void BadResponse()
    {
        GetNewDialog(DialogType.WrongPart);
        partDialogPosition++;
        dialogPosition--;
        DecreaseReputation();
    }

    public void ImproveReputation()
    {
        currentCustomer.currentOpinion++;
        reputationSlider.value = currentCustomer.currentOpinion;
    }

    public void DecreaseReputation()
    {
        currentCustomer.currentOpinion--;
        reputationSlider.value = currentCustomer.currentOpinion;
    }
}
