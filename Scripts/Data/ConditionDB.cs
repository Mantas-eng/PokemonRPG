using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDB
{
    public static Dictionary<ConditionsID, Condition> Conditions { get; set; } = new Dictionary<ConditionsID, Condition>()
    {

        {
            ConditionsID.psn,
            new Condition()
            {
                Name = "Poison",
                StartMessage = "has been poisoned"
            }
        }

    };


    }

public enum ConditionsID{
    psn,brn,slp,par,frz
}