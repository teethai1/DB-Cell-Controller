<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTorinokoshi
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.PackageDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TorinokoshiPackageBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DBxDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DBxDataSet = New CellController.DBxDataSet()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TorinokoshiPackageBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DBxDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DBxDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PackageDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.TorinokoshiPackageBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(12, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(245, 353)
        Me.DataGridView1.TabIndex = 1
        '
        'PackageDataGridViewTextBoxColumn
        '
        Me.PackageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.PackageDataGridViewTextBoxColumn.DataPropertyName = "Package"
        Me.PackageDataGridViewTextBoxColumn.HeaderText = "Package"
        Me.PackageDataGridViewTextBoxColumn.Name = "PackageDataGridViewTextBoxColumn"
        Me.PackageDataGridViewTextBoxColumn.Width = 75
        '
        'TorinokoshiPackageBindingSource
        '
        Me.TorinokoshiPackageBindingSource.DataMember = "TorinokoshiPackage"
        Me.TorinokoshiPackageBindingSource.DataSource = Me.DBxDataSetBindingSource
        '
        'DBxDataSetBindingSource
        '
        Me.DBxDataSetBindingSource.DataSource = Me.DBxDataSet
        Me.DBxDataSetBindingSource.Position = 0
        '
        'DBxDataSet
        '
        Me.DBxDataSet.DataSetName = "DBxDataSet"
        Me.DBxDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(282, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(317, 25)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "กรุณาใส่ชื่อ Package ที่รัน Torinokoshi"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(76, 49)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 47)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Normal"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(303, 127)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(266, 231)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ควรกดก่อนจบ Lot"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(76, 133)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(103, 47)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Torinokoshi"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmTorinokoshi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 394)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTorinokoshi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmTorinokoshi"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TorinokoshiPackageBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DBxDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DBxDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents PackageDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TorinokoshiPackageBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DBxDataSetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DBxDataSet As CellController.DBxDataSet
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
