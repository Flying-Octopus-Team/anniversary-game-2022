using Godot;

namespace FOAnniversary.UI.Button.Scripts
{
    public class SwitchSceneButton : BasicButton
    {
        [Export] private PackedScene _scene;

        protected override void OnButtonClick()
        {
            GetTree().ChangeScene(_scene.ResourcePath);
        }
    }
}