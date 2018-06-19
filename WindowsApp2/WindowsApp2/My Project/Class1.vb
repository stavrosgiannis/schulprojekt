Public Class Class1

    Dim klassen() As String
    Public Function GetElementByName(ByVal elementName As String)

        Dim webb As New WebBrowser

        webb.Navigate("http://btineuss.rhein-kreis-neuss.de/UNTIS_HTML_Schueler/frames/navbar.htm")

        'Waiting for page to load function
        If webb.ReadyState = WebBrowserReadyState.Complete Then
            'Get Eleent by Name
            For Each element As HtmlElement In webb.Document.All
                If element.Name IsNot Nothing Then
                    If InStr(element.Name, elementName) Then
                        Debug.Print(element.Id & " Found!")

                        For i As Integer = 0 To 70
                            klassen(i) = (element.All.Item(i).InnerText)
                            Return klassen(i)
                        Next

                    End If
                End If
            Next
        End If
        Return False
    End Function
End Class
