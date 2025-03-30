namespace UI
{
    public class UIButtonRestartScene : UIButton
    {
        public override void Click()
        {
            GameInstance.Instance.UI.StartTransaction(RestartScene);
        }
        private void RestartScene()
        {
            GameInstance.Instance.RestartScene();
        }
    }
}