Imports Microsoft.Graph
Imports WindowsApp2.Klassen

Public Class Form2
    Public klassen(70) As String
    Public Function GetElementByName(ByVal elementName As String)
        ProgressBar1.Value = 0
        'Waiting for page to load function
        If WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then

            'Get Eleent by Name
            For Each element As HtmlElement In WebBrowser1.Document.All
                If element.Name IsNot Nothing Then
                    If InStr(element.Name, elementName) Then
                        Debug.Print(element.Id & " Found!")
                        For i As Integer = 0 To element.All.Count - 1
                            ProgressBar1.Maximum = element.All.Count - 1
                            'MsgBox(ProgressBar1.Maximum.ToString)
                            ProgressBar1.Increment(1)
                            klassen(i) = (element.All.Item(i).InnerText)
                            ListBox1.Items.Add(klassen(i))
                        Next
                    End If
                End If
            Next
        End If
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'NAVIGATE
        WebBrowser1.Navigate("http://btineuss.rhein-kreis-neuss.de/UNTIS_HTML_Schueler/frames/navbar.htm")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call GetElementByName("element")
    End Sub

End Class