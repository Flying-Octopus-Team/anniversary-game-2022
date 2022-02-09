using System;
using Godot;

namespace FOAnniversary.UI.Button.Scripts
{
    public class LinkButton : BasicButton
    {
        [Export()] private String _url;

        protected override void OnButtonClick()
        {
            OS.ShellOpen(_url);
        }
    }
}