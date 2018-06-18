Imports Microsoft.Graph

Public Class Form2
    Public klassen(70) As String
    Private Function GetElementByName(ByVal elementName As String)

        'Waiting for page to load function
        If WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then

            'Get Eleent by Name
            For Each element As HtmlElement In WebBrowser1.Document.All
                If element.Name IsNot Nothing Then
                    If InStr(element.Name, elementName) Then
                        Debug.Print(element.Id & " Found!")

                        For i As Integer = 0 To 70
                            klassen(i) = (element.All.Item(i).InnerText)
                            ListBox1.Items.Add(klassen(i))
                        Next

                    End If
                End If

            Next


            '\/ - Perform Actions
            'WebBrowser1.Document.GetElementById(fb_button).SetAttribute("value", Password) 'or InvokeMember("submit") or InvokeMember("click")
            'MsgBox("Done")
        End If
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'NAVIGATE
        WebBrowser1.Navigate("http://btineuss.rhein-kreis-neuss.de/UNTIS_HTML_Schueler/frames/navbar.htm")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call GetElementByName("element")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub
End Class