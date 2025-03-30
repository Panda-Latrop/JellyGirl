using UnityEngine;

namespace UI
{
    public class UIButtonChangeScene : UIButton
    {
        [SerializeField] private string sceneName;
        public override void Click()
        {
            GameInstance.Instance.UI.StartTransaction(ChangeScene);
        }
        private void ChangeScene()
        {
            GameInstance.Instance.ChangeScene(sceneName);
        }
    }
}
