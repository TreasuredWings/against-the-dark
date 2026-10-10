using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;

public class AllyStatus : MonoBehaviour
{
    [field: SerializeField] public Ally AssignedAlly {get; private set;}
    [field: SerializeField] public TextMeshProUGUI NameDisplay {get; private set;}
    [field: SerializeField] public AllyValueStatus HPDisplay {get; private set;}
    [field: SerializeField] public AllyValueStatus MPDisplay {get; private set;}

    public void SetAlly(Ally newAlly)
    {
        AssignedAlly = newAlly;
        NameDisplay.text = AssignedAlly.Name;
    }

    public void UpdateValues()
    {
        if (AssignedAlly != null)
        {
            HPDisplay.SetValues(AssignedAlly.HPMax, AssignedAlly.HP);
            MPDisplay.SetValues(AssignedAlly.MPMax, AssignedAlly.MP);
        }
        else
        {
            Debug.Log(this.name + " is not hooked to an ally");
        }
    }
}
