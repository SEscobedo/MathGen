Imports System.Windows.Forms

Public Class RichConsole
    Inherits RichTextBox

    Public Event InputChanged As EventHandler

    Public ReadOnly Property Input() As String
        Get
            Return Me.Text.Substring(InputStart).Replace(vbLf, "")
        End Get
    End Property

    Public Sub Write(txt As String)
        Me.AppendText(txt)
        InputStart = Me.SelectionStart
    End Sub

    Public Sub WriteLine(txt As String)
        Write(txt & vbLf)
    End Sub

    Private InputStart As Integer

    Protected Overrides Function ProcessCmdKey(ByRef m As Message, keyData As Keys) As Boolean
        '' Defeat backspace
        If (keyData = Keys.Back OrElse keyData = Keys.Left) AndAlso InputStart = Me.SelectionStart Then Return True
        '' Defeat up/down cursor keys
        If keyData = Keys.Up OrElse keyData = Keys.Down Then Return True
        '' Detect Enter key
        If keyData = Keys.[Return] Then
            Me.AppendText(vbLf)
            RaiseEvent InputChanged(Me, EventArgs.Empty)
            InputStart = Me.SelectionStart
            Return True
        End If
        Return MyBase.ProcessCmdKey(m, keyData)
    End Function

    Protected Overrides Sub WndProc(ByRef m As Message)
        '' Defeat the mouse
        If m.Msg >= &H200 AndAlso m.Msg <= &H209 Then Return
        MyBase.WndProc(m)
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)

    End Sub


End Class