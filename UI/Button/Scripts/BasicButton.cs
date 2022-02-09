using Godot;

namespace FOAnniversary.UI.Button.Scripts
{
    public class BasicButton : TextureButton
    {
        [Export] private string _buttonText;
        [Export] private Font _buttonFont;

        [Export] private NodePath _buttonAudioPlayer;
        private AudioStreamPlayer _audioPlayer;

        [Export] private NodePath _labelPath;
        private Label _label;

        public override void _Ready()
        {
            _audioPlayer = GetNode<AudioStreamPlayer>(_buttonAudioPlayer);
            _label = GetNode<Label>(_labelPath);
            _label.Text = _buttonText;
            if (_buttonFont != null)
            {
                _label.AddFontOverride("font", _buttonFont);
            }

            Connect("pressed", this, "OnButtonPressed");
        }

        private async void OnButtonPressed()
        {
            _audioPlayer.Play();
            await ToSignal(_audioPlayer, "finished");
            OnButtonClick();
        }


        protected virtual void OnButtonClick()
        {
        }
    }
}