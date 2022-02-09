using System;
using Godot;

namespace FOAnniversary.UI.Button.Scripts
{
    public class SwitchSceneWithStringButton : BasicButton
    {
        [Export] private String _sceneResourcePath;

        protected override void OnButtonClick()
        {
            GetTree().ChangeScene(_sceneResourcePath);
        }
    }
}