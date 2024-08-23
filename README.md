<img src="https://raw.githubusercontent.com/dwndland/Chapter.Net.WPF/master/Icon.png" alt="logo" width="64"/>

# Chapter.Net.WPF Library

## Overview
Brings additional behavior and features to WPF in general.

## Features
- **BindingAdapter:** Allows to modify a binding by using another bindings to their properties.
- **ControlFocus:** Brings an easy possibility to focus another UI control.
- **IconReader:** Provides a way to read file, extension and folder icons with and without caching.
- **InputWatcher:** Allows to listen for global keyboard or mouse actions without the app be in focus.
- **PopupHandler:** Implements an auto close event for custom WPF popups.
- **SystemTexts:** Loads translations for the current windows language from the system.
- **UIDispatcher:** Gives easy access to the current UI dispatcher and allows override for unit tests.
- **VisualTreeAssist:** Provides an easy way to seach for UI controls by any condition in any direction in an easy way.
- **WindowHooks:** Gives easy ways to hook into the WinAPI queue with custom callbacks.
- **WindowObserver:** Gives easy ways to WinAPI events on the current window.

## Getting Started

1. **Installation:**
    - Install the Chapter.Net.WPF library via NuGet Package Manager:
    ```bash
    dotnet add package Chapter.Net.WPF
    ```

2. **BindingAdapter:**
    - Bind a binding converter and bind a tool tip fallback value.
    ```csharp
    <TextBlock Text="{Binding Demo}" ToolTip="{Binding AnyTag}">
        <Helpers:BindingAdapter.BindingExtensions>
            <Helpers:BindingExtensionCollection>
                <Helpers:BindingExtension Property="TextBlock.Text"
                                            Converter="{Binding DemoConverter}"
                                            ConverterParameter="{Binding DemoConverterParameter}" />
                <Helpers:BindingExtension Property="TextBlock.ToolTip"
                                            FallbackValue="{Binding BindingFallbackValue}" />
            </Helpers:BindingExtensionCollection>
        </Helpers:BindingAdapter.BindingExtensions>
    </TextBlock>
    ```

3. **ControlFocus:**
    - Usage.
    ```csharp
    public class MyControl : Control
    {
        protected void OnGotFocus()
        {
            ControlFocus.GiveFocus(myButton);
        }
    }
    ```

4. **IconReader:**
    ```csharp
    Icon icon = new Icon
    {
        Source = IconReader.FileIcon("C:\\path\\Demo.exe", false, false)
    }
    ```

5. **InputWatcher:**
    - Watch global keyboard events.
    ```csharp
    public class DemoClass : IDisposable
    {
        private SubscribeToken _token;

        public void Demo()
        {
            var watcher = new InputWatcher();

            _token = watcher.Observe(new KeyboardInput(Key.Delete, ModifierKeys.Control, OnDeletePress));

            watcher.Start();
            //watcher.Stop();
        }

        private void OnDeletePress(KeyboardEventArgs obj)
        {
            //obj.Key
            //obj.KeyPressState
            //obj.ModifierKeys
        }

        public void Dispose()
        {
            _token.Dispose();
        }
    }
    ```
    - Watch global mouse events.
    ```csharp
    public class DemoClass : IDisposable
    {
        private SubscribeToken _token;

        public void Demo()
        {
            var watcher = new InputWatcher();

            _token = watcher.Observe(new MouseInput(MouseAction.RightDoubleClick, OnRightDoubleClick));

            watcher.Start();
            //watcher.Stop();
        }

        private void OnRightDoubleClick(MouseEventArgs obj)
        {
            //obj.Modifiers
        }

        public void Dispose()
        {
            _token.Dispose();
        }
    }
    ```

6. **PopupHandler:**
    - Auto close a custom popup.
    ```csharp
    public class Control : ContentControl
    {
        private PopupHandler _popupHandler;

        public override void OnApplyTemplate()
        {
            var popup = GetTemplateChild("PART_Popup") as Popup;
            if (popup == null)
                return;

            _popupHandler = new PopupHandler();
            _popupHandler.AutoClose(popup, OnPopupClosed);
        }

        private void OnPopupClosed()
        {
        }
    }
    ```

7. **SystemTexts:**
    - Load the translation for the "Cancel" button from windows.
    ```csharp
    var cancelLabel = SystemTexts.GetString(SystemTexts.CANCEL_CAPTION);
    ```

8. **UIDispatcher:**
    - Use the UI dispatcher and override on unit tests.
    ```csharp
    public void ViewModel : ObservableObject
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => NotifyAndSetIfChanged(ref _name, value);
        }

        public void Update(string name)
        {
            UIDispatcher.Current.Invoke(() => Name = name);
        }
    }
    ```
    ```csharp
    [TestFixture]
    public class ViewModelTests
    {
        private ViewModel _target;

        [SetUp]
        public void Setup()
        {
            UIDispatcher.Override(Dispatcher.CurrentDispatcher);

            _target = new ViewModel();
        }

        [Test]
        public void Update_Called_SetsTheProperty()
        {
            _target.Update("Peter");

            Assert.That(_target.Name, Is.EqualTo("Peter"));
        }
    }
    ```

9. **VisualTreeAssist:**
    - Find the first child button.
    ```csharp
    var childButton = VisualTreeAssist.FindChild<Button>(this);
    ```
    - Find a child text box with a name.
    ```csharp
    var namedChildTextBox = VisualTreeAssist.FindNamedChild<TextBox>(this, "PART_TextBox");
    ```
    - Find parent user control but stop at the current window.
    ```csharp
    var firstUserControlInWindow = VisualTreeAssist.GetParentsUntil<UserControl, Window>(this);
    ```

10. **WindowHooks:**
    - Hook in to global keyboard events
    ```csharp
    public void HookIn()
    {
        var windowKeyboardHooks = new WindowHooks();
        _windowKeyboardHooks.HookIn(process, WH.KEYBOARD_LL, KeyboardEventReceived);
    }
    
    private void KeyboardEventReceived(int code, IntPtr wParam, IntPtr lParam)
    {
    }
    
    public void HookOut()
    {
        _windowKeyboardHooks.HookOut();
    }
    ```

11. **WindowObserver:**
    - Do something if user double clicked the window title bar.
    ```csharp
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
    
            var observer = new WindowObserver(this);
            observer.AddCallback(OnEventHappened);
        }
    
        private void OnEventHappened(NotifyEventArgs e)
        {
            if (e.MessageId == Chapter.Net.WinAPI.Data.WM.NCLBUTTONDBLCLK)
            {
                // User double clicked in the non client area (title bar most likely)
            }
        }
    }
    ```
    ```csharp
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
    
            var observer = new WindowObserver(this);
            observer.AddCallbackFor(Chapter.Net.WinAPI.Data.WM.NCLBUTTONDBLCLK, OnEventHappened);
        }
    
        private void OnEventHappened(NotifyEventArgs e)
        {
            // User double clicked in the non client area (title bar most likely)
        }
    }
    ```

## Links
* [NuGet](https://www.nuget.org/packages/Chapter.Net.WPF)
* [GitHub](https://github.com/dwndland/Chapter.Net.WPF)

## License
Copyright (c) David Wendland. All rights reserved.
Licensed under the MIT License. See LICENSE file in the project root for full license information.
