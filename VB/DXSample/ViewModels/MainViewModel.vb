Imports System.Windows.Input
Imports DevExpress.Mvvm

Namespace DXSample.ViewModels
	Public Class MainViewModel
		Inherits ViewModelBase

		Protected ReadOnly Property WindowService() As IWindowService
			Get
				Return Me.GetService(Of IWindowService)()
			End Get
		End Property
		Private privateShowChildWindowCommand As ICommand
		Public Property ShowChildWindowCommand() As ICommand
			Get
				Return privateShowChildWindowCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateShowChildWindowCommand = value
			End Set
		End Property
		Private privateCloseChildWindowCommand As ICommand
		Public Property CloseChildWindowCommand() As ICommand
			Get
				Return privateCloseChildWindowCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateCloseChildWindowCommand = value
			End Set
		End Property
		Private privateRestoreChildWindowCommand As ICommand
		Public Property RestoreChildWindowCommand() As ICommand
			Get
				Return privateRestoreChildWindowCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateRestoreChildWindowCommand = value
			End Set
		End Property
		Public Property ChildWindowViewModel() As ChildViewModel
			Get
				Return GetValue(Of ChildViewModel)()
			End Get
			Set(ByVal value As ChildViewModel)
				SetValue(value)
			End Set
		End Property
		Public Sub New()
			ShowChildWindowCommand = New DelegateCommand(AddressOf ShowChildWindow, AddressOf CanShowChildWindow)
			CloseChildWindowCommand = New DelegateCommand(AddressOf CloseChildWindow, AddressOf CanCloseChildWindow)
		End Sub

		Public Sub ShowChildWindow()
			If ChildWindowViewModel Is Nothing Then
				ChildWindowViewModel = New ChildViewModel() With {.Caption = "Hello, World!"}
			End If
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