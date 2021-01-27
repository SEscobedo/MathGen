Imports System.Windows.Forms

Public Class Dialog3
    Property codigoAxioma As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        codigoAxioma = "axioma:" & TextBoxTítulo.Text & ";" & TextBoxNombre.Text & ";" & TextBoxProposición.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
