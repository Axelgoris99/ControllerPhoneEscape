using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class serverListEntryController
{
    Label serverIp;

    public void SetVisualElement(VisualElement visualElement)
    {
        serverIp = visualElement.Q<Label>("ServerIp");
    }

    public void SetServerData(string info)
    {
        serverIp.text = info;
    }
}
