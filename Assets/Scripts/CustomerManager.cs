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

}
