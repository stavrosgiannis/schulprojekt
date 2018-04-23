Imports System.IO
Imports System
Imports System.Runtime.InteropServices
Imports System.Text
Public Class Form1

#Region "ini. API"
    ''' <summary>
    ''' Create a New INI file to store or load data
    ''' </summary>

    Public _inipath As String = Application.StartupPath & "\config.ini"

    Private Declare Function WritePrivateProfileStringA Lib "kernel32" (ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long

    Private Declare Function GetPrivateProfileStringA Lib "kernel32" (ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer

    ''' <summary>
    ''' Write Data to the INI File
    ''' </summary>
    ''' <PARAM name="Section"></PARAM>
    ''' Section name
    ''' <PARAM name="Key"></PARAM>
    ''' Key Name
    ''' <PARAM name="Value"></PARAM>
    ''' Value Name
    Public Sub IniWriteValue(ByVal Section As String, ByVal Key As String, ByVal Value As String)
        WritePrivateProfileStringA(Section, Key, Value, Me._inipath)
    End Sub

    ''' <summary>
    ''' Read Data Value From the Ini File
    ''' </summary>
    ''' <PARAM name="Section"></PARAM>
    ''' <PARAM name="Key"></PARAM>
    ''' <PARAM name="Path"></PARAM>
    ''' <returns></returns>
    Public Function IniReadValue(ByVal Section As String, ByVal Key As String) As String
        Dim temp As StringBuilder = New StringBuilder(255)
        Dim i As Integer = GetPrivateProfileStringA(Section, Key, "", temp, 255, Me._inipath)
        Return temp.ToString
    End Function
#End Region

#Region "Error Logger"
    '*************************************************************
    'NAME:          WriteToErrorLog
    'PURPOSE:       Open or create an error log and submit error message
    'PARAMETERS:    msg - message to be written to error file
    '               stkTrace - stack trace from error message
    '               title - title of the error file entry
    'RETURNS:       Nothing
    '*************************************************************
    Public Sub WriteToErrorLog(ByVal msg As String,
           ByVal stkTrace As String, ByVal title As String)

        'check and make the directory if necessary; this is set to look in 
        'the Application folder, you may wish to place the error log in 
        'another Location depending upon the user's role and write access to 
        'different areas of the file system
        If Not System.IO.Directory.Exists(Application.StartupPath &
    "\Errors\") Then
            System.IO.Directory.CreateDirectory(Application.StartupPath &
            "\Errors\")
        End If

        'check the file
        Dim fs As FileStream = New FileStream(Application.StartupPath &
        "\Errors\errlog.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite)
        Dim s As StreamWriter = New StreamWriter(fs)
        s.Close()
        fs.Close()

        'log it
        Dim fs1 As FileStream = New FileStream(Application.StartupPath &
        "\Errors\errlog.txt", FileMode.Append, FileAccess.Write)
        Dim s1 As StreamWriter = New StreamWriter(fs1)
        s1.Write("Title: " & title & vbCrLf)
        s1.Write("Message: " & msg & vbCrLf)
        s1.Write("StackTrace: " & stkTrace & vbCrLf)
        s1.Write("Date/Time: " & DateTime.Now.ToString() & vbCrLf)
        s1.Write("================================================" & vbCrLf)
        s1.Close()
        fs1.Close()

    End Sub
#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try



            'Initializing frameworks
            If My.Computer.FileSystem.FileExists(_inipath) Then
                MsgBox(IniReadValue("SECTION", "KEY"))
            Else


            End If
        Catch ex As Exception
            WriteToErrorLog(ex.Message, ex.StackTrace, "Exception")
            MsgBox("Oops! Looks like something went wrong..", MsgBoxStyle.Critical)
            Application.Exit()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        IniWriteValue("SECTION", "KEY", "VALUE")
    End Sub
End Class
