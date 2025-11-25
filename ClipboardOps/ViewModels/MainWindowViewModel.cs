using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClipboardOps.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _text = "Avalonia is a cross-platform UI framework for dotnet, providing a" +
                                                 " flexible styling system and supporting a wide range of platforms" +
                                                 " such as Windows, macOS, Linux, iOS, Android and WebAssembly. ";

    [ObservableProperty] private int _caretIndex;

    [RelayCommand]
    private async Task CopyText(CancellationToken token)
    {
        ErrorMessages?.Clear();
        try
        {
            await DoSetClipboardTextAsync(Text);
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
    }

    // 代码生成器,可以让命令更简洁. 来自CommunityToolkit.Mvvm.Input;
    [RelayCommand]
    private async Task PasteText(CancellationToken token)
    {
        ErrorMessages?.Clear();
        try
        {
            if (await DoGetClipboardTextAsync() is { } pastedText)
                Text = Text?.Insert(CaretIndex, pastedText);
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
    }

    private async Task DoSetClipboardTextAsync(string? text)
    {
        // For learning purposes, we opted to directly get the reference
        // for StorageProvider APIs here inside the ViewModel.

        // For your real-world apps, you should follow the MVVM principles
        // by making service classes and locating them with DI/IoC.

        // See DepInject project for a sample of how to accomplish this.
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.Clipboard is not { } provider)
            throw new NullReferenceException("Missing Clipboard instance.");

        // is not {}是 C# 9.0 引入的模式匹配语法，用于检查对象是否为特定类型并进行解构赋值。
        // is not {} 和 is not null 在效果上是等价的

        // Avalonia 有 Clipboard API,同时这个 API 是异步的设计风格

        await provider.SetTextAsync(text);
    }

    private async Task<string?> DoGetClipboardTextAsync()
    {
        // For learning purposes, we opted to directly get the reference
        // for StorageProvider APIs here inside the ViewModel.

        // For your real-world apps, you should follow the MVVM principles
        // by making service classes and locating them with DI/IoC.

        // See DepInject project for a sample of how to accomplish this.
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.Clipboard is not { } provider)
            throw new NullReferenceException("Missing Clipboard instance.");

        return await provider.GetTextAsync();
    }
}