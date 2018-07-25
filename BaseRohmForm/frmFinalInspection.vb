Imports Rohm.Apcs.Tdc

Public Class frmFinalInspection
    Dim m_frmMain As ProcessForm
    Dim frmKey As New frmKeyboard
    Public c_EndMode As EndModeType

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btNormalEnd.Click, btAbnormalEnd.Click

        Dim bt As Button = CType(sender, Button)
        If frmKey.Visible = True Then
            frmKey.Visible = False
        End If

        If bt.Name = "btNormalEnd" Then
            c_EndMode = EndModeType.Normal
        ElseIf bt.Name = "btAbnormalEnd" Then
            Dim frmBM As New frmBMWarning
            If frmBM.ShowDialog <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            c_EndMode = EndModeType.AbnormalEndAccumulate

        End If

        If IsNumeric(tbAlmBonder.Text) = False OrElse IsNumeric(tbAlmBridgeInsp.Text) = False OrElse IsNumeric(tbAlmFrameOut.Text) = False OrElse IsNumeric(tbAlmPickup.Text) = False OrElse IsNumeric(tbAlmPreform.Text) = False OrElse IsNumeric(tbAlmPreformInsp.Text) = False Then
            m_frmWarningDialog("กรุณากรอก Alarm ", False)
            Exit Sub
        ElseIf IsNumeric(tbInputQty.Text) = False OrElse IsNumeric(tbGoodQty.Text) = False OrElse IsNumeric(tbNGQty.Text) = False OrElse IsNumeric(tbNoChip.Text) = False Then
            m_frmWarningDialog("กรุณากรอก Input ,Good ,NG และ No Chip", False)
            Exit Sub
        ElseIf CInt(tbInputQty.Text) > 70000 OrElse CInt(tbGoodQty.Text) > 70000 OrElse CInt(tbNGQty.Text) > 70000 OrElse CInt(tbNoChip.Text) > 70000 Then
            m_frmWarningDialog("กรอกจำนวนเกิน 70000 กรุณาตรวจสอบด้วยครับ", False)
            Exit Sub
        ElseIf CInt(tbGoodQty.Text) = 0 And bt.Name = "btNormalEnd" Then
            m_frmWarningDialog("Good Qty ต้องไม่เท่ากับ 0 กรุณาตรวจสอบ", False)
            Exit Sub
        End If



        If My.Settings.MCType = "IDBW" Then
            Dim frmQR As New frmOPConfirm
            If frmQR.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                Exit Sub
            End If
        Else
            Dim frmQR As New frmdisplayinput(Nothing)
            frmQR.lbcaption.Text = "กรุณาสแกน Operator Number"
            frmQR.tbOPCheck.Visible = True
            frmQR.tbOP.Visible = False
            frmQR.tbQR.Visible = False
            frmQR.Panel1.Visible = False
            If frmQR.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                Exit Sub
            End If
        End If



    End Sub



    Private Sub frmFinalInspection_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmKey.Visible = False
    End Sub

    Private Sub frmFinalInspection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lbInputQty.Text = CellConTag.INPUTQty.ToString
        lbGoodQty.Text = CStr(CellConTag.TotalGood)
        lbNGQty.Text = CStr(CellConTag.TotalNG)

        tbGoodQty.Text = lbGoodQty.Text
        tbInputQty.Text = lbInputQty.Text
        tbNGQty.Text = lbNGQty.Text

        If My.Settings.SECS_Enable = True OrElse My.Settings.MCType = "IDBW" Then
            tbAlmBonder.Text = CStr(CellConTag.AlarmBonder)
            tbAlmBridgeInsp.Text = CStr(CellConTag.AlarmBridgeInsp)
            tbAlmFrameOut.Text = CStr(CellConTag.AlarmFrameOut)
            tbAlmPickup.Text = CStr(CellConTag.AlarmPickup)
            tbAlmPreform.Text = CStr(CellConTag.AlarmPreform)
            tbAlmPreformInsp.Text = CStr(CellConTag.AlarmPreformInsp)
        End If
        If My.Settings.MCType = "Canon-D10R" Then
            gbRelease.Visible = True
        Else
            gbRelease.Visible = False
        End If

    End Sub

    Public Sub New(ByVal frmMain As ProcessForm)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_frmMain = frmMain
    End Sub

    Private Sub tbAlmPickup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbAlmPickup.Click, tbNoChip.Click, tbNGQty.Click, tbInputQty.Click, tbGoodQty.Click, tbFrameBurn.Click, tbFrameBent.Click, tbDoubleFrame.Click, tbBondingNG.Click, tbAlmPreformInsp.Click, tbAlmPreform.Click, tbAlmFrameOut.Click, tbAlmBridgeInsp.Click, tbAlmBonder.Click, tbRemark.Click
        If frmKey.IsDisposed = True Then
            frmKey = New frmKeyboard
        End If

        DefaultColorTextbox()
        Dim tb As TextBox = CType(sender, TextBox)
        frmKey.TargetText = tb
        tb.BackColor = Color.Yellow
        If tb.Name = "tbRemark" Then
            frmKey.Height = 338
        Else
            frmKey.Height = 144
        End If
        frmKey.Show()
        frmKey.Focus()
    End Sub

    Private Sub KeyboardSendkeys(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbAlmPickup.Enter, tbNoChip.Enter, tbNGQty.Enter, tbInputQty.Enter, tbGoodQty.Enter, tbFrameBurn.Enter, tbFrameBent.Enter, tbDoubleFrame.Enter, tbBondingNG.Enter, tbAlmPreformInsp.Enter, tbAlmPreform.Enter, tbAlmFrameOut.Click, tbAlmBridgeInsp.Enter, tbAlmBonder.Enter, tbAlmFrameOut.Enter, tbRemark.Enter
        If frmKey.IsDisposed = True Then
            frmKey = New frmKeyboard
        End If
        DefaultColorTextbox()
        Dim tb As TextBox = CType(sender, TextBox)
        frmKey.TargetText = tb
        tb.BackColor = Color.Yellow
        If tb.Name = "tbRemark" Then
            frmKey.Height = 338
        Else
            frmKey.Height = 144
        End If
        frmKey.Show()
        frmKey.Focus()

    End Sub


    Private Sub DefaultColorTextbox()
        tbAlmPickup.BackColor = Color.WhiteSmoke
        tbAlmPreform.BackColor = Color.WhiteSmoke
        tbAlmBonder.BackColor = Color.WhiteSmoke
        tbAlmFrameOut.BackColor = Color.WhiteSmoke
        tbAlmBridgeInsp.BackColor = Color.WhiteSmoke
        tbAlmPreformInsp.BackColor = Color.WhiteSmoke

        tbInputQty.BackColor = Color.WhiteSmoke
        tbGoodQty.BackColor = Color.WhiteSmoke
        tbNGQty.BackColor = Color.WhiteSmoke
        tbNoChip.BackColor = Color.WhiteSmoke

        tbDoubleFrame.BackColor = Color.WhiteSmoke
        tbFrameBent.BackColor = Color.WhiteSmoke
        tbFrameBurn.BackColor = Color.WhiteSmoke
        tbBondingNG.BackColor = Color.WhiteSmoke

        tbRemark.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If My.Settings.SECS_Enable = True AndAlso m_frmMain.BackColor = Color.Red Then 'ถ้าเป็น Secsgem จะต้องเช็คก่อนส่ง
            MsgBox("กรุณเชื่อมต่อCellCon กับ M/C ด้วยครับ")
            Exit Sub
        End If

        If MessageBox.Show("คุณต้องการผลิตงานต่อใช่ไหม ?", "", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If



        RemoteCMD_Remote()

        RemoteCMD_Release()

        Me.DialogResult = Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub


    Private Sub RemoteCMD_Release()
        Dim RcmdGORemote As S2F41 = New S2F41
        RcmdGORemote.RemoteCommand = "RELEASE"
        MDIParent1.Host.Send(RcmdGORemote)
    End Sub

    Private Sub RemoteCMD_Remote()
        Dim RcmdGORemote As S2F41 = New S2F41
        RcmdGORemote.RemoteCommand = "REMOTE"
        MDIParent1.Host.Send(RcmdGORemote)
    End Sub

End Class