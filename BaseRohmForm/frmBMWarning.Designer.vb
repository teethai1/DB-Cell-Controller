<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBMWarning
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
        Me.components = New System.ComponentModel.Container()
        Me.lbMessege = New System.Windows.Forms.Label()
        Me.btYes = New System.Windows.Forms.Button()
        Me.btNo = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'lbMessege
        '
        Me.lbMessege.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbMessege.Location = New System.Drawing.Point(12, 9)
        Me.lbMessege.Name = "lbMessege"
        Me.lbMessege.Size = New System.Drawing.Size(582, 158)
        Me.lbMessege.TabIndex = 0
        Me.lbMessege.Text = "Abnormal Lot End  : " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "โหมดนี้จะให้ใช้ต่อเมื่อเครื่องมีปัญหาและต้องการย้ายงานไป Di" & _
            "e bond เครื่องอื่น,เครื่อง Hang " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ไม่มีผลต่อระบบ APCS  เมื่อ Input Lot นี้ใหม่อี" & _
            "กครั้ง"
        Me.lbMessege.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btYes
        '
        Me.btYes.Enabled = False
        Me.btYes.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btYes.Location = New System.Drawing.Point(12, 203)
        Me.btYes.Name = "btYes"
        Me.btYes.Size = New System.Drawing.Size(363, 63)
        Me.btYes.TabIndex = 1
        Me.btYes.Text = "กรุณารอ. . .(20)"
        Me.btYes.UseVisualStyleBackColor = True
        '
        'btNo
        '
        Me.btNo.BackColor = System.Drawing.Color.Red
        Me.btNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btNo.Location = New System.Drawing.Point(444, 203)
        Me.btNo.Name = "btNo"
        Me.btNo.Size = New System.Drawing.Size(114, 63)
        Me.btNo.TabIndex = 1
        Me.btNo.Text = "No"
        Me.btNo.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'frmBMWarning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 279)
        Me.Controls.Add(Me.btNo)
        Me.Controls.Add(Me.btYes)
        Me.Controls.Add(Me.lbMessege)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBMWarning"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmBMWarning"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbMessege As System.Windows.Forms.Label
    Friend WithEvents btYes As System.Windows.Forms.Button
    Friend WithEvents btNo As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
