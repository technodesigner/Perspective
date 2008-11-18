using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows;
using System.Windows.Threading;
using Perspective.Wpf3D.Controls;

namespace Perspective.Wpf3D.Automation.Peers
{
    /// <summary>
    /// Exposes Button3D type to UI Automation.
    /// </summary>
    public class Button3DAutomationPeer : UIElement3DAutomationPeer, IInvokeProvider
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner"></param>
        public Button3DAutomationPeer(UIElement3D owner) : base(owner)
        {
        }
        
        /// <summary>
        /// Gets the name of the control that is associated with this UI Automation peer.
        /// </summary>
        /// <returns>A string that contains the class name.</returns>
        protected override string GetClassNameCore()
        {
            return "Button3D";
        }

        /// <summary>
        /// Gets the control type of the element that is associated with the UI Automation peer.
        /// </summary>
        /// <returns>The AutomationControlType enumeration Button value.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Button;
        }

        // TODO ? (évite d'instrumenter le XAML avec AutomationProperties.AutomationId=...
        ///// <summary>
        ///// Gets the AutomationId for the element associated with this Button3DAutomationPeer.
        ///// </summary>
        ///// <returns>A string that contains the Id.</returns>
        //protected override string GetAutomationIdCore()
        //{
        //    return "Button3D";
        // Here is the code for FrameworkElementAutomationPeer :
        //       string automationIdCore = base.GetAutomationIdCore();
        //if (string.IsNullOrEmpty(automationIdCore))
        //{
        //    FrameworkElement owner = (FrameworkElement) base.Owner;
        //    automationIdCore = DefinitionProperties.GetUid(owner);
        //    if (string.IsNullOrEmpty(automationIdCore))
        //    {
        //        automationIdCore = owner.Name;
        //    }
        //}
        //return (automationIdCore ?? string.Empty);

        //}

        // TODO ? (évite d'instrumenter le XAML avec AutomationProperties.Name=...
        ///// <summary>
        ///// Gets the name of the class of the element associated with this Button3DAutomationPeer.
        ///// </summary>
        ///// <returns>A string that contains the class name.</returns>
        //protected override string GetNameCore()
        //{
        //    return "Button3D";
        //// Here is the code for FrameworkElementAutomationPeer :
        //        string nameCore = base.GetNameCore();
        //if (string.IsNullOrEmpty(nameCore))
        //{
        //    AutomationPeer labeledByCore = this.GetLabeledByCore();
        //    if (labeledByCore != null)
        //    {
        //        nameCore = labeledByCore.GetName();
        //    }
        //    if (string.IsNullOrEmpty(nameCore))
        //    {
        //        nameCore = ((FrameworkElement) base.Owner).GetPlainText();
        //    }
        //}
        //return (nameCore ?? string.Empty);

        //}

        /// <summary>
        /// Gets the object that supports the specified control pattern of the element that is associated with this automation peer.
        /// </summary>
        /// <param name="patternInterface">A value in the PatternInterface enumeration.</param>
        /// <returns>If patternInterface is Invoke, this method returns a this pointer, otherwise this method returns null.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.Invoke)
            {
                return this;
            }
            return base.GetPattern(patternInterface);
        }

        #region IInvokeProvider Members

        /// <summary>
        /// Sends a request to activate a control and initiate its single, unambiguous action.
        /// </summary>
        public void Invoke()
        {
            // Executes the specified delegate asynchronously on the thread the AutomationPeer's Dispatcher is associated with.
            base.Dispatcher.BeginInvoke(
                DispatcherPriority.Input,
                new Button3DClickDelegate(Button3DClick));
        }

        #endregion

        private delegate void Button3DClickDelegate();

        private void Button3DClick()
        {
            ((Button3D)base.Owner).AutomationButton3DClick();
        }
    }
}
