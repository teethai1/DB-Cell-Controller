Public Class frmOPConfirm

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button9.Click, Button8.Click, Button7.Click, Button6.Click, Button5.Click, Button4.Click, Button3.Click, Button2.Click, Button12.Click
        Dim bt As Button = CType(sender, Button)
        tbOPNo.Focus()
        SendKeys.Send(bt.Text)
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        tbOPNo.Text = ""
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click

        If tbOPNo.Text.Length = 6 Then                   'OPID Qr length is 6
            If Not IsNumeric(Microsoft.VisualBasic.Right(tbOPNo.Text, 5)) Then '170116 \783 Support subcontact Sxxxxx

                m_frmWarningDialog("OP No " & tbOPNo.Text & " ไม่ถูกต้องกรุณาตรวจสอบ", True)
                Exit Sub
            End If

            If My.Settings.PersonAuthorization = True Then

                Dim workslip As New WorkingSlipQRCode
                workslip.SplitQRCode(CellConTag.QrData)

                Dim Authen As New Authentication
                Authen.PermiisionCheck(workslip.DeviceTPDirection2, tbOPNo.Text, My.Settings.UserAuthenOP, My.Settings.UserAuthenGL, My.Settings.ProcessName, My.Settings.ProcessName & "-" & My.Settings.EquipmentNo)
                If Authen.Ispass = False Then
                    m_frmWarningDialog(Authen.ErrorMessage, True)
                    tbOPNo.Text = ""
                    Exit Sub
                End If

            End If

            CellConTag.OPCheck = tbOPNo.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            m_frmWarningDialog("QRCode:" & tbOPNo.Text & " ไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง", True)
            tbOPNo.Text = ""
        End If
    End Sub
End Class