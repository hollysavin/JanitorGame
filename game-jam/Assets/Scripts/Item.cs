using Palmmedia.ReportGenerator.Core.Reporting.Builders.Rendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemOwner = null;

    public string GetItemOwner()
    {
        return itemOwner;
    }

    public void SetItemOwner(string newOwner)
    {
        itemOwner = newOwner;
    }

    public bool HasOwner()
    {
        return (itemOwner != null);
    }

}
