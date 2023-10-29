using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
    }
    private void Start()
    {
        GM.Contents.StartGame(true);
        GM.Contents.GamePause(false);
    }
    public override void Clear()
    {
        GM.Contents.StartGame(false);
    }
}
