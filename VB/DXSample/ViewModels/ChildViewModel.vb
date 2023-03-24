Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Windows
Imports System.Windows.Threading

Namespace DXSample.ViewModels

    Public Class ChildViewModel
        Inherits ViewModelBase

        Protected ReadOnly Property CurrentWindowService As ICurrentWindowService
            Get
                Return GetService(Of ICurrentWindowService)()
            End Get
        End Property

        Public Property Caption As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property WindowState As WindowState
            Get
                Return GetValue(Of WindowState)()
            End Get

            Set(ByVal value As WindowState)
                SetValue(value)
            End Set
        End Property

        <Command>
        Public Sub CloseWindow()
            CurrentWindowService.Close()
        End Sub

        <Command>
        Public Sub TemporarilyHideWindow()
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
