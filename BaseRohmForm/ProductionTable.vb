Imports System.ComponentModel

Public Class ProductionTable
#Region "Commomn Define"
    Event E_SlInfo(ByVal info As String)
    Event E_QRReadSuccess()
    Event E_OPIDClick()                   '170116 \783 Revise qr system
    Event E_QRReadOPIDSuccess()
    Event E_WorkSlipClick()
    Event E_AlarmCellconReset()

#End Region

    Private Sub tbOther_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles tbOther.Paint
        Dim myPen As Pen
        myPen = New Pen(Color.CadetBlue, 1)
        e.Graphics.DrawLine(myPen, 4, 28, Me.Width - 12, 28)

    End Sub
   
    Public Delegate Sub MakeAlarmCellConDelegate(ByVal AlarmMessage As String, ByVal Location As String, ByVal Status As String, ByVal AlarmID As String)
    Public Sub MakeAlarmCellCon(ByVal AlarmMessage As String, Optional ByVal Location As String = "", Optional ByVal Status As String = "", Optional ByVal AlarmID As String = "")
        If Me.InvokeRequired Then
            Me.Invoke(New MakeAlarmCellConDelegate(AddressOf MakeAlarmCellCon), AlarmMessage, Location, Status, AlarmID)
        Else
            Try
                If AlarmMessage = _PrdTableClear Then   '160712 \783 PrdTable Clear
                    dgvAlarmCellCon.Rows.Clear()
                    Exit Sub
                End If
                lblAlarm.Text = AlarmMessage
                If dgvAlarmCellCon.RowCount > 100 Then dgvAlarmCellCon.Rows.Clear()
                dgvAlarmCellCon.Rows.Add()
                Dim r As Integer = dgvAlarmCellCon.RowCount - 2
                dgvAlarmCellCon.Item(0, r).Value = Format(Now, "yyyy/MM/dd HH:mm:ss.fff")
                dgvAlarmCellCon.Item(1, r).Value = Location
                dgvAlarmCellCon.Item(2, r).Value = Status
                dgvAlarmCellCon.Item(3, r).Value = AlarmID
                dgvAlarmCellCon.Item(4, r).Value = AlarmMessage
                dgvAlarmCellCon.Sort(dgvAlarmCellCon.Columns.Item(0), ListSortDirection.Descending)
                tbPageMain.SelectTab("tbAlarmCellCon")
            Catch ex As Exception
                SaveCatchLog(ex.Message, "MakeAlarmCellCon()")
            End Try

        End If


    End Sub
    Public Delegate Sub AlarmTableDelegate(ByVal AlarmALCD As Boolean, ByVal AlarmALID As String, ByVal AlarmALTX As String, ByVal AlarmType As String)
    Public Sub AlarmTable(ByVal AlarmALCD As Boolean, ByVal AlarmALID As String, ByVal AlarmALTX As String, Optional ByVal AlarmType As String = "0")
        If Me.InvokeRequired Then
            Me.Invoke(New AlarmTableDelegate(AddressOf AlarmTable), AlarmALCD, AlarmALID, AlarmALTX, AlarmType)
        Else
            Try
                If AlarmALID = _PrdTableClear Then  '160712 \783 PrdTable Clear
                    dgvAlarm.Rows.Clear()
                    Exit Sub
                End If



                If dgvAlarm.RowCount > 50 Then dgvAlarm.Rows.Clear()
                dgvAlarm.Rows.Add()
                Dim r As Integer = dgvAlarm.RowCount - 2
                dgvAlarm.Item(0, r).Value = Format(Now, "yyyy/MM/dd HH:mm:ss.fff")
                dgvAlarm.Item(3, r).Value = AlarmType
                If AlarmALCD = True Then
                    dgvAlarm.Item(1, r).Value = "Set"
                Else
                    dgvAlarm.Item(1, r).Value = "Clear"
                End If
                dgvAlarm.Item(2, r).Value = AlarmALID
                dgvAlarm.Item(4, r).Value = AlarmALTX
                dgvAlarm.Sort(dgvAlarm.Columns.Item(0), ListSortDirection.Descending)
                'Update_dgvLogs("Received", "S5F1[" & Alarm.ALTX & "]")
            Catch ex As Exception
                SaveCatchLog(ex.Message, "AlarmTable()")
            End Try

        End If


    End Sub

    Public Delegate Sub Update_dgvProductionInfo1Delegate2(ByVal _CarrierID As String, ByVal LotID As String, ByVal Package As String, ByVal Device As String, ByVal ByValREMARK As String, ByVal StartTime As String)
    Public Sub Update_dgvProductionInfo1(ByVal _CarrierID As String, ByVal LotID As String, ByVal Package As String, ByVal Device As String, Optional ByVal REMARK As String = "", Optional ByVal StartTime As String = "")
        If Me.InvokeRequired Then
            Me.Invoke(New Update_dgvProductionInfo1Delegate2(AddressOf Update_dgvProductionInfo1), _CarrierID, LotID, Package, Device, REMARK, StartTime)
        Else

            Try

                If Not My.Settings.FrmProdTableInfo Then  '161222 \783 Add/Remove tabpage
                    Exit Sub
                End If

                If _CarrierID = _PrdTableClear Then  '160712 \783 PrdTable Clear
                    dgvProductionInfo1.Rows.Clear()
                    Exit Sub
                End If



                If dgvProductionInfo1.RowCount > 300 Then dgvProductionInfo1.Rows.Clear()
                Dim TmpRow As New DataGridViewRow
                dgvProductionInfo1.Rows.Insert(0, TmpRow)
                dgvProductionInfo1.Item("LDCarrierID", 0).Value = _CarrierID
                If StartTime = "" Then
                    dgvProductionInfo1.Item("StartTime", 0).Value = Format(Now, "yyyy/MM/dd HH:mm:ss.fff")
                Else
                    dgvProductionInfo1.Item("StartTime", 0).Value = StartTime
                End If
                dgvProductionInfo1.Item("LotID", 0).Value = LotID
                dgvProductionInfo1.Item("Package", 0).Value = Package
                dgvProductionInfo1.Item("Device", 0).Value = Device

                If Not REMARK = "" Then
                    dgvProductionInfo1.Item("Remark", 0).Value = REMARK
                End If
                tbPageMain.SelectTab("tbProductionPage")
            Catch ex As Exception
                SaveCatchLog(ex.ToString, "Update_dgvProductionInfo1(LoadCarier)")
            End Try
        End If
    End Sub

    Public Delegate Sub Update_dgvProductionInfo1Delegate3(ByVal _CarrierID As String, ByVal LotNo As String, ByVal Count As String, ByVal ByValREMARK As String)
    Public Sub Update_dgvProductionInfoEnd(ByVal _CarrierID As String, ByVal LotNo As String, ByVal Count As String, Optional ByVal REMARK As String = "")
        If Me.InvokeRequired Then
            Me.Invoke(New Update_dgvProductionInfo1Delegate3(AddressOf Update_dgvProductionInfoEnd), _CarrierID, LotNo, Count, REMARK)
        Else

            Try
                If Not My.Settings.FrmProdTableInfo Then  '161222 \783 Add/Remove tabpage
                    Exit Sub
                End If
                If _CarrierID = _PrdTableClear Then  '160712 \783 PrdTable Clear
                    dgvProductionInfo1.Rows.Clear()
                    Exit Sub
                End If

                Dim Inx As Integer = -1
                For Each Row As DataGridViewRow In dgvProductionInfo1.Rows
                    If Row.Cells("LotID").Value Is Nothing Then
                        Continue For
                    End If
                    If Row.Cells("LotID").Value.ToString = LotNo Then
                        If Row.Cells("ULDCarrierID").Value Is Nothing Then
                            Inx = Row.Index
                            Exit For
                        End If
                    End If
                Next

                If Inx = -1 Then
                    Exit Sub
                End If

                If dgvProductionInfo1.Rows(Inx).Cells("ULDCarrierID").Value Is Nothing Then
                    dgvProductionInfo1.Item("ULDCarrierID", Inx).Value = _CarrierID
                    dgvProductionInfo1.Item("EndTime", Inx).Value = Format(Now, "yyyy/MM/dd HH:mm:ss.fff")
                    dgvProductionInfo1.Item("OutPutPcs", Inx).Value = Count
                    If Not REMARK = "" Then
                        dgvProductionInfo1.Item("Remark", Inx).Value = REMARK
                    End If
                End If



            Catch ex As Exception
                SaveCatchLog(ex.ToString, "Update_dgvProductionInfo1(UnoadCarier)")
            End Try
        End If
    End Sub




    Private Delegate Sub Update_dgvProductionDetailDelegate(ByVal itemID As String, ByVal type As String, ByVal action As String, ByVal location As String)
    Public Sub Update_dgvProductionDetail(ByVal itemID As String, ByVal type As String, ByVal action As String, ByVal location As String)
        If Me.InvokeRequired Then
            Me.Invoke(New Update_dgvProductionDetailDelegate(AddressOf Update_dgvProductionDetail), itemID, type, action, location)
        Else

            If Not My.Settings.FrmProdTableIDetail Then '161222 \783 Add/Remove tabpage
                Exit Sub
            End If

            If itemID = _PrdTableClear Then  '160712 \783 PrdTable Clear
                dgvProductionDetail.Rows.Clear()
            End If


            If dgvProductionDetail.RowCount > 300 Then dgvProductionDetail.Rows.Clear()
            dgvProductionDetail.Rows.Add()
            Dim r As Integer = dgvProductionDetail.RowCount - 2
            dgvProductionDetail.Item("Time", r).Value = Format(Now, "yyyy/MM/dd HH:mm:ss.fff")
            dgvProductionDetail.Item("ItemID", r).Value = itemID
            dgvProductionDetail.Item("Type", r).Value = type
            dgvProductionDetail.Item("Action", r).Value = action
            dgvProductionDetail.Sort(dgvProductionDetail.Columns.Item(0), ListSortDirection.Descending)
            'tbPageMain.SelectTab("tbDetail")
        End If

    End Sub





    'Protected Overrides ReadOnly Property CreateParams() As CreateParams   'Disable Close(x) Button
    '    Get
    '        Dim param As CreateParams = MyBase.CreateParams
    '        param.ClassStyle = param.ClassStyle Or &H200
    '        Return param
    '    End Get
    'End Property

    Private Sub btnCellConAlmRst_Click(sender As System.Object, e As System.EventArgs) Handles btnCellConAlmRst.Click
        lblAlarm.Text = ""
        RaiseEvent E_AlarmCellconReset()
    End Sub

    
    Protected Overrides ReadOnly Property CreateParams() As CreateParams   'Disable Close(x) Button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property

    Private Sub ToolStripMenuItem4_Click(sender As System.Object, e As System.EventArgs) Handles CloseToolStripMenuItem4.Click
        Me.Close()     ' \170210 \783 Disable this function
    End Sub
  

   
    Private Sub ProductionTable_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'tbPageMain.SelectTab(1)
      
     

        If Not OprData.QrReadSystemOn Then   '160801 \783 Hide QR Read
            btnOPID.Hide()
            btnWorkSlip.Hide()
            pbxOPID.Hide()
            pbxQR.Hide()
            tbxKey.Hide()
            tbxQR_OPID.Hide()
        End If

        If Not My.Settings.FrmProdTableInfo Then
            tbPageMain.TabPages.Remove(tbProductionPage)

        End If

        If Not My.Settings.FrmProdTableIDetail Then
            tbPageMain.TabPages.Remove(tbDetail)

        End If


    End Sub
    Private Sub SiableToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles SiableToolStripMenuItem2.Click
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
    End Sub
  
    Private Sub FixoolTStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles FixoolTStripMenuItem3.Click
        Me.Height = 211
        Me.Dock = DockStyle.Bottom
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    End Sub
    Private Sub ToolStripMenuItem5_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem5.Click
        'Me.Dock = DockStyle.None    '160627 Display revise \783
        'Me.Hide()
        ''Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub pbxQR_Click(sender As System.Object, e As System.EventArgs) Handles pbxQR.Click, btnWorkSlip.Click
        QRWorkSlipClick()
    End Sub

    Private Sub QRWorkSlipClick()     '170116 \783 Revise QR Work

        If Not OprData.QrReadSystemOn Then   '160629 \783 QrSystemON/OFF 
            Exit Sub
        End If
        If Not IsNumeric(Microsoft.VisualBasic.Right(btnOPID.Text, 5)) Then '170116 \783 Support subcontact Sxxxxx
            MsgBox("Please input OPID")
            tbxQR_OPID.Focus()
            pbxOPIDBorder.BackColor = Color.Blue
            Exit Sub
        End If
        tbxKey.Text = ""
        tbxKey.Focus()
        btnWorkSlip.ForeColor = Color.Green
        RaiseEvent E_WorkSlipClick()
        RaiseEvent E_SlInfo("Please Read Work Slip QR Data")
    End Sub

    Private Sub tbxKey_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbxKey.KeyPress, TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If tbxKey.Text.Length = 252 Or tbxKey.Text.Length = My.Settings.WorkingSlipQRLenght Then              '160629 \783 Support New QR
                OprData.QrData = tbxKey.Text.ToUpper
                tbxKey.Text = ""
                RaiseEvent E_QRReadSuccess()

            Else

                RaiseEvent E_SlInfo("QR Slip Read False")
                tbxKey.Text = ""                        'Clear for New value input
            End If
        End If
        btnWorkSlip.ForeColor = Color.Black
    End Sub

    Private Sub pbxOPID_Click(sender As System.Object, e As System.EventArgs) Handles pbxOPID.Click, btnOPID.Click
        OpidQrClick()
    End Sub

    Public Sub OpidQrClick()
        'If My.Settings.SECS_Enable Or My.Settings.CsProtocol_Enable Then    'if All Protocol Disable manual input
        '    Exit Sub
        'End If

        If Not OprData.QrReadSystemOn Then   '160629 \783 QrSystemON/OFF 
            Exit Sub
        End If

     
      

        OprData.OPID = ""
        tbxQR_OPID.Text = ""
        tbxQR_OPID.Focus()
        btnOPID.ForeColor = Color.Green
        pbxOPIDBorder.BackColor = Color.Blue
        pbxWorkSlipBorder.BackColor = Color.Transparent

        RaiseEvent E_OPIDClick()
    End Sub


    Private Sub tbxQR_OPID_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbxQR_OPID.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then


            If tbxQR_OPID.Text.Length = 6 Then                   'OPID Qr length is 6


                If Not IsNumeric(Microsoft.VisualBasic.Right(tbxQR_OPID.Text, 5)) Then '170116 \783 Support subcontact Sxxxxx
                    Exit Sub
                End If

                OprData.OPID = tbxQR_OPID.Text
                tbxQR_OPID.Text = ""
                RaiseEvent E_QRReadOPIDSuccess()
                pbxOPID.Focus()
                btnOPID.ForeColor = Color.Black
                QRWorkSlipClick()
            Else

                RaiseEvent E_SlInfo("QR OPID Read False : " & tbxQR_OPID.Text)
                tbxQR_OPID.Text = ""     'Clear for New value input

            End If
        End If
        btnOPID.ForeColor = Color.Black

    End Sub

    Private Sub tbxQR_OPID_LostFocus(sender As Object, e As System.EventArgs) Handles tbxQR_OPID.LostFocus
        pbxOPIDBorder.BackColor = Color.Transparent
        btnOPID.ForeColor = Color.Black

    End Sub


    'If Tabpages = "tbOther" Then
    '     If FrmProdTable.pbxOPIDBorder.BackColor = Color.Transparent Then
    '         FrmProdTable.OpidQrClick()
    '     End If
    ' End If


    Private Sub tbxKey_LostFocus(sender As Object, e As System.EventArgs) Handles tbxKey.LostFocus, TextBox1.LostFocus
        pbxWorkSlipBorder.BackColor = Color.Transparent
        btnWorkSlip.ForeColor = Color.Black
    End Sub

  
 
End Class