Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim L As New book
        'Dim L As New libro
        '        Dim T As New teoría
        'T = T.getTeoría(RichTextBox1.Lines)
        ' Libro_de_prueba(L)
        'RichTextBox2.Text = L.getLibro(T)
        'RichTextBox2.Text = GenBook(L)
        RichTextBox2.Text = Experiment()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = ".txt|*.txt"
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                RichTextBox1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.PlainText)
            Catch ex As Exception
                MsgBox(Err.Description)
            End Try

        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dialog1.codigoDefinición = ""
        If Dialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            RichTextBox1.SelectedText = Dialog1.codigoDefinición
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Dialog2.codigoTeorema = ""
        If Dialog2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            RichTextBox1.SelectedText = Dialog2.codigoTeorema
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Dialog3.codigoAxioma = ""
        If Dialog3.ShowDialog() = Windows.Forms.DialogResult.OK Then
            RichTextBox1.SelectedText = Dialog3.codigoAxioma
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f As New FrmExperiment
        f.Show()
    End Sub
End Class
