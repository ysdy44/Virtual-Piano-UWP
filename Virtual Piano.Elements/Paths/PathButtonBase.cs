using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Virtual_Piano.Elements
{
    [TemplateVisualState(Name = Focused, GroupName = FocusStates)]
    [TemplateVisualState(Name = Unfocused, GroupName = FocusStates)]
    [TemplateVisualState(Name = PointerFocused, GroupName = FocusStates)]
    public abstract class PathButtonBase : Control
    {
        //@Const
        const string FocusStates = "FocusStates";
        const string Focused = "Focused";
        const string Unfocused = "Unfocused";
        const string PointerFocused = "PointerFocused";

        //@Command
        public ICommand Command { get; set; }

        public ClickMode ClickMode { get; set; } = ClickMode.Release;
        public bool IsPressed { get; private set; }

        #region DependencyProperty

        /// <summary> Gets or set the Data for <see cref="Path"/>. </summary>
        public Geometry Data
        {
            get => (Geometry)base.GetValue(DataProperty);
            set => base.SetValue(DataProperty, value);
        }
        /// <summary> Identifies the <see cref = "PathButtonBase.Data" /> dependency property. </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(PathButtonBase), new PropertyMetadata(Geometry.Empty));

        #endregion

        //@Construct
        protected PathButtonBase()
        {
            base.LostFocus += (s, e) => this.Update(base.FocusState);
            base.GotFocus += (s, e) => this.Update(base.FocusState);

            base.PreviewKeyUp += (s, e) =>
            {
                switch (this.ClickMode)
                {
                    case ClickMode.Release:
                        switch (base.FocusState)
                        {
                            case FocusState.Keyboard:
                                switch (e.Key)
                                {
                                    case VirtualKey.Space:
                                    case VirtualKey.Enter:
                                        this.OnClick();
                                        break;
                                    default:
                                        break;
                                }
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
                switch (this.ClickMode)
                {
                    case ClickMode.Press:
                        switch (base.FocusState)
                        {
                            case FocusState.Keyboard:
                                switch (e.Key)
                                {
                                    case VirtualKey.Space:
                                    case VirtualKey.Enter:
                                        this.OnClick();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            };

            base.PointerCanceled += (s, e) => this.Update(false);
            base.PointerCaptureLost += (s, e) => this.Update(false);

            base.PointerExited += (s, e) => this.Update(false);
            base.PointerEntered += (s, e) => this.Update(e.Pointer.IsInContact);

            base.PointerReleased += (s, e) =>
            {
                this.Update(false);
                switch (this.ClickMode)
                {
                    case ClickMode.Release:
                        this.OnClick();
                        break;
                    default:
                        break;
                }
            };
            base.PointerPressed += (s, e) =>
            {
                this.Update(true);
                switch (this.ClickMode)
                {
                    case ClickMode.Press:
                        this.OnClick();
                        break;
                    default:
                        break;
                }
            };
        }

        //@Abstract
        public abstract void OnClick();
        public abstract void GoToState(bool value);

        private void Update(bool value)
        {
            if (this.IsPressed == value) return;
            this.IsPressed = value;
            this.GoToState(value);
        }
        private void Update(FocusState value)
        {
            switch (value)
            {
                case FocusState.Keyboard:
                case FocusState.Programmatic:
                    VisualStateManager.GoToState(this, Focused, false);
                    break;

                case FocusState.Pointer:
                    VisualStateManager.GoToState(this, PointerFocused, false);
                    break;

                default:
                    VisualStateManager.GoToState(this, Unfocused, false);
                    break;
            }
        }
    }
}