using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.System;

namespace Virtual_Piano.Midi.Core
{
    [TemplateVisualState(Name = Normal)]
    [TemplateVisualState(Name = Pressed)]
    public abstract class ContactButton : ContentControl
    {
        const string Normal = "Normal";
        const string Pressed = "Pressed";

        readonly ICollection<uint> Contacts = new HashSet<uint>();
        bool IsInContact;

        protected abstract void OnContactOn();
        protected abstract void OnContactOff();

        public ContactButton()
        {
            base.Unloaded += (s, e) => this.Clear();
            base.Loaded += (s, e) => this.Clear();

            base.PreviewKeyUp += (s, e) =>
            {
                switch (base.FocusState)
                {
                    case FocusState.Keyboard:
                        switch (e.Key)
                        {
                            case VirtualKey.Space:
                            case VirtualKey.Enter:
                                this.Clear();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            };
            base.PreviewKeyDown += (s, e) =>
            {
                switch (base.FocusState)
                {
                    case FocusState.Keyboard:
                        switch (e.Key)
                        {
                            case VirtualKey.Space:
                            case VirtualKey.Enter:
                                this.Add();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            };

            base.PointerCanceled += (s, e) => this.Clear();
            base.PointerCaptureLost += (s, e) => this.Clear();

            base.PointerReleased += (s, e) => this.Remove(e.Pointer.PointerId);
            base.PointerPressed += (s, e) => this.Add(e.Pointer.PointerId);

            base.PointerExited += (s, e) => this.Remove(e.Pointer.PointerId);
            base.PointerEntered += (s, e) =>
            {
                if (e.Pointer.IsInContact)
                {
                    this.Add(e.Pointer.PointerId);
                }
            };
        }

        private void Remove(uint id)
        {
            if (this.Contacts.Remove(id))
            {
                if (this.Contacts.Count == 0)
                {
                    if (this.IsInContact == false) return;
                    this.IsInContact = false;

                    VisualStateManager.GoToState(this, Normal, false);
                    this.OnContactOff();
                }
                else
                {
                    if (this.IsInContact == true) return;
                    this.IsInContact = true;

                    VisualStateManager.GoToState(this, Pressed, false);
                    this.OnContactOn();
                }
            }
        }
        private void Add(uint id)
        {
            this.Contacts.Add(id);

            if (this.IsInContact == true) return;
            this.IsInContact = true;

            VisualStateManager.GoToState(this, Pressed, false);
            this.OnContactOn();
        }

        public void Add()
        {
            this.Contacts.Add(0);

            if (this.IsInContact == true) return;
            this.IsInContact = true;

            VisualStateManager.GoToState(this, Pressed, false);
            this.OnContactOn();
        }
        public void Clear()
        {
            this.Contacts.Clear();

            if (this.IsInContact == false) return;
            this.IsInContact = false;

            VisualStateManager.GoToState(this, Normal, false);
            this.OnContactOff();
        }
    }
}