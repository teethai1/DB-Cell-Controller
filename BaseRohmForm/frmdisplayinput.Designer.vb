<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmdisplayinput
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
        Me.lbcaption = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.tbQR = New System.Windows.Forms.TextBox()
        Me.tbOP = New System.Windows.Forms.TextBox()
        Me.tbOPCheck = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbnNormalStartTDC = New System.Windows.Forms.RadioButton()
        Me.rbnSeparateEndStartTDC = New System.Windows.Forms.RadioButton()
        Me.rbnSeparateStartTDC = New System.Windows.Forms.RadioButton()
        Me.rdNormal = New System.Windows.Forms.RadioButton()
        Me.rdTorinokoshi = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.panelTorinokoshi = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.panelTorinokoshi.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbcaption
        '
        Me.lbcaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbcaption.Location = New System.Drawing.Point(12, 55)
        Me.lbcaption.Name = "lbcaption"
        Me.lbcaption.Size = New System.Drawing.Size(494, 115)
        Me.lbcaption.TabIndex = 99
        Me.lbcaption.Text = "Label1"
        Me.lbcaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(16, 187)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(489, 36)
        Me.ProgressBar1.TabIndex = 1
        '
        'tbQR
        '
        Me.tbQR.Location = New System.Drawing.Point(-62, 268)
        Me.tbQR.Name = "tbQR"
        Me.tbQR.Size = New System.Drawing.Size(58, 20)
        Me.tbQR.TabIndex = 0
        '
        'tbOP
        '
        Me.tbOP.Location = New System.Drawing.Point(-62, 216)
        Me.tbOP.Name = "tbOP"
        Me.tbOP.Size = New System.Drawing.Size(58, 20)
        Me.tbOP.TabIndex = 2
        '
        'tbOPCheck
        '
        Me.tbOPCheck.Location = New System.Drawing.Point(-28, 242)
        Me.tbOPCheck.Name = "tbOPCheck"
        Me.tbOPCheck.Size = New System.Drawing.Size(24, 20)
        Me.tbOPCheck.TabIndex = 100
        Me.tbOPCheck.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.rbnNormalStartTDC)
        Me.Panel1.Controls.Add(Me.rbnSeparateEndStartTDC)
        Me.Panel1.Controls.Add(Me.rbnSeparateStartTDC)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(518, 42)
        Me.Panel1.TabIndex = 101
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "TDCStartMode"
        '
        'rbnNormalStartTDC
        '
        Me.rbnNormalStartTDC.AutoSize = True
        Me.rbnNormalStartTDC.Checked = True
        Me.rbnNormalStartTDC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbnNormalStartTDC.Location = New System.Drawing.Point(6, 21)
        Me.rbnNormalStartTDC.Name = "rbnNormalStartTDC"
        Me.rbnNormalStartTDC.Size = New System.Drawing.Size(68, 17)
        Me.rbnNormalStartTDC.TabIndex = 0
        Me.rbnNormalStartTDC.TabStop = True
        Me.rbnNormalStartTDC.Text = "แบบปกติ"
        Me.rbnNormalStartTDC.UseVisualStyleBackColor = True
        '
        'rbnSeparateEndStartTDC
        '
        Me.rbnSeparateEndStartTDC.AutoSize = True
        Me.rbnSeparateEndStartTDC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbnSeparateEndStartTDC.Location = New System.Drawing.Point(187, 21)
        Me.rbnSeparateEndStartTDC.Name = "rbnSeparateEndStartTDC"
        Me.rbnSeparateEndStartTDC.Size = New System.Drawing.Size(108, 17)
        Me.rbnSeparateEndStartTDC.TabIndex = 0
        Me.rbnSeparateEndStartTDC.TabStop = True
        Me.rbnSeparateEndStartTDC.Text = "สิ้นสุดการแบ่ง Lot"
        Me.rbnSeparateEndStartTDC.UseVisualStyleBackColor = True
        '
        'rbnSeparateStartTDC
        '
        Me.rbnSeparateStartTDC.AutoSize = True
        Me.rbnSeparateStartTDC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbnSeparateStartTDC.Location = New System.Drawing.Point(88, 21)
        Me.rbnSeparateStartTDC.Name = "rbnSeparateStartTDC"
        Me.rbnSeparateStartTDC.Size = New System.Drawing.Size(85, 17)
        Me.rbnSeparateStartTDC.TabIndex = 0
        Me.rbnSeparateStartTDC.TabStop = True
        Me.rbnSeparateStartTDC.Text = "แบบแบ่ง Lot"
        Me.rbnSeparateStartTDC.UseVisualStyleBackColor = True
        '
        'rdNormal
        '
        Me.rdNormal.AutoSize = True
        Me.rdNormal.Checked = True
        Me.rdNormal.Location = New System.Drawing.Point(17, 31)
        Me.rdNormal.Name = "rdNormal"
        Me.rdNormal.Size = New System.Drawing.Size(58, 17)
        Me.rdNormal.TabIndex = 103
        Me.rdNormal.TabStop = True
        Me.rdNormal.Text = "Normal"
        Me.rdNormal.UseVisualStyleBackColor = True
        '
        'rdTorinokoshi
        '
        Me.rdTorinokoshi.AutoSize = True
        Me.rdTorinokoshi.Location = New System.Drawing.Point(143, 31)
        Me.rdTorinokoshi.Name = "rdTorinokoshi"
        Me.rdTorinokoshi.Size = New System.Drawing.Size(80, 17)
        Me.rdTorinokoshi.TabIndex = 103
        Me.rdTorinokoshi.Text = "Torinokoshi"
        Me.rdTorinokoshi.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Running Mode"
        '
        'panelTorinokoshi
        '
        Me.panelTorinokoshi.Controls.Add(Me.Label2)
        Me.panelTorinokoshi.Controls.Add(Me.rdTorinokoshi)
        Me.panelTorinokoshi.Controls.Add(Me.rdNormal)
        Me.panelTorinokoshi.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTorinokoshi.Location = New System.Drawing.Point(0, 42)
        Me.panelTorinokoshi.Name = "panelTorinokoshi"
        Me.panelTorinokoshi.Size = New System.Drawing.Size(518, 56)
        Me.panelTorinokoshi.TabIndex = 102
        Me.panelTorinokoshi.Visible = False
        '
        'frmdisplayinput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 240)
        Me.Controls.Add(Me.panelTorinokoshi)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.tbOPCheck)
        Me.Controls.Add(Me.tbOP)
        Me.Controls.Add(Me.tbQR)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lbcaption)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmdisplayinput"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Display"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.panelTorinokoshi.ResumeLayout(False)
        Me.panelTorinokoshi.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbcaption As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents tbQR As System.Windows.Forms.TextBox
    Friend WithEvents tbOP As System.Windows.Forms.TextBox
    Friend WithEvents tbOPCheck As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rbnNormalStartTDC As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSeparateEndStartTDC As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSeparateStartTDC As System.Windows.Forms.RadioButton
    Friend WithEvents rdNormal As System.Windows.Forms.RadioButton
    Friend WithEvents rdTorinokoshi As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents panelTorinokoshi As System.Windows.Forms.Panel
End Class
