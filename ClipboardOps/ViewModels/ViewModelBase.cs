using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClipboardOps.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    protected ViewModelBase()
    {
        ErrorMessages = new ObservableCollection<string>();
    }

    // 代码生成器,可以让属性更简洁. 来自CommunityToolkit.Mvvm.ComponentModel;
    // 除了常用类型, 还可以用在 ObservableCollection<T> 类型
    [ObservableProperty]
    private ObservableCollection<string>? _errorMessages;
}