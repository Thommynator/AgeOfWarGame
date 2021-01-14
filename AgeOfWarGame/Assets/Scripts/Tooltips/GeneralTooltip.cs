using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GeneralTooltip : Tooltip
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI content;

    public override void SetContent(ScriptableObject scriptableObject)
    {
        if (scriptableObject.GetType() != typeof(GeneralTooltipInfoConfig))
        {
            return;
        }

        GeneralTooltipInfoConfig generalInfo = (GeneralTooltipInfoConfig)scriptableObject;
        header.text = generalInfo.header;

        // if (generalInfo.content != null && generalInfo.content != "")
        // {
        content.text = generalInfo.content;
        // }
        int wrapLimit = 60;
        GetComponent<LayoutElement>().enabled = header.text.Length > wrapLimit || content.text.Length > wrapLimit;
    }
}
