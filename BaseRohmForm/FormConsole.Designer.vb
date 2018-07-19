<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConsole
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtCommand = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControlMessageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SECSMessageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCommand
        '
        Me.txtCommand.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCommand.BackColor = System.Drawing.Color.Black
        Me.txtCommand.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCommand.ForeColor = System.Drawing.Color.White
        Me.txtCommand.Location = New System.Drawing.Point(12, 27)
        Me.txtCommand.Multiline = True
        Me.txtCommand.Name = "txtCommand"
        Me.txtCommand.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCommand.Size = New System.Drawing.Size(895, 226)
        Me.txtCommand.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(907, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'FilterToolStripMenuItem
        '
        Me.FilterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ControlMessageToolStripMenuItem, Me.SECSMessageToolStripMenuItem, Me.AllToolStripMenuItem})
        Me.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem"
        Me.FilterToolStripMenuItem.Size = New System.Drawing.Size(139, 20)
        Me.FilterToolStripMenuItem.Text = "Filter by Message Type"
        '
        'ControlMessageToolStripMenuItem
        '
        Me.ControlMessageToolStripMenuItem.Name = "ControlMessageToolStripMenuItem"
        Me.ControlMessageToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.ControlMessageToolStripMenuItem.Text = "Control Message"
        '
        'SECSMessageToolStripMenuItem
        '
        Me.SECSMessageToolStripMenuItem.Name = "SECSMessageToolStripMenuItem"
        Me.SECSMessageToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.SECSMessageToolStripMenuItem.Text = "SECS Message"
        '
        'AllToolStripMenuItem
        '
        Me.AllToolStripMenuItem.Name = "AllToolStripMenuItem"
        Me.AllToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.AllToolStripMenuItem.Text = "All"
        '
        'FormConsole
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(907, 265)
        Me.Controls.Add(Me.txtCommand)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormConsole"
        Me.Text = "Console"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCommand As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FilterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ControlMessageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SECSMessageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
