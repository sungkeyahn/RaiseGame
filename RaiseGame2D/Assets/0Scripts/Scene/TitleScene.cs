using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    UI_Title title;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Title;
    }
    private void Start()
    {
       title = GM.UI.ShowSceneUI<UI_Title>(null, "TitleUI");
    }
    public override void Clear()
    {   }

}
