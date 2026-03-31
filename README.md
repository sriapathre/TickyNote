# TickyNote

A modern, lightweight countdown timer app for Windows built with WPF and .NET 10.

![TickyNote](https://img.shields.io/badge/.NET-10.0-blue) ![WPF](https://img.shields.io/badge/WPF-Desktop-green) ![License](https://img.shields.io/badge/License-MIT-yellow)

## Features

- **Multiple Timers** - Create and manage multiple countdown timers simultaneously
- **Customizable Duration** - Set timers with days, hours, minutes, and seconds
- **Toast Notifications** - Get notified when timers complete
- **Theme Colors** - Choose from 7 beautiful color themes (Yellow, Green, Orange, Pink, Blue, Purple, Gray)
- **Always on Top** - Window stays visible above other applications
- **Draggable Window** - Borderless, modern UI that can be dragged anywhere
- **Auto-Save** - Timers are automatically saved and restored on app restart
- **Collapsible Input** - Input section collapses automatically for a cleaner view

## Screenshots

The app features a sticky note-style interface with:
- Rounded corners and subtle shadows
- Semi-transparent overlays
- Modern scrollbar design
- Windows 11-style icons

## Getting Started

### Prerequisites

- Windows 10/11
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/TickyNote.git
   ```

2. Navigate to the project directory:
   ```bash
   cd TickyNote
   ```

3. Build and run:
   ```bash
   dotnet run
   ```

## Usage

### Adding a Timer
1. Enter a title for your timer (e.g., "Meeting", "Break", "Workout")
2. Set the duration using Days, Hours, Mins, and Secs fields
3. Click **Start Countdown**

### Managing Timers
- **Edit Title**: Click on the timer title to edit it
- **Delete Timer**: Click the trash icon (🗑) next to a timer
- **Collapse Input**: Click "Add New Timer" toggle when timers exist

### Changing Theme
1. Click the settings icon (⚙) in the top-right corner
2. Select **Theme Color**
3. Choose from the available color swatches

### Window Controls
- **Drag**: Click and drag anywhere on the window to move it
- **Close**: Click the X button in the top-right corner

## Project Structure

```
TickyNote/
├── Converters/
│   └── BoolToExpanderIconConverter.cs
├── Models/
│   └── TimerItem.cs
├── Services/
│   ├── NotificationService.cs
│   └── TimerStore.cs
├── Themes/
│   └── ThemeResources.xaml
├── ViewModels/
│   ├── MainViewModel.cs
│   └── TimerItemViewModel.cs
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
└── MainWindow.xaml.cs
```

## Architecture

- **MVVM Pattern** - Clean separation of concerns using CommunityToolkit.Mvvm
- **Observable Properties** - Automatic UI updates via `[ObservableProperty]` attributes
- **Relay Commands** - Type-safe command binding with `[RelayCommand]` attributes
- **Resource Dictionary** - Centralized theming and styles

## Dependencies

- [CommunityToolkit.Mvvm](https://www.nuget.org/packages/CommunityToolkit.Mvvm) - MVVM framework
- [Microsoft.Toolkit.Uwp.Notifications](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications) - Toast notifications

## Data Storage

Timer data is stored locally at:
```
%LOCALAPPDATA%\TickyNote\timers.json
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Icons from [Segoe MDL2 Assets](https://docs.microsoft.com/en-us/windows/apps/design/style/segoe-ui-symbol-font)
- Color palette inspired by Tailwind CSS
