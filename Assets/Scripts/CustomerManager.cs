using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    #region Singleton
    public static CustomerManager instance;
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

    private AmbientDialogManager ambientDialogManager;

    public Customer currentCustomer;

    private void Start()
    {
        ambientDialogManager = AmbientDialogManager.instance;
    }

    public void NegativeResponse()
    {
        switch (currentCustomer.currentOpinion)
        {
            case 3:
                //dialogManager.DisplayText(currentCustomer.negativeResponse1);
                break;
            case 2:
                //dialogManager.DisplayText(currentCustomer.negativeResponse2);
                break;
            case 1:
                //dialogManager.DisplayText(currentCustomer.negativeResponse3);
                break;

            default:
                break;
        }
    }
}
