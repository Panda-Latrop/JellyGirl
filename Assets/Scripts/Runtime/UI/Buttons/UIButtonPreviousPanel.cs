namespace UI
{
    public class UIButtonPreviousPanel : UIButton
    {
        public override void Click()
        {
            GameInstance.Instance.UI.Back();
        }
    }
}
