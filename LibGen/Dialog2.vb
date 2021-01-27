Imports System.Windows.Forms

Public Class Dialog2
    Property codigoTeorema
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        codigoTeorema = "teorema:" & TextBoxTítulo.Text & ";" & TextBoxNombre.Text & ";" & TextBoxGrupo.Text & ";" & TextBoxExpr.Text & ";" & TextBoxPrueba.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

   
End Class
