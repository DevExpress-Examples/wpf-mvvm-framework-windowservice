Imports System
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Threading
Imports DevExpress.Mvvm

Namespace DXSample.ViewModels

    Public Class ChildViewModel
        Inherits ViewModelBase

        Private _CloseWindowCommand As ICommand, _TemporarilyHideWindowCommand As ICommand

        Protected ReadOnly Property CurrentWindowService As ICurrentWindowService
            Get
                Return GetService(Of ICurrentWindowService)()
            End Get
        End Property

        Public Property CloseWindowCommand As ICommand
            Get
                Return _CloseWindowCommand
            End Get

            Private Set(ByVal value As ICommand)
                _CloseWindowCommand = value
            End Set
        End Property

        Public Property TemporarilyHideWindowCommand As ICommand
            Get
                Return _TemporarilyHideWindowCommand
            End Get

            Private Set(ByVal value As ICommand)
                _TemporarilyHideWindowCommand = value
            End Set
        End Property

        Public Property Caption As String
            Get
                Return GetProperty(Function() Me.Caption)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() Caption, value)
            End Set
        End Property

        Public Property WindowState As WindowState
            Get
                Return GetProperty(Function() Me.WindowState)
            End Get

            Set(ByVal value As WindowState)
                SetProperty(Function() WindowState, value)
            End Set
        End Property

        Public Sub New()
            CloseWindowCommand = New DelegateCommand(AddressOf CloseWindow)
            TemporarilyHideWindowCommand = New DelegateCommand(AddressOf TemporarilyHideWindow)
        End Sub

        Private Sub CloseWindow()
            CurrentWindowService.Close()
        End Sub

        Private Sub TemporarilyHideWindow()
            CurrentWindowService.Hide()
            Dim timer = New DispatcherTimer() With {.Interval = TimeSpan.FromSeconds(3)}
            AddHandler timer.Tick, Sub(o, e)
                CurrentWindowService.SetWindowState(WindowState.Maximized)
                CurrentWindowService.Show()
                CurrentWindowService.Activate()
                timer.Stop()
            End Sub
            timer.Start()
        End Sub
    End Class
End Namespace
