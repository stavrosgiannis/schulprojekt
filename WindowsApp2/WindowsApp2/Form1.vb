Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel

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

#Region "searchForUpdate"
    Dim WithEvents WC As New WebClient
    Dim url As String = "https://raw.githubusercontent.com/stavrosgiannis/schulprojekt/master/config.ini"
    Public Sub queryNewVersion()


        If My.Computer.FileSystem.FileExists(_inipath) = False Then
            My.Computer.Network.DownloadFile(url, _inipath)
            IniWriteValue("update", "url", url)
        Else
            My.Computer.FileSystem.DeleteFile(_inipath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            My.Computer.Network.DownloadFile(url, _inipath)
            IniWriteValue("update", "url", url)
        End If

        ReadUpdateFiles()

    End Sub

    Public Sub downloadUpdate()
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\update.exe") Then
            My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\update.exe", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            WC.DownloadFileAsync(New Uri(url), Application.StartupPath & "\update.exe")
        Else
            WC.DownloadFileAsync(New Uri(url), Application.StartupPath & "\update.exe")
        End If
    End Sub

    Public Sub extractbatchfile()
        FileIO.FileSystem.CopyFile(My.Resources.update, Application.StartupPath & "\update.bat")
        Process.Start(Application.StartupPath & "\update.bat")
    End Sub

    Public Sub ReadUpdateFiles()
        If My.Computer.FileSystem.FileExists(_inipath) Then
            Dim iniversion As String = IniReadValue("update", "version")
            If iniversion <= Application.ProductVersion Then

            Else
                downloadUpdate()
            End If
        End If
    End Sub


#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\update.bat") Then
                File.Delete(Application.StartupPath & "\update.bat")
            End If
            'Initializing frameworks
            queryNewVersion()



        Catch ex As Exception
            WriteToErrorLog(ex.Message, ex.StackTrace, "Exception")
            MsgBox("Oops! Looks like something went wrong..", MsgBoxStyle.Critical)
            Application.Exit()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' IniWriteValue("application", "Version", Application.ProductVersion)
    End Sub

    Private Sub WC_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles WC.DownloadProgressChanged
        ProgressBar1.Visible = True
        TextBox1.Visible = True
        ProgressBar1.Value = e.ProgressPercentage
        TextBox1.Text = e.ProgressPercentage & "% " & e.BytesReceived & "/" & e.TotalBytesToReceive
    End Sub

    Private Sub WC_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles WC.DownloadFileCompleted
        ProgressBar1.Visible = False
        TextBox1.Visible = False
    End Sub
End Class
