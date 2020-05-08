Imports System
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Threading
Imports DevExpress.Mvvm

Namespace DXSample.ViewModels
	Public Class ChildViewModel
		Inherits ViewModelBase

		Protected ReadOnly Property CurrentWindowService() As ICurrentWindowService
			Get
				Return Me.GetService(Of ICurrentWindowService)()
			End Get
		End Property
		Private privateCloseWindowCommand As ICommand
		Public Property CloseWindowCommand() As ICommand
			Get
				Return privateCloseWindowCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateCloseWindowCommand = value
			End Set
		End Property
		Private privateTemporarilyHideWindowCommand As ICommand
		Public Property TemporarilyHideWindowCommand() As ICommand
			Get
				Return privateTemporarilyHideWindowCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateTemporarilyHideWindowCommand = value
			End Set
		End Property
		Public Property Caption() As String
			Get
				Return GetProperty(Function() Caption)
			End Get
			Set(ByVal value As String)
				SetProperty(Function() Caption, value)
			End Set
		End Property
		Public Property WindowState() As WindowState
			Get
				Return GetProperty(Function() WindowState)
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
			Me.CurrentWindowService.Close()
		End Sub
		Private Sub TemporarilyHideWindow()
			Me.CurrentWindowService.Hide()
			Dim timer = New DispatcherTimer() With {.Interval = TimeSpan.FromSeconds(3)}
			AddHandler timer.Tick, Sub(o, e)
				Me.CurrentWindowService.SetWindowState(WindowState.Maximized)
				Me.CurrentWindowService.Show()
				Me.CurrentWindowService.Activate()
				timer.Stop()
			End Sub
			timer.Start()
		End Sub
	End Class
End Namespace