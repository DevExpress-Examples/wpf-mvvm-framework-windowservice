Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace DXSample.ViewModels

    Public Class MainViewModel
        Inherits ViewModelBase

        Protected ReadOnly Property WindowService As IWindowService
            Get
                Return GetService(Of IWindowService)()
            End Get
        End Property

        Public Property ChildWindowViewModel As ChildViewModel
            Get
                Return GetValue(Of ChildViewModel)()
            End Get

            Set(ByVal value As ChildViewModel)
                SetValue(value)
            End Set
        End Property

        <Command>
        Public Sub ShowChildWindow()
            If ChildWindowViewModel Is Nothing Then ChildWindowViewModel = New ChildViewModel() With {.Caption = "Hello, World!"}
            WindowService.Show(ChildWindowViewModel)
        End Sub

        Public Function CanShowChildWindow() As Boolean
            Return Not WindowService.IsWindowAlive
        End Function

        <Command>
        Public Sub CloseChildWindow()
            ChildWindowViewModel = Nothing
            WindowService.Close()
        End Sub

        Public Function CanCloseChildWindow() As Boolean
            Return WindowService.IsWindowAlive
        End Function
    End Class
End Namespace
