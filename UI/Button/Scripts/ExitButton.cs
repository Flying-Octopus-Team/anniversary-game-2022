namespace FOAnniversary.UI.Button.Scripts
{
    public class ExitButton : BasicButton
    {
        protected override void OnButtonClick()
        {
            GetTree().Quit();
        }
    }
}
