Public Class frmFrameInputManual
    Dim _tb As TextBox
    Public _MarkerLotNo As String
    Public _FrameType As String
    Dim _ShowFrameType As Boolean
    Private Sub Bt1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt1.Click, Button1.Click, BtZ.Click, BtY.Click, BtX.Click, BtW.Click, BtV.Click, BtU.Click, Btt.Click, BtS.Click, BtR.Click, BtQ.Click, BtP.Click, BtO.Click, BtN.Click, BtM.Click, BtL.Click, BtK.Click, BtJ.Click, BtI.Click, BtH.Click, BtG.Click, BtF.Click, BtE.Click, BtD.Click, BtC.Click, BtBS.Click, BtB.Click, BtA.Click, Bt9.Click, Bt8.Click, Bt7.Click, Bt6.Click, Bt5.Click, Bt4.Click, Bt3.Click, Bt2.Click, Bt0.Click
        Dim bt As Button = CType(sender, Button)
        _tb.Focus()
        If bt.Text <> "BACKSPACE" Then
            SendKeys.Send(bt.Text.ToUpper)
        Else
            SendKeys.Send("{BS}")
        End If
    End Sub

    Private Sub tbMarkerLotNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbMarkerLotNo.Click, tbFrameType.Click
        tbFrameType.BackColor = Color.White
        tbMarkerLotNo.BackColor = Color.White
        Dim tb As TextBox = CType(sender, TextBox)
        _tb = tb
        _tb.BackColor = Color.Yellow
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If tbMarkerLotNo.Text = "" Then
            MsgBox("กรุณากรอกข้อมูล Marker LotNo")
            Exit Sub
        ElseIf tbFrameType.Text = "" Then
            MsgBox("กรุณากรอกข้อมูล FrameType")
            Exit Sub
        ElseIf tbMarkerLotNo.Text.Length > 20 Then
            MsgBox("จำนวนตัวอักษรของ Marker Lot No เกิน 20 ตัว กรุณาตรวจสอบ")
            Exit Sub
        ElseIf tbFrameType.Text.Length > 16 Then
            MsgBox("จำนวนตัวอักษรของ FrameType เกิน 16 ตัว กรุณาตรวจสอบ")
            Exit Sub
        End If

        _MarkerLotNo = tbMarkerLotNo.Text
        _FrameType = tbFrameType.Text

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmFrameInputManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _tb = tbMarkerLotNo
        _tb.BackColor = Color.Yellow
        _tb.Focus()
        _tb.Select()
    End Sub
End Class