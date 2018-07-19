<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductionTable
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProductionTable))
        Me.tbAlarmCellCon = New System.Windows.Forms.TabPage()
        Me.lblAlarm = New System.Windows.Forms.Label()
        Me.btnCellConAlmRst = New System.Windows.Forms.Button()
        Me.dgvAlarmCellCon = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbAlarm = New System.Windows.Forms.TabPage()
        Me.dgvAlarm = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlarmMessage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbDetail = New System.Windows.Forms.TabPage()
        Me.dgvProductionDetail = New System.Windows.Forms.DataGridView()
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Action = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbProductionPage = New System.Windows.Forms.TabPage()
        Me.dgvProductionInfo1 = New System.Windows.Forms.DataGridView()
        Me.LDCarrierID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ULDCarrierID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LotID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Package = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Device = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutPutPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EndTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbPageMain = New System.Windows.Forms.TabControl()
        Me.tbOther = New System.Windows.Forms.TabPage()
        Me.pbxOPID = New System.Windows.Forms.PictureBox()
        Me.btnOPID = New System.Windows.Forms.Button()
        Me.pbxInput = New System.Windows.Forms.PictureBox()
        Me.pbxQR = New System.Windows.Forms.PictureBox()
        Me.btnInputQty = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnWorkSlip = New System.Windows.Forms.Button()
        Me.tbxKey = New System.Windows.Forms.TextBox()
        Me.pbxInputBorder = New System.Windows.Forms.PictureBox()
        Me.tbxQR_OPID = New System.Windows.Forms.TextBox()
        Me.pbxWorkSlipBorder = New System.Windows.Forms.PictureBox()
        Me.pbxOPIDBorder = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SiableToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FixoolTStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SizableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FixedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MinimizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tbAlarmCellCon.SuspendLayout()
        CType(Me.dgvAlarmCellCon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbAlarm.SuspendLayout()
        CType(Me.dgvAlarm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbDetail.SuspendLayout()
        CType(Me.dgvProductionDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbProductionPage.SuspendLayout()
        CType(Me.dgvProductionInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbPageMain.SuspendLayout()
        Me.tbOther.SuspendLayout()
        CType(Me.pbxOPID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxInput, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxQR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxInputBorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxWorkSlipBorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxOPIDBorder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbAlarmCellCon
        '
        Me.tbAlarmCellCon.Controls.Add(Me.lblAlarm)
        Me.tbAlarmCellCon.Controls.Add(Me.btnCellConAlmRst)
        Me.tbAlarmCellCon.Controls.Add(Me.dgvAlarmCellCon)
        Me.tbAlarmCellCon.Location = New System.Drawing.Point(4, 25)
        Me.tbAlarmCellCon.Name = "tbAlarmCellCon"
        Me.tbAlarmCellCon.Size = New System.Drawing.Size(756, 163)
        Me.tbAlarmCellCon.TabIndex = 4
        Me.tbAlarmCellCon.Text = "AlarmCellCon"
        Me.tbAlarmCellCon.UseVisualStyleBackColor = True
        '
        'lblAlarm
        '
        Me.lblAlarm.AutoSize = True
        Me.lblAlarm.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlarm.ForeColor = System.Drawing.Color.Red
        Me.lblAlarm.Location = New System.Drawing.Point(130, 20)
        Me.lblAlarm.Name = "lblAlarm"
        Me.lblAlarm.Size = New System.Drawing.Size(154, 24)
        Me.lblAlarm.TabIndex = 45
        Me.lblAlarm.Text = "Alarm Message"
        '
        'btnCellConAlmRst
        '
        Me.btnCellConAlmRst.Location = New System.Drawing.Point(6, 7)
        Me.btnCellConAlmRst.Name = "btnCellConAlmRst"
        Me.btnCellConAlmRst.Size = New System.Drawing.Size(99, 47)
        Me.btnCellConAlmRst.TabIndex = 44
        Me.btnCellConAlmRst.Text = "Alarm Reset"
        Me.btnCellConAlmRst.UseVisualStyleBackColor = True
        '
        'dgvAlarmCellCon
        '
        Me.dgvAlarmCellCon.AllowUserToDeleteRows = False
        Me.dgvAlarmCellCon.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAlarmCellCon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAlarmCellCon.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13})
        Me.dgvAlarmCellCon.Location = New System.Drawing.Point(3, 61)
        Me.dgvAlarmCellCon.Name = "dgvAlarmCellCon"
        Me.dgvAlarmCellCon.ReadOnly = True
        Me.dgvAlarmCellCon.Size = New System.Drawing.Size(750, 106)
        Me.dgvAlarmCellCon.TabIndex = 39
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn5.HeaderText = "Time"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 64
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn9.HeaderText = "Location"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 84
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn11.HeaderText = "Condition"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 89
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn12.HeaderText = "AlarmID"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 81
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn13.HeaderText = "Alarm Message"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'tbAlarm
        '
        Me.tbAlarm.Controls.Add(Me.dgvAlarm)
        Me.tbAlarm.Location = New System.Drawing.Point(4, 25)
        Me.tbAlarm.Name = "tbAlarm"
        Me.tbAlarm.Padding = New System.Windows.Forms.Padding(3)
        Me.tbAlarm.Size = New System.Drawing.Size(756, 163)
        Me.tbAlarm.TabIndex = 1
        Me.tbAlarm.Text = "Alarm"
        Me.tbAlarm.UseVisualStyleBackColor = True
        '
        'dgvAlarm
        '
        Me.dgvAlarm.AllowUserToDeleteRows = False
        Me.dgvAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAlarm.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn7, Me.AlarmMessage})
        Me.dgvAlarm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAlarm.Location = New System.Drawing.Point(3, 3)
        Me.dgvAlarm.Name = "dgvAlarm"
        Me.dgvAlarm.ReadOnly = True
        Me.dgvAlarm.Size = New System.Drawing.Size(750, 157)
        Me.dgvAlarm.TabIndex = 38
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn3.HeaderText = "Time"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 64
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn6.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 70
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn4.HeaderText = "AlarmID"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 81
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn7.HeaderText = "AlarmType"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'AlarmMessage
        '
        Me.AlarmMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.AlarmMessage.HeaderText = "Alarm Message"
        Me.AlarmMessage.Name = "AlarmMessage"
        Me.AlarmMessage.ReadOnly = True
        '
        'tbDetail
        '
        Me.tbDetail.Controls.Add(Me.dgvProductionDetail)
        Me.tbDetail.Location = New System.Drawing.Point(4, 25)
        Me.tbDetail.Name = "tbDetail"
        Me.tbDetail.Size = New System.Drawing.Size(756, 163)
        Me.tbDetail.TabIndex = 3
        Me.tbDetail.Text = "Production Details"
        Me.tbDetail.UseVisualStyleBackColor = True
        '
        'dgvProductionDetail
        '
        Me.dgvProductionDetail.AllowUserToDeleteRows = False
        Me.dgvProductionDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductionDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Time, Me.ItemID, Me.Type, Me.Action})
        Me.dgvProductionDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProductionDetail.Location = New System.Drawing.Point(0, 0)
        Me.dgvProductionDetail.Name = "dgvProductionDetail"
        Me.dgvProductionDetail.ReadOnly = True
        Me.dgvProductionDetail.Size = New System.Drawing.Size(756, 163)
        Me.dgvProductionDetail.TabIndex = 0
        '
        'Time
        '
        Me.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Time.HeaderText = "Time"
        Me.Time.Name = "Time"
        Me.Time.ReadOnly = True
        Me.Time.Width = 64
        '
        'ItemID
        '
        Me.ItemID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ItemID.HeaderText = "ItemID"
        Me.ItemID.Name = "ItemID"
        Me.ItemID.ReadOnly = True
        Me.ItemID.Width = 71
        '
        'Type
        '
        Me.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.ReadOnly = True
        Me.Type.Width = 65
        '
        'Action
        '
        Me.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Action.HeaderText = "Action"
        Me.Action.Name = "Action"
        Me.Action.ReadOnly = True
        '
        'tbProductionPage
        '
        Me.tbProductionPage.Controls.Add(Me.dgvProductionInfo1)
        Me.tbProductionPage.Location = New System.Drawing.Point(4, 25)
        Me.tbProductionPage.Name = "tbProductionPage"
        Me.tbProductionPage.Padding = New System.Windows.Forms.Padding(3)
        Me.tbProductionPage.Size = New System.Drawing.Size(756, 163)
        Me.tbProductionPage.TabIndex = 0
        Me.tbProductionPage.Text = "Production Info."
        Me.tbProductionPage.UseVisualStyleBackColor = True
        '
        'dgvProductionInfo1
        '
        Me.dgvProductionInfo1.AllowUserToDeleteRows = False
        Me.dgvProductionInfo1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvProductionInfo1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductionInfo1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LDCarrierID, Me.ULDCarrierID, Me.LotID, Me.Package, Me.Device, Me.OutPutPcs, Me.StartTime, Me.EndTime, Me.Remark})
        Me.dgvProductionInfo1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProductionInfo1.Location = New System.Drawing.Point(3, 3)
        Me.dgvProductionInfo1.Name = "dgvProductionInfo1"
        Me.dgvProductionInfo1.ReadOnly = True
        Me.dgvProductionInfo1.Size = New System.Drawing.Size(750, 157)
        Me.dgvProductionInfo1.TabIndex = 37
        '
        'LDCarrierID
        '
        Me.LDCarrierID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.LDCarrierID.HeaderText = "LDCarrierID"
        Me.LDCarrierID.Name = "LDCarrierID"
        Me.LDCarrierID.ReadOnly = True
        Me.LDCarrierID.Width = 103
        '
        'ULDCarrierID
        '
        Me.ULDCarrierID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ULDCarrierID.HeaderText = "ULDCarrierID"
        Me.ULDCarrierID.Name = "ULDCarrierID"
        Me.ULDCarrierID.ReadOnly = True
        Me.ULDCarrierID.Width = 113
        '
        'LotID
        '
        Me.LotID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.LotID.HeaderText = "LotID"
        Me.LotID.Name = "LotID"
        Me.LotID.ReadOnly = True
        Me.LotID.Width = 64
        '
        'Package
        '
        Me.Package.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Package.HeaderText = "Package"
        Me.Package.Name = "Package"
        Me.Package.ReadOnly = True
        Me.Package.Width = 88
        '
        'Device
        '
        Me.Device.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Device.HeaderText = "Device"
        Me.Device.Name = "Device"
        Me.Device.ReadOnly = True
        Me.Device.Width = 76
        '
        'OutPutPcs
        '
        Me.OutPutPcs.HeaderText = "OutPutPcs"
        Me.OutPutPcs.Name = "OutPutPcs"
        Me.OutPutPcs.ReadOnly = True
        Me.OutPutPcs.Width = 95
        '
        'StartTime
        '
        Me.StartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.StartTime.HeaderText = "StartTime"
        Me.StartTime.Name = "StartTime"
        Me.StartTime.ReadOnly = True
        Me.StartTime.Width = 91
        '
        'EndTime
        '
        Me.EndTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.EndTime.HeaderText = "EndTime"
        Me.EndTime.Name = "EndTime"
        Me.EndTime.ReadOnly = True
        Me.EndTime.Width = 88
        '
        'Remark
        '
        Me.Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Remark.HeaderText = "Remark"
        Me.Remark.Name = "Remark"
        Me.Remark.ReadOnly = True
        '
        'tbPageMain
        '
        Me.tbPageMain.Controls.Add(Me.tbOther)
        Me.tbPageMain.Controls.Add(Me.tbProductionPage)
        Me.tbPageMain.Controls.Add(Me.tbDetail)
        Me.tbPageMain.Controls.Add(Me.tbAlarm)
        Me.tbPageMain.Controls.Add(Me.tbAlarmCellCon)
        Me.tbPageMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbPageMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbPageMain.Location = New System.Drawing.Point(0, 0)
        Me.tbPageMain.Name = "tbPageMain"
        Me.tbPageMain.SelectedIndex = 0
        Me.tbPageMain.Size = New System.Drawing.Size(764, 192)
        Me.tbPageMain.TabIndex = 39
        '
        'tbOther
        '
        Me.tbOther.Controls.Add(Me.pbxOPID)
        Me.tbOther.Controls.Add(Me.btnOPID)
        Me.tbOther.Controls.Add(Me.pbxInput)
        Me.tbOther.Controls.Add(Me.pbxQR)
        Me.tbOther.Controls.Add(Me.btnInputQty)
        Me.tbOther.Controls.Add(Me.TextBox1)
        Me.tbOther.Controls.Add(Me.btnWorkSlip)
        Me.tbOther.Controls.Add(Me.tbxKey)
        Me.tbOther.Controls.Add(Me.pbxInputBorder)
        Me.tbOther.Controls.Add(Me.tbxQR_OPID)
        Me.tbOther.Controls.Add(Me.pbxWorkSlipBorder)
        Me.tbOther.Controls.Add(Me.pbxOPIDBorder)
        Me.tbOther.Controls.Add(Me.MenuStrip1)
        Me.tbOther.Location = New System.Drawing.Point(4, 25)
        Me.tbOther.Name = "tbOther"
        Me.tbOther.Padding = New System.Windows.Forms.Padding(3)
        Me.tbOther.Size = New System.Drawing.Size(756, 163)
        Me.tbOther.TabIndex = 5
        Me.tbOther.UseVisualStyleBackColor = True
        '
        'pbxOPID
        '
        Me.pbxOPID.BackColor = System.Drawing.SystemColors.Control
        Me.pbxOPID.BackgroundImage = Global.CellController.My.Resources.Resources.UserBlank
        Me.pbxOPID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbxOPID.Location = New System.Drawing.Point(34, 58)
        Me.pbxOPID.Name = "pbxOPID"
        Me.pbxOPID.Size = New System.Drawing.Size(70, 50)
        Me.pbxOPID.TabIndex = 51
        Me.pbxOPID.TabStop = False
        '
        'btnOPID
        '
        Me.btnOPID.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnOPID.ForeColor = System.Drawing.Color.Black
        Me.btnOPID.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnOPID.ImageKey = "(none)"
        Me.btnOPID.Location = New System.Drawing.Point(27, 50)
        Me.btnOPID.Name = "btnOPID"
        Me.btnOPID.Size = New System.Drawing.Size(85, 80)
        Me.btnOPID.TabIndex = 50
        Me.btnOPID.Text = "OPID"
        Me.btnOPID.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnOPID.UseVisualStyleBackColor = False
        '
        'pbxInput
        '
        Me.pbxInput.BackColor = System.Drawing.SystemColors.Control
        Me.pbxInput.BackgroundImage = CType(resources.GetObject("pbxInput.BackgroundImage"), System.Drawing.Image)
        Me.pbxInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbxInput.Enabled = False
        Me.pbxInput.Location = New System.Drawing.Point(238, 57)
        Me.pbxInput.Name = "pbxInput"
        Me.pbxInput.Size = New System.Drawing.Size(70, 50)
        Me.pbxInput.TabIndex = 48
        Me.pbxInput.TabStop = False
        '
        'pbxQR
        '
        Me.pbxQR.BackColor = System.Drawing.SystemColors.Control
        Me.pbxQR.BackgroundImage = CType(resources.GetObject("pbxQR.BackgroundImage"), System.Drawing.Image)
        Me.pbxQR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbxQR.Location = New System.Drawing.Point(135, 58)
        Me.pbxQR.Name = "pbxQR"
        Me.pbxQR.Size = New System.Drawing.Size(70, 50)
        Me.pbxQR.TabIndex = 48
        Me.pbxQR.TabStop = False
        '
        'btnInputQty
        '
        Me.btnInputQty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnInputQty.Enabled = False
        Me.btnInputQty.ForeColor = System.Drawing.Color.Black
        Me.btnInputQty.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnInputQty.ImageKey = "(none)"
        Me.btnInputQty.Location = New System.Drawing.Point(231, 50)
        Me.btnInputQty.Name = "btnInputQty"
        Me.btnInputQty.Size = New System.Drawing.Size(85, 80)
        Me.btnInputQty.TabIndex = 47
        Me.btnInputQty.Text = "Input Qty"
        Me.btnInputQty.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnInputQty.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 3.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.TextBox1.Location = New System.Drawing.Point(240, 108)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(70, 6)
        Me.TextBox1.TabIndex = 49
        '
        'btnWorkSlip
        '
        Me.btnWorkSlip.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnWorkSlip.ForeColor = System.Drawing.Color.Black
        Me.btnWorkSlip.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnWorkSlip.ImageKey = "(none)"
        Me.btnWorkSlip.Location = New System.Drawing.Point(128, 51)
        Me.btnWorkSlip.Name = "btnWorkSlip"
        Me.btnWorkSlip.Size = New System.Drawing.Size(85, 80)
        Me.btnWorkSlip.TabIndex = 47
        Me.btnWorkSlip.Text = "WorkSlip"
        Me.btnWorkSlip.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnWorkSlip.UseVisualStyleBackColor = False
        '
        'tbxKey
        '
        Me.tbxKey.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tbxKey.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 3.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbxKey.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.tbxKey.Location = New System.Drawing.Point(137, 109)
        Me.tbxKey.Name = "tbxKey"
        Me.tbxKey.Size = New System.Drawing.Size(70, 6)
        Me.tbxKey.TabIndex = 49
        '
        'pbxInputBorder
        '
        Me.pbxInputBorder.Enabled = False
        Me.pbxInputBorder.Location = New System.Drawing.Point(227, 45)
        Me.pbxInputBorder.Name = "pbxInputBorder"
        Me.pbxInputBorder.Size = New System.Drawing.Size(93, 88)
        Me.pbxInputBorder.TabIndex = 53
        Me.pbxInputBorder.TabStop = False
        '
        'tbxQR_OPID
        '
        Me.tbxQR_OPID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxQR_OPID.ForeColor = System.Drawing.Color.White
        Me.tbxQR_OPID.Location = New System.Drawing.Point(34, 100)
        Me.tbxQR_OPID.Name = "tbxQR_OPID"
        Me.tbxQR_OPID.Size = New System.Drawing.Size(53, 15)
        Me.tbxQR_OPID.TabIndex = 52
        '
        'pbxWorkSlipBorder
        '
        Me.pbxWorkSlipBorder.Location = New System.Drawing.Point(124, 46)
        Me.pbxWorkSlipBorder.Name = "pbxWorkSlipBorder"
        Me.pbxWorkSlipBorder.Size = New System.Drawing.Size(93, 88)
        Me.pbxWorkSlipBorder.TabIndex = 53
        Me.pbxWorkSlipBorder.TabStop = False
        '
        'pbxOPIDBorder
        '
        Me.pbxOPIDBorder.Location = New System.Drawing.Point(23, 47)
        Me.pbxOPIDBorder.Name = "pbxOPIDBorder"
        Me.pbxOPIDBorder.Size = New System.Drawing.Size(93, 88)
        Me.pbxOPIDBorder.TabIndex = 53
        Me.pbxOPIDBorder.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.PowderBlue
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.CloseToolStripMenuItem4, Me.ToolStripMenuItem5})
        Me.MenuStrip1.Location = New System.Drawing.Point(3, 3)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(750, 25)
        Me.MenuStrip1.TabIndex = 55
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SiableToolStripMenuItem2, Me.FixoolTStripMenuItem3})
        Me.ToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(117, 21)
        Me.ToolStripMenuItem1.Text = "FormBorderStyle"
        '
        'SiableToolStripMenuItem2
        '
        Me.SiableToolStripMenuItem2.Name = "SiableToolStripMenuItem2"
        Me.SiableToolStripMenuItem2.Size = New System.Drawing.Size(117, 22)
        Me.SiableToolStripMenuItem2.Text = "Sizable"
        '
        'FixoolTStripMenuItem3
        '
        Me.FixoolTStripMenuItem3.Name = "FixoolTStripMenuItem3"
        Me.FixoolTStripMenuItem3.Size = New System.Drawing.Size(117, 22)
        Me.FixoolTStripMenuItem3.Text = "Fixed"
        '
        'CloseToolStripMenuItem4
        '
        Me.CloseToolStripMenuItem4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.CloseToolStripMenuItem4.Name = "CloseToolStripMenuItem4"
        Me.CloseToolStripMenuItem4.Size = New System.Drawing.Size(52, 21)
        Me.CloseToolStripMenuItem4.Text = "Close"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(47, 21)
        Me.ToolStripMenuItem5.Text = "Hide"
        '
        'WindowsToolStripMenuItem
        '
        Me.WindowsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SizableToolStripMenuItem, Me.FixedToolStripMenuItem})
        Me.WindowsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.WindowsToolStripMenuItem.Name = "WindowsToolStripMenuItem"
        Me.WindowsToolStripMenuItem.Size = New System.Drawing.Size(117, 21)
        Me.WindowsToolStripMenuItem.Text = "FormBorderStyle"
        '
        'SizableToolStripMenuItem
        '
        Me.SizableToolStripMenuItem.Name = "SizableToolStripMenuItem"
        Me.SizableToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.SizableToolStripMenuItem.Text = "Sizable"
        '
        'FixedToolStripMenuItem
        '
        Me.FixedToolStripMenuItem.Name = "FixedToolStripMenuItem"
        Me.FixedToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        '
        'CloseToolStripMenuItem2
        '
        Me.CloseToolStripMenuItem2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.CloseToolStripMenuItem2.Name = "CloseToolStripMenuItem2"
        Me.CloseToolStripMenuItem2.Size = New System.Drawing.Size(52, 21)
        Me.CloseToolStripMenuItem2.Text = "Close"
        '
        'MinimizeToolStripMenuItem
        '
        Me.MinimizeToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimizeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MinimizeToolStripMenuItem.Name = "MinimizeToolStripMenuItem"
        Me.MinimizeToolStripMenuItem.Size = New System.Drawing.Size(47, 21)
        Me.MinimizeToolStripMenuItem.Text = "Hide"
        '
        'ProductionTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(764, 192)
        Me.Controls.Add(Me.tbPageMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProductionTable"
        Me.Text = "ProductionTable"
        Me.tbAlarmCellCon.ResumeLayout(False)
        Me.tbAlarmCellCon.PerformLayout()
        CType(Me.dgvAlarmCellCon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbAlarm.ResumeLayout(False)
        CType(Me.dgvAlarm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbDetail.ResumeLayout(False)
        CType(Me.dgvProductionDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbProductionPage.ResumeLayout(False)
        CType(Me.dgvProductionInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbPageMain.ResumeLayout(False)
        Me.tbOther.ResumeLayout(False)
        Me.tbOther.PerformLayout()
        CType(Me.pbxOPID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxInput, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxQR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxInputBorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxWorkSlipBorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxOPIDBorder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbAlarmCellCon As System.Windows.Forms.TabPage
    Friend WithEvents lblAlarm As System.Windows.Forms.Label
    Friend WithEvents btnCellConAlmRst As System.Windows.Forms.Button
    Friend WithEvents dgvAlarmCellCon As System.Windows.Forms.DataGridView
    Friend WithEvents tbAlarm As System.Windows.Forms.TabPage
    Friend WithEvents dgvAlarm As System.Windows.Forms.DataGridView
    Friend WithEvents tbDetail As System.Windows.Forms.TabPage
    Friend WithEvents tbProductionPage As System.Windows.Forms.TabPage
    Friend WithEvents tbPageMain As System.Windows.Forms.TabControl
    Friend WithEvents dgvProductionInfo1 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvProductionDetail As System.Windows.Forms.DataGridView
    Friend WithEvents Time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Action As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tbOther As System.Windows.Forms.TabPage
    Friend WithEvents WindowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SizableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FixedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AlarmMessage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MinimizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbxQR As System.Windows.Forms.PictureBox
    Friend WithEvents btnWorkSlip As System.Windows.Forms.Button
    Friend WithEvents tbxKey As System.Windows.Forms.TextBox
    Friend WithEvents pbxOPID As System.Windows.Forms.PictureBox
    Friend WithEvents btnOPID As System.Windows.Forms.Button
    Friend WithEvents tbxQR_OPID As System.Windows.Forms.TextBox
    Friend WithEvents LDCarrierID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ULDCarrierID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LotID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Package As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Device As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutPutPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StartTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pbxWorkSlipBorder As System.Windows.Forms.PictureBox
    Friend WithEvents pbxOPIDBorder As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SiableToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FixoolTStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbxInput As System.Windows.Forms.PictureBox
    Friend WithEvents btnInputQty As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents pbxInputBorder As System.Windows.Forms.PictureBox
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
