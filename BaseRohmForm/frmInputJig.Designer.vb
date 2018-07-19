<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInputJig
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
        Me.tbRevQr = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lbCaption = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tbRevQr
        '
        Me.tbRevQr.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbRevQr.Location = New System.Drawing.Point(-17, 12)
        Me.tbRevQr.Name = "tbRevQr"
        Me.tbRevQr.Size = New System.Drawing.Size(14, 38)
        Me.tbRevQr.TabIndex = 6
        Me.tbRevQr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(48, 125)
        Me.ProgressBar1.Maximum = 10
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(448, 36)
        Me.ProgressBar1.TabIndex = 5
        '
        'lbCaption
        '
        Me.lbCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbCaption.Location = New System.Drawing.Point(42, 41)
        Me.lbCaption.Name = "lbCaption"
        Me.lbCaption.Size = New System.Drawing.Size(448, 33)
        Me.lbCaption.TabIndex = 4
        Me.lbCaption.Text = "Input Rubber Collet"
        Me.lbCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmInputJig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(543, 214)
        Me.Controls.Add(Me.tbRevQr)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lbCaption)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInputJig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmInputJig"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbRevQr As System.Windows.Forms.TextBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lbCaption As System.Windows.Forms.Label
End Class
