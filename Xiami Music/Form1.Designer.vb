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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btn_search = New System.Windows.Forms.Button()
        Me.txt_search = New System.Windows.Forms.TextBox()
        Me.lvw_results = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvw_details = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_streamit = New System.Windows.Forms.Button()
        Me.AxWindowsMediaPlayer1 = New AxWMPLib.AxWindowsMediaPlayer()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.btn_download = New System.Windows.Forms.Button()
        Me.lbl_status = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_search
        '
        Me.btn_search.Location = New System.Drawing.Point(167, 21)
        Me.btn_search.Name = "btn_search"
        Me.btn_search.Size = New System.Drawing.Size(75, 23)
        Me.btn_search.TabIndex = 1
        Me.btn_search.Text = "search"
        Me.btn_search.UseVisualStyleBackColor = True
        '
        'txt_search
        '
        Me.txt_search.Location = New System.Drawing.Point(27, 24)
        Me.txt_search.Name = "txt_search"
        Me.txt_search.Size = New System.Drawing.Size(134, 20)
        Me.txt_search.TabIndex = 0
        '
        'lvw_results
        '
        Me.lvw_results.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvw_results.FullRowSelect = True
        Me.lvw_results.HideSelection = False
        Me.lvw_results.Location = New System.Drawing.Point(27, 50)
        Me.lvw_results.MultiSelect = False
        Me.lvw_results.Name = "lvw_results"
        Me.lvw_results.Size = New System.Drawing.Size(388, 400)
        Me.lvw_results.TabIndex = 2
        Me.lvw_results.UseCompatibleStateImageBehavior = False
        Me.lvw_results.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Song"
        Me.ColumnHeader1.Width = 151
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Artist"
        Me.ColumnHeader2.Width = 123
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Album"
        Me.ColumnHeader3.Width = 105
        '
        'lvw_details
        '
        Me.lvw_details.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lvw_details.FullRowSelect = True
        Me.lvw_details.HideSelection = False
        Me.lvw_details.Location = New System.Drawing.Point(421, 50)
        Me.lvw_details.MultiSelect = False
        Me.lvw_details.Name = "lvw_details"
        Me.lvw_details.Size = New System.Drawing.Size(220, 222)
        Me.lvw_details.TabIndex = 3
        Me.lvw_details.UseCompatibleStateImageBehavior = False
        Me.lvw_details.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Format"
        Me.ColumnHeader4.Width = 48
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Bitrate"
        Me.ColumnHeader5.Width = 48
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Duration"
        Me.ColumnHeader6.Width = 57
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Size"
        Me.ColumnHeader7.Width = 61
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_streamit)
        Me.GroupBox1.Controls.Add(Me.AxWindowsMediaPlayer1)
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Controls.Add(Me.btn_download)
        Me.GroupBox1.Location = New System.Drawing.Point(421, 278)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(220, 172)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Downloadbox"
        Me.GroupBox1.Visible = False
        '
        'btn_streamit
        '
        Me.btn_streamit.Location = New System.Drawing.Point(139, 93)
        Me.btn_streamit.Name = "btn_streamit"
        Me.btn_streamit.Size = New System.Drawing.Size(75, 23)
        Me.btn_streamit.TabIndex = 3
        Me.btn_streamit.Text = "Stream it!"
        Me.btn_streamit.UseVisualStyleBackColor = True
        '
        'AxWindowsMediaPlayer1
        '
        Me.AxWindowsMediaPlayer1.Enabled = True
        Me.AxWindowsMediaPlayer1.Location = New System.Drawing.Point(6, 121)
        Me.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1"
        Me.AxWindowsMediaPlayer1.OcxState = CType(resources.GetObject("AxWindowsMediaPlayer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWindowsMediaPlayer1.Size = New System.Drawing.Size(208, 45)
        Me.AxWindowsMediaPlayer1.TabIndex = 2
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(6, 71)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(208, 16)
        Me.ProgressBar1.TabIndex = 1
        '
        'btn_download
        '
        Me.btn_download.Location = New System.Drawing.Point(6, 19)
        Me.btn_download.Name = "btn_download"
        Me.btn_download.Size = New System.Drawing.Size(208, 46)
        Me.btn_download.TabIndex = 0
        Me.btn_download.Text = "Download"
        Me.btn_download.UseVisualStyleBackColor = True
        '
        'lbl_status
        '
        Me.lbl_status.AutoSize = True
        Me.lbl_status.Location = New System.Drawing.Point(248, 27)
        Me.lbl_status.Name = "lbl_status"
        Me.lbl_status.Size = New System.Drawing.Size(32, 13)
        Me.lbl_status.TabIndex = 5
        Me.lbl_status.Text = "idle..."
        '
        'Form1
        '
        Me.AcceptButton = Me.btn_search
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 464)
        Me.Controls.Add(Me.lbl_status)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lvw_details)
        Me.Controls.Add(Me.lvw_results)
        Me.Controls.Add(Me.txt_search)
        Me.Controls.Add(Me.btn_search)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Xiami Music"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_search As Button
    Friend WithEvents txt_search As TextBox
    Friend WithEvents lvw_results As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents lvw_details As ListView
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btn_download As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents btn_streamit As Button
    Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents lbl_status As Label
End Class
