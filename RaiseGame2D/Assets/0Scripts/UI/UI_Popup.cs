using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        GM.UI.SetCanvas(gameObject, true);
    }
    public virtual void ClosePopupUI()
    {
        GM.UI.ClosePopupUI(this);
    }
}
