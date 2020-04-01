using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmbientDialogManager : MonoBehaviour
{
    #region Singleton
    public static AmbientDialogManager instance;
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

    //Timer Variables
    float ambientTimer;
    float ambientTimerStart;

    Customer currentCustomer; //LINK TO CUSTOMER MANAGER

    CustomerDialog previousDialog;
    CustomerDialog currentDialog;
    DialogChoices playerChoice;

    private void Start()
    {
        currentCustomer = CustomerManager.instance.currentCustomer;

        GetNewDialog(DialogType.Ambient);
    }

    void GetNewDialog(DialogType dialogType)
    {
        dialogBox.color = Color.white;

        playerChoice = DialogChoices.Null;

        previousDialog = currentDialog;

        switch (dialogType)
        {
            case DialogType.Ambient:
                currentDialog = CustomerManager.instance.currentCustomer.ambientDialog[Random.Range(0, CustomerManager.instance.currentCustomer.ambientDialog.Length)];
                break;
            case DialogType.Positive:
                dialogBox.color = Color.green;
                currentDialog = CustomerManager.instance.currentCustomer.positiveResponses[Random.Range(0, CustomerManager.instance.currentCustomer.positiveResponses.Length)];
                break;
            case DialogType.Negative:
                dialogBox.color = Color.red;
                currentDialog = CustomerManager.instance.currentCustomer.negativeResponses[Random.Range(0, CustomerManager.instance.currentCustomer.negativeResponses.Length)];
                break;
            default:
                break;
        }

        while (previousDialog == currentDialog)
        {
            currentDialog = CustomerManager.instance.currentCustomer.ambientDialog[Random.Range(0, CustomerManager.instance.currentCustomer.ambientDialog.Length)];
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
                GetNewDialog(DialogType.Ambient);
            }

            else if (playerChoice == currentDialog.dialogResponse)
            {
                currentCustomer.currentOpinion++;
                Debug.Log("CORRECT CHOICE");
                GetNewDialog(DialogType.Positive);
            }

            else if (playerChoice != currentDialog.dialogResponse)
            {
                currentCustomer.currentOpinion--;
                Debug.Log("WRONG CHOICE");
                GetNewDialog(DialogType.Negative);
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


}
