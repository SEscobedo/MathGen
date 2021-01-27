Public Class FrmConsola


    Private Sub FrmConsola_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub FrmConsola_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

    End Sub


    Private Sub RichConsole1_TextChanged(sender As Object, e As EventArgs) Handles RichConsole1.TextChanged



        If RichConsole1.Text.EndsWith("def") Then
            RichConsole1.Select(RichConsole1.TextLength - 3, 3)
            RichConsole1.SelectionColor = Color.GreenYellow
            RichConsole1.Select(RichConsole1.TextLength, RichConsole1.TextLength)
            RichConsole1.SelectionColor = Color.LightGray
        ElseIf RichConsole1.Text.EndsWith("as") Then
            RichConsole1.Select(RichConsole1.TextLength - 2, 2)
            RichConsole1.SelectionColor = Color.GreenYellow
            RichConsole1.Select(RichConsole1.TextLength, RichConsole1.TextLength)
            RichConsole1.SelectionColor = Color.LightGray
        ElseIf RichConsole1.Text.EndsWith("theorem") Then
            RichConsole1.Select(RichConsole1.TextLength - 7, 7)
            RichConsole1.SelectionColor = Color.GreenYellow
            RichConsole1.Select(RichConsole1.TextLength, RichConsole1.TextLength)
            RichConsole1.SelectionColor = Color.LightGray
        ElseIf RichConsole1.Text.StartsWith("%") Then
            RichConsole1.Select(RichConsole1.TextLength - 1, 1)
            RichConsole1.SelectionColor = Color.Black
            RichConsole1.Select(RichConsole1.TextLength, RichConsole1.TextLength)
            RichConsole1.SelectionColor = Color.LightGray
        Else
            RichConsole1.SelectionColor = Color.LightGray
        End If
    End Sub
End Class