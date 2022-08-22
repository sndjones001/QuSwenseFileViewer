using QuSwenseToolsMainWindow.Common;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace QuSwenseToolsMainWindow.ViewModel
{
    public partial class WindowThemeViewModel : ViewModelBase
    {
        private Window _thisWindow;
        private int _marginAroundWindowToAllowDropShadow = 10;
        private int _radiusWindowEdges = 10;
        private WindowDockPosition _dockPosition = WindowDockPosition.Undocked;

        public double WindowMinimumWidth { get; set; } = 400;
        public double WindowMinimumHeight { get; set; } = 400;

        /// <summary>
        /// True if the window should be borderless because it is docked or maximized
        /// </summary>
        public bool IsWindowBorderless { get => _thisWindow.WindowState == WindowState.Maximized || _dockPosition != WindowDockPosition.Undocked; }
        public int ResizeBorderAroundWindow { get; set; } = 6;
        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get => IsWindowBorderless ? 0 : _marginAroundWindowToAllowDropShadow;
            set => _marginAroundWindowToAllowDropShadow = value;
        }
        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness { get => new Thickness(ResizeBorderAroundWindow + OuterMarginSize); }
        public Thickness InnerContentPadding { get => new Thickness(ResizeBorderAroundWindow); }
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }
        public int WindowRadius
        {
            get => IsWindowBorderless ? 0 : _radiusWindowEdges;
            set => _radiusWindowEdges = value;
        }
        public CornerRadius WindowCornerRadius { get => new CornerRadius(WindowRadius); }
        public CornerRadius CloseButtonCornerRadius { get => new CornerRadius(0, WindowRadius, 0, 0); }
        public int TitleHeight { get; set; } = 42;
        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorderAroundWindow); } }
        public ICommand MinimizeCommand { get; protected set; }
        public ICommand MaximizeCommand { get; protected set; }
        public ICommand CloseCommand { get; protected set; }
        public ICommand MenuCommand { get; protected set; }

        public WindowThemeViewModel(Window window)
        {
            _thisWindow = window;

            // Listen out for the window resizing
            _thisWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };

            MinimizeCommand = new RelayCommand(() => _thisWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _thisWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(_thisWindow.Close);
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_thisWindow, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(_thisWindow);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                _dockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }

        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(_thisWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + _thisWindow.Left, position.Y + _thisWindow.Top);
        }

        private void WindowResized() 
        {
            // Fire off events for all properties that are affected by a resize
            NotifyPropertyChanged(nameof(IsWindowBorderless));
            NotifyPropertyChanged(nameof(ResizeBorderThickness));
            NotifyPropertyChanged(nameof(OuterMarginSize));
            NotifyPropertyChanged(nameof(OuterMarginSizeThickness));
            NotifyPropertyChanged(nameof(WindowRadius));
            NotifyPropertyChanged(nameof(WindowCornerRadius));
            NotifyPropertyChanged(nameof(CloseButtonCornerRadius));
        }
    }
}
