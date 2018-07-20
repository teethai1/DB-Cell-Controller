Imports Rohm.Apcs.Tdc
Imports CellController.ServiceReference1

Public Class frmdisplayinput
    Public reqmode As RunModeType
    Public LotReqReply As String
    Public m_DenpyoChipSizeX As String
    Public m_DenpyoChipSizeY As String
    Public m_DenpyoTsukaigeNo As String
    Public m_DenpyoRubberNo As String
    Public m_DenpyoPreformType As String
    Dim m_frmMain As ProcessForm

    Private Sub tbQR_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbQR.KeyPress
        Dim probar As Integer = CInt((tbQR.Text.Length / 252) * 100)
        If probar > 100 Then
            probar = 100
        End If
        ProgressBar1.Value = probar

        If e.KeyChar = Convert.ToChar(13) Then
            If tbQR.Text.Length = 252 Or tbQR.Text.Length = My.Settings.WorkingSlipQRLenght Then              '160629 \783 Support New QR
                Dim WorkSlipQR As New WorkingSlipQRCode
                WorkSlipQR.SplitQRCode(tbQR.Text.ToUpper)

                If rbnNormalStartTDC.Checked Then
                    reqmode = RunModeType.Normal
                ElseIf rbnSeparateStartTDC.Checked Then
                    reqmode = RunModeType.Separated
                ElseIf rbnSeparateEndStartTDC.Checked Then
                    reqmode = RunModeType.SeparatedEnd
                End If

                If LotRequestTDC(WorkSlipQR.LotNo, reqmode) = False Then
                    ProgressBar1.Value = 0
                    tbQR.Text = ""
                    tbQR.Focus()
                    Exit Sub
                End If




                If m_frmMain.QRWorkingSlipInputInitailCheck(False, WorkSlipQR) = True Then

                    TorinokoshiPackage(WorkSlipQR.Package)

                    Para.QrData = tbQR.Text.ToUpper
                    Para.WaferLotID = WorkSlipQR.WFLotNo

                    tbQR.Text = ""
                    tbOP.Focus()
                    tbQR.Visible = False
                    ProgressBar1.Value = tbOP.Text.Length
                    lbcaption.Text = "กรุณาสแกน Operator Number"
                    Panel1.Visible = False
                Else
                    m_frmWarningDialog(m_frmMain.m_QRReadAlarm, True)

                    ProgressBar1.Value = 0
                    tbQR.Text = ""
                    tbQR.Focus()
                End If
            Else
                ProgressBar1.Value = 0
                tbQR.Text = ""
                m_frmWarningDialog("QRCode ไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง", True)                'Clear for New value input
            End If
        End If

    End Sub

    Private Sub tbOP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbOP.KeyPress

        Dim probar As Integer = CInt((tbOP.Text.Length / 6) * 100)
        If probar > 100 Then
            probar = 100
        End If
  
        ProgressBar1.Value = probar
        Dim wr As New WorkingSlipQRCode
        If e.KeyChar = Convert.ToChar(13) Then


            If tbOP.Text.Length = 6 Then                   'OPID Qr length is 6
                If Not IsNumeric(Microsoft.VisualBasic.Right(tbOP.Text, 5)) Then '170116 \783 Support subcontact Sxxxxx
                    m_frmWarningDialog("OP No:" & tbOP.Text & " ไม่ถูกต้องกรุณาตรวจสอบ", True)
                    ProgressBar1.Value = 0
                    tbOP.Text = ""
                    tbOP.Focus()
                    Exit Sub
                End If

                If My.Settings.PersonAuthorization = True Then
                    Dim workslip As New WorkingSlipQRCode
                    workslip.SplitQRCode(Para.QrData)

                    Dim Authen As New Authentication
                    Authen.PermiisionCheck(workslip.DeviceTPDirection2, tbOP.Text, My.Settings.UserAuthenOP, My.Settings.UserAuthenGL, My.Settings.ProcessName, My.Settings.ProcessName & "-" & My.Settings.EquipmentNo)
                    If Authen.Ispass = False Then
                        m_frmWarningDialog(Authen.ErrorMessage, True)
                        tbOP.Text = ""
                        ProgressBar1.Value = 0
                        tbOP.Focus()
                        Exit Sub
                    End If

                End If

                Para.OPID = tbOP.Text

                If rdNormal.Checked = True Then
                    Para.Torinokoshi = False
                ElseIf rdTorinokoshi.Checked = True Then
                    Para.Torinokoshi = True
                Else
                    Para.Torinokoshi = False
                End If

                tbOP.Visible = False

                'Input Qty

                wr.SplitQRCode(Para.QrData)

                Try
                    wr.TransactionDataSave(Para.QrData)
                Catch ex As Exception

                End Try



                Dim Denpyo As New DBxDataSetTableAdapters.LCQW_UNION_WORK_DENPYO_PRINTTableAdapter
                Dim _DenPyoTable As DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTDataTable = New DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTDataTable
                Denpyo.Fill(_DenPyoTable, wr.LotNo)
                For Each strDataRow As DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTRow In _DenPyoTable.Rows
                    m_DenpyoChipSizeX = strDataRow.ChipSizeX
                    m_DenpyoChipSizeY = strDataRow.ChipSizeY
                    m_DenpyoTsukaigeNo = strDataRow.TsukaigeNo
                    m_DenpyoRubberNo = strDataRow.RubberNo
                    m_DenpyoPreformType = strDataRow.Preform
                Next

                Dim inputQty As New INPUTQty

                Dim frmInsp As New frmFirstInspection
                If inputQty.ShowDialog = Windows.Forms.DialogResult.OK Then 'InputQty form

                    frmInsp.tbChipSizeX.Text = m_DenpyoChipSizeX
                    frmInsp.tbChipSizeY.Text = m_DenpyoChipSizeY
                    frmInsp.tbTsukaigeNeedNo.Text = m_DenpyoTsukaigeNo
                    frmInsp.tbRubberColletNo.Text = m_DenpyoRubberNo

                    If My.Settings.ColletCheckControl Then
                        Dim frmCollet As New frmInputJig(wr.LotNo, "DB-" & My.Settings.EquipmentNo, My.Settings.JigMcType, tbOP.Text)
                        If frmCollet.ShowDialog <> Windows.Forms.DialogResult.OK Then
                            Para.RubberColletID = frmCollet.c_ItemCode
                            Me.Close()
                            Exit Sub
                        End If
                    End If

                    If frmInsp.ShowDialog <> Windows.Forms.DialogResult.OK Then 'First Insp. form

                        Me.Close()
                        Exit Sub
                    End If
                Else
                    'Dim warning As New frmWarning
                    'warning.WarningTimeout(inputQty.m_tdcErrMes & vbCrLf & "Err TDC ไม่สามารถทำงานได้ กรุณาตรวจสอบ LotNo นี้" & vbCrLf & wr.LotNo)
                    'warning.ShowDialog()
                    Me.Close()
                    Exit Sub
                End If

                With Para
                    'OP QR Input
                    wr.SplitQRCode(.QrData)
                    .LotID = wr.LotNo
                    .Package = wr.Package
                    .DeviceName = wr.Device
                    .WaferLotID = wr.WFLotNo
                    .Recipe = wr.Code
                    .INPUTQty = CInt(inputQty.lbResult.Text)
                    .LSMode = reqmode
                    .LRReply = LotReqReply

                    'FirstInsp.

                    .TsukaigeNeedNo = CShort(frmInsp.tbTsukaigeNeedNo.Text)
                    If frmInsp.rdTsukaigeA.Checked = True Then
                        .TsukaigeMode = "A"
                    ElseIf frmInsp.rdTsukaigeB.Checked = True Then
                        .TsukaigeMode = "B"
                    Else
                        .TsukaigeMode = "C"
                    End If

                    If frmInsp.tbTsukaigePinStrock.Text <> "" Then
                        .TsukaigePinStrock = CShort(frmInsp.tbTsukaigePinStrock.Text)
                    End If

                    .RubberColletNo = CShort(frmInsp.tbRubberColletNo.Text)

                    If frmInsp.rdRubberCheckA.Checked = True Then
                        .RubberMode = "A"
                    ElseIf frmInsp.rdRubberCheckB.Checked = True Then
                        .RubberMode = "B"
                    Else
                        .RubberMode = "C"
                    End If


                    If frmInsp.rbPasteNozzleA.Checked = True Then
                        .PasteNozzleMode = "A"
                    ElseIf frmInsp.rbPasteNozzleB.Checked = True Then
                        .PasteNozzleMode = "B"
                    ElseIf frmInsp.rbPasteNozzleC.Checked = True Then
                        .PasteNozzleMode = "C"
                    End If


                    If frmInsp.cbPasteNozzleType.Text <> "" Then
                        .PasteNozzleType = frmInsp.cbPasteNozzleType.Text
                    End If

                    If frmInsp.cbxPastNozzleNo.Text <> "" Then
                        .PastNozzleNo = frmInsp.cbxPastNozzleNo.Text
                    End If



                    If frmInsp.rdBlockChcekA.Checked = True Then
                        .BlockCheck = "A"
                    ElseIf frmInsp.rdBlockChcekB.Checked = True Then
                        .BlockCheck = "B"
                    Else
                        .BlockCheck = "C"
                    End If

                    If frmInsp.tbChipSizeX.Text <> "" Then
                        .ChipSizeX = CSng(frmInsp.tbChipSizeX.Text)
                    End If
                    If frmInsp.tbChipSizeY.Text <> "" Then
                        .ChipSizeY = CSng(frmInsp.tbChipSizeY.Text)
                    End If

                    If frmInsp.tbPreformThickness0.Text <> "" Then
                        .PreformThickness1 = CShort(frmInsp.tbPreformThickness0.Text)
                    End If
                    If frmInsp.tbPreformThickness1.Text <> "" Then
                        .PreformThickness2 = CShort(frmInsp.tbPreformThickness1.Text)
                    End If
                    If frmInsp.tbPreformThickness2.Text <> "" Then
                        .PreformThickness3 = CShort(frmInsp.tbPreformThickness2.Text)
                    End If
                    If frmInsp.tbPreformThickness3.Text <> "" Then
                        .PreformThickness4 = CShort(frmInsp.tbPreformThickness3.Text)
                    End If
                    If frmInsp.tbPreformAver.Text <> "" Then
                        .PreformThicknessAverage = CShort(frmInsp.tbPreformAver.Text)
                    End If
                    If frmInsp.tbPreformR.Text <> "" Then
                        .PreformThicknessR = CShort(frmInsp.tbPreformR.Text)
                    End If
                End With

                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                m_frmWarningDialog("OP No:" & tbOP.Text & " ไม่ถูกต้องกรุณาตรวจสอบ", True)
                ProgressBar1.Value = 0
                tbOP.Text = ""
                tbOP.Focus()
            End If
        End If
    End Sub

    Private Sub frmdisplay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Para = New CellConObj

    End Sub

    Public Sub New(ByVal frmmain As ProcessForm)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_frmMain = frmmain
    End Sub


    Private Sub tbOPCheck_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbOPCheck.KeyPress
        Dim probar As Integer = CInt((tbOPCheck.Text.Length / 6) * 100)
        If probar > 100 Then
            probar = 100
        End If
        ProgressBar1.Value = probar
        Dim wr As New WorkingSlipQRCode
        If e.KeyChar = Convert.ToChar(13) Then
            If tbOPCheck.Text.Length = 6 Then                   'OPID Qr length is 6
                If Not IsNumeric(Microsoft.VisualBasic.Right(tbOPCheck.Text, 5)) Then '170116 \783 Support subcontact Sxxxxx
                    m_frmWarningDialog("OP No " & tbOPCheck.Text & " ไม่ถูกต้องกรุณาตรวจสอบ", True)
                    Exit Sub
                End If

                If My.Settings.PersonAuthorization = True Then

                    Dim workslip As New WorkingSlipQRCode
                    workslip.SplitQRCode(Para.QrData)

                    Dim Authen As New Authentication
                    Authen.PermiisionCheck(workslip.DeviceTPDirection2, tbOP.Text, My.Settings.UserAuthenOP, My.Settings.UserAuthenGL, My.Settings.ProcessName, My.Settings.ProcessName & "-" & My.Settings.EquipmentNo)
                    If Authen.Ispass = False Then
                        m_frmWarningDialog(Authen.ErrorMessage, True)
                        tbOPCheck.Text = ""
                        ProgressBar1.Value = 0
                        tbOPCheck.Focus()
                        Exit Sub
                    End If

                End If


                Para.OPCheck = tbOPCheck.Text
                Me.DialogResult = Windows.Forms.DialogResult.OK
                tbOPCheck.Visible = False
            Else
                m_frmWarningDialog("QRCode:" & tbOPCheck.Text & " ไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง", True)
                ProgressBar1.Value = 0
                tbOPCheck.Text = ""
                tbOPCheck.Focus()
            End If
        End If


    End Sub

    Function LotRequestTDC(ByVal LotNo As String, ByVal rm As RunModeType) As Boolean
        Dim mc As String = My.Settings.ProcessName & "-" & My.Settings.EquipmentNo

        Dim res As TdcLotRequestResponse = m_TdcService.LotRequest(mc, LotNo, rm)

        If res.HasError Then

            Using svError As ApcsWebServiceSoapClient = New ApcsWebServiceSoapClient
                If svError.LotRptIgnoreError(mc, res.ErrorCode) = False Then
                    Dim li As LotInfo = Nothing
                    li = m_TdcService.GetLotInfo(LotNo, mc)
                    Using dlg As TdcAlarmMessageForm = New TdcAlarmMessageForm(res.ErrorCode, res.ErrorMessage, LotNo, li)
                        dlg.ShowDialog()
                        Return False
                    End Using
                End If
            End Using
            LotReqReply = res.ErrorCode & " : " & res.ErrorMessage
            Return True
        Else
            LotReqReply = "00 : Run Normal"
            Return True
        End If



    End Function



    Private Sub rbnSeparateEndStartTDC_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnSeparateStartTDC.Enter, rbnSeparateEndStartTDC.Enter, rbnNormalStartTDC.Enter
        tbQR.Focus()
    End Sub

    Sub TorinokoshiPackage(ByVal package As String)
        For Each strdatarow As DBxDataSet.TorinokoshiPackageRow In m_frmMain.DBxDataSet.TorinokoshiPackage.Rows
            If package = strdatarow.Package Then
                panelTorinokoshi.Visible = True
            End If
        Next
    End Sub

    Private Sub rdTorinokoshi_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdTorinokoshi.Enter, rdNormal.Enter
        tbOP.Focus()
    End Sub
End Class

