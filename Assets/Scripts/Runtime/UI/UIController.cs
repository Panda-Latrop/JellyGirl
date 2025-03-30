#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup group;
        private bool hasTransaction;
        [SerializeField] private UITransaction transaction;
        [SerializeField] private UIPanel[] panels;
        private UIPanel activePanel;
        private Stack<UIPanel> prevPanels = new Stack<UIPanel>();

        public void Init()
        {
            
            if (hasTransaction = transaction != null)
            {
                transaction.OnFinish += OnTransactionFinish;
                transaction.Init();
            }

            if (panels.Length >= 1)
            {
                activePanel = panels[0];
                activePanel.Initialize(this);
                activePanel.SetActivity(true);
                for (int i = 1; i < panels.Length; i++)
                {
                    panels[i].Initialize(this);
                    panels[i].SetActivity(false);
                }
            }
            Refresh();
        }
        private void OnDestroy()
        {
            if(hasTransaction)
                transaction.OnFinish -= OnTransactionFinish;

        }
        public void Change(string specifer)
        {
            Change(GetPanelBySpecifer(specifer));
        }
        public void Change(UIPanel panel)
        {
            if (panel != null && !panel.Equals(activePanel))
            {
                prevPanels.Push(activePanel);
                activePanel.Close();
                activePanel = panel;
                activePanel.Open();
            }
        }
        public void Open(UIPanel panel)
        {
            if (!panel.Equals(activePanel))
            {
                prevPanels.Push(activePanel);
                activePanel = panel;
                activePanel.Open();
            }
        }
        public void Close(UIPanel panel)
        {
            if (panel.IsActive)
            {
                panel.Close();
            }
        }
        public void Back()
        {
            if (prevPanels.Count > 0)
            {
                UIPanel panel = prevPanels.Pop();
                activePanel.Close();
                activePanel = panel;
                activePanel.Open();
            }
        }

        public void StartTransaction(System.Action finishAction)
        {
            if (hasTransaction)
            {
                if (!transaction.InAnimation)
                {
                    transaction.SetFinishAction(finishAction);
                    transaction.Play();
                    group.interactable = false;
                }
            }
            else
            {
                finishAction.Invoke();
            }
        }
        public UIPanel GetPanelBySpecifer(string specifer)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                var panel = panels[i];
                if(string.Equals(panel.Specifer,specifer))
                    return panel;
            }
            return null;
        }
        public void Refresh()
        {
            if(hasTransaction)
            {
                transaction.Reverse();
            }
        }
        private void OnTransactionFinish()
        {
            group.interactable = true;
        }

#if UNITY_EDITOR
        [ContextMenu("Auto Set UIPanels")]
        private void EDITOR_AutoSetUIPanels()
        {
            panels = GetComponentsInChildren<UIPanel>();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}