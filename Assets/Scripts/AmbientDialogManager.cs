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
    Color endColor = Color.red;
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

        GetNewDialog();
    }

    void GetNewDialog()
    {
        dialogBox.color = Color.white;

        playerChoice = DialogChoices.Null;

        previousDialog = currentDialog;

        currentDialog = CustomerManager.instance.currentCustomer.ambientDialog[Random.Range(0, CustomerManager.instance.currentCustomer.ambientDialog.Length)];
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

        if (Time.time > ambientTimer)
        {
            if (currentDialog.dialogResponse == DialogChoices.Null)
            {
                GetNewDialog();
            }

            else if (playerChoice == currentDialog.dialogResponse)
            {
                currentCustomer.currentOpinion++;
                Debug.Log("CORRECT CHOICE");
                GetNewDialog();
            }

            else if (playerChoice != currentDialog.dialogResponse)
            {
                currentCustomer.currentOpinion--;
                Debug.Log("WRONG CHOICE");
                GetNewDialog();
            }

        }

        if (currentDialog.dialogResponse != DialogChoices.Null)
        {
            float t = (Time.time - lerpTime) / currentDialog.dialogTimer;
            dialogBox.color = Color.Lerp(startcolor, endColor, t);
        }
    }


}
