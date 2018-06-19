Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Name: " & Form1.name
        Label2.Text = "Email: " & Form1.username
        For i As Integer = 0 To Form2.klassen.Count - 1
            If Form1.ResultText.Text.Contains(Form2.klassen(i).ToString) Then
                Label3.Text = ("Klasse: " & Form2.klassen(i).ToString)
            End If
        Next
    End Sub
End Class