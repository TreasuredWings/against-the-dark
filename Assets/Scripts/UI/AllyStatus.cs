using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor;

public class AllyStatus : MonoBehaviour
{
    [field: SerializeField] public Ally AssignedAlly {get; private set;}
    [field: SerializeField] public TextMeshProUGUI NameDisplay {get; private set;}
    [field: SerializeField] public AllyValueStatus HPDisplay {get; private set;}
    [field: SerializeField] public AllyValueStatus MPDisplay {get; private set;}

    private Color IdleColor = new Color32(65, 105, 225, 225);
    private Color ActiveColor = new Color32(100, 149, 237, 255);

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
            if (AssignedAlly == InputManager.Instance.ActiveAlly)
            {
                GetComponent<UnityEngine.UI.Image>().color = ActiveColor;
            }
            else
            {
                GetComponent<UnityEngine.UI.Image>().color = IdleColor;
            }
        }
    }
}
