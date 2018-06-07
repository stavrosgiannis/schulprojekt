<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TokenInfoText = New System.Windows.Forms.TextBox()
        Me.CallGraphButton = New System.Windows.Forms.Button()
        Me.ResultText = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(439, 386)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "write"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(573, 386)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "read"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 434)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(800, 15)
        Me.ProgressBar1.TabIndex = 2
        Me.ProgressBar1.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(507, 415)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(281, 13)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "0%"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox1.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(352, 12)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TokenInfoText
        '
        Me.TokenInfoText.Location = New System.Drawing.Point(12, 58)
        Me.TokenInfoText.Multiline = True
        Me.TokenInfoText.Name = "TokenInfoText"
        Me.TokenInfoText.Size = New System.Drawing.Size(762, 90)
        Me.TokenInfoText.TabIndex = 5
        '
        'CallGraphButton
        '
        Me.CallGraphButton.Location = New System.Drawing.Point(507, 12)
        Me.CallGraphButton.Name = "CallGraphButton"
        Me.CallGraphButton.Size = New System.Drawing.Size(107, 23)
        Me.CallGraphButton.TabIndex = 6
        Me.CallGraphButton.Text = "Button4"
        Me.CallGraphButton.UseVisualStyleBackColor = True
        '
        'ResultText
        '
        Me.ResultText.Location = New System.Drawing.Point(12, 190)
        Me.ResultText.Multiline = True
        Me.ResultText.Name = "ResultText"
        Me.ResultText.Size = New System.Drawing.Size(762, 167)
        Me.ResultText.TabIndex = 7
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ResultText)
        Me.Controls.Add(Me.CallGraphButton)
        Me.Controls.Add(Me.TokenInfoText)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents TokenInfoText As TextBox
    Friend WithEvents CallGraphButton As Button
    Friend WithEvents ResultText As TextBox
End Class
