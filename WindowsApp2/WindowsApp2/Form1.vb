Imports System.IO
Imports System.Net
Imports System.DirectoryServices
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
    Dim urlini As String = "https://raw.githubusercontent.com/stavrosgiannis/schulprojekt/master/config.ini"
    Dim urlexe As String = "https://github.com/stavrosgiannis/schulprojekt/raw/master/update.exe"
    Public Sub queryNewVersion()


        If My.Computer.FileSystem.FileExists(_inipath) = False Then
            My.Computer.Network.DownloadFile(urlini, _inipath)
        Else
            My.Computer.FileSystem.DeleteFile(_inipath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            My.Computer.Network.DownloadFile(urlini, _inipath)
        End If

        ReadUpdateFiles()

    End Sub

    Public Sub downloadUpdate()
        urlexe = IniReadValue("update", "urlexe")

        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\update.exe") Then
            My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\update.exe", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            WC.DownloadFileAsync(New Uri(urlexe), Application.StartupPath & "\update.exe")
        Else
            WC.DownloadFileAsync(New Uri(urlexe), Application.StartupPath & "\update.exe")
        End If



    End Sub

    Public Sub extractbatchfile()

        My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/stavrosgiannis/schulprojekt/master/update.bat", Application.StartupPath & "\update.bat")
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
        extractbatchfile()
    End Sub
#Region "Active Directory"
    Private Function GetActiveDirUserDetails(ByVal username As String) As String
        Dim dirEntry As System.DirectoryServices.DirectoryEntry
        Dim dirSearcher As System.DirectoryServices.DirectorySearcher
        Try
            dirEntry = New System.DirectoryServices.DirectoryEntry("LDAP://172.17.25.10:389/DC=bsidomain,DC=com")
            dirSearcher = New System.DirectoryServices.DirectorySearcher(dirEntry)
            dirSearcher.Filter = "(samAccountName=" & username & ")"
            dirSearcher.PropertiesToLoad.Add("GivenName")
            dirSearcher.PropertiesToLoad.Add("sn")
            Dim sr As DirectoryServices.SearchResult = dirSearcher.FindOne()
            If sr Is Nothing Then
                Return False
            End If

            Dim de As System.DirectoryServices.DirectoryEntry = sr.GetDirectoryEntry()

            Dim ObjFirstName As String = ""
            Dim ObjLastName As String = String.Empty

            Try
                ObjFirstName = de.Properties("GivenName").Value.ToString()
                ObjLastName = de.Properties("sn").Value.ToString()

            Catch ex As Exception
                ObjFirstName = de.Properties("DisplayName").Value.ToString()
            End Try

            MsgBox(ObjFirstName + ObjLastName)

        Catch ex As Exception ' return false if exception occurs 
            Return ex.Message
        End Try

    End Function
#End Region

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub
End Class
