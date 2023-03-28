Imports System.Windows.Input
Imports DevExpress.Mvvm

Namespace DXSample.ViewModels

    Public Class MainViewModel
        Inherits ViewModelBase

        Private _ShowChildWindowCommand As ICommand, _CloseChildWindowCommand As ICommand, _RestoreChildWindowCommand As ICommand

        Protected ReadOnly Property WindowService As IWindowService
            Get
                Return GetService(Of IWindowService)()
            End Get
        End Property

        Public Property ShowChildWindowCommand As ICommand
            Get
                Return _ShowChildWindowCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ShowChildWindowCommand = value
            End Set
        End Property

        Public Property CloseChildWindowCommand As ICommand
            Get
                Return _CloseChildWindowCommand
            End Get

            Private Set(ByVal value As ICommand)
                _CloseChildWindowCommand = value
            End Set
        End Property

        Public Property RestoreChildWindowCommand As ICommand
            Get
                Return _RestoreChildWindowCommand
            End Get

            Private Set(ByVal value As ICommand)
                _RestoreChildWindowCommand = value
            End Set
        End Property

        Public Property ChildWindowViewModel As ChildViewModel
            Get
                Return GetProperty(Function() Me.ChildWindowViewModel)
            End Get

            Set(ByVal value As ChildViewModel)
                SetProperty(Function() ChildWindowViewModel, value)
            End Set
        End Property

        Public Sub New()
            ShowChildWindowCommand = New DelegateCommand(AddressOf ShowChildWindow, AddressOf CanShowChildWindow)
            CloseChildWindowCommand = New DelegateCommand(AddressOf CloseChildWindow, AddressOf CanCloseChildWindow)
        End Sub

        Public Sub ShowChildWindow()
            If ChildWindowViewModel Is Nothing Then ChildWindowViewModel = New ChildViewModel() With {.Caption = "Hello, World!"}
            WindowService.Show(ChildWindowViewModel)
        End Sub

        Private Function CanShowChildWindow() As Boolean
            Return Not WindowService.IsWindowAlive
        End Function

        Private Sub CloseChildWindow()
            ChildWindowViewModel = Nothing
            WindowService.Close()
        End Sub

        Private Function CanCloseChildWindow() As Boolean
            Return WindowService.IsWindowAlive
        End Function
    End Class
End Namespace
