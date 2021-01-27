Public Class FrmExperiment
    Private Sub RichTextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then




            'reescribir texto de usuario
            RichTextBox1.AppendText(vbCrLf)
            RichTextBox1.AppendText(DelTrim(RichTextBox2.Text))

            'escribir respuesta del programa
            RichTextBox1.AppendText(vbCrLf)
            Dim r As response = IO(DelTrim(RichTextBox2.Text))

            If Not r.verbal = "" Then RichTextBox1.AppendText(">> " & r.verbal)
            If Not r.factual = "" Then Ejecutar(r.factual)

            'reajustar
            On Error Resume Next
            RichTextBox1.SelectionStart = RichTextBox1.TextLength
                RichTextBox1.ScrollToCaret()
                RichTextBox2.Clear()

            End If
    End Sub

    Private Sub RichTextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles RichTextBox2.KeyUp

    End Sub

    Private Sub RichTextBox2_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox2.TextChanged

    End Sub

    Sub Ejecutar(s As String)
        If s = "clear1" Then
            RichTextBox1.Clear()
        ElseIf s = "clear2" Then
            RichTextBox2.Clear()
        ElseIf s = "close" Then
            Me.Close()
        End If
    End Sub

    Function DelTrim(s As String) As String
        s = Replace(s, vbLf, "")
        s = Replace(s, vbLf, "")
        Return s
    End Function

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub FrmExperiment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class