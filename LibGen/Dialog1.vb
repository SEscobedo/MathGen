Imports System.Windows.Forms

Public Class Dialog1
    Property codigoDefinición As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        'Generar código
        If CheckBox1.Checked Then
            codigoDefinición = "Definición:"
        Else
            codigoDefinición = "definición:"
        End If
        codigoDefinición &= TextBoxNombre.Text & ";"
        codigoDefinición &= TextBoxNombre2.Text & ";"
        codigoDefinición &= TextBoxClase.Text & ";"
        codigoDefinición &= TextBoxExpr.Text & ";"
        codigoDefinición &= TextBoxFormula.Text & ";"

        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
