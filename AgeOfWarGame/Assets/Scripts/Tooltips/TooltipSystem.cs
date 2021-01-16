﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{

    private static TooltipSystem current;
    private Tooltip tooltip;

    public GeneralTooltip generalTooltip;
    public SoldierTooltip soldierTooltip;
    public TurretTooltip turretTooltip;

    void Awake()
    {
        current = this;
    }

    public static void Show(ScriptableObject scriptableObject)
    {
        if (scriptableObject.GetType() == typeof(SoldierConfig))
        {
            current.tooltip = current.soldierTooltip;
        }
        else if (scriptableObject.GetType() == typeof(GeneralTooltipInfoConfig))
        {
            current.tooltip = current.generalTooltip;
        }
        else if (scriptableObject.GetType() == typeof(TurretConfig))
        {
            current.tooltip = current.turretTooltip;
        }
        else
        {
            return;
        }

        current.tooltip.SetContent(scriptableObject);
        current.tooltip.gameObject.transform.localScale = Vector3.zero;
        current.tooltip.gameObject.SetActive(true);
        LeanTween.scale(current.tooltip.gameObject, Vector3.one, 0.2f).setEaseInExpo();
    }


    public static void Hide()
    {
        if (current.tooltip)
        {
            current.tooltip.gameObject.SetActive(false);
        }
    }

    public static Tooltip GetCurrentTooltip()
    {
        return current.tooltip;
    }

}
