
Public Class frmBMWarning
    Dim c_CountDown As Integer = 20


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        c_CountDown -= 1
        If c_CountDown = 0 Then
            btYes.Text = "Yes..(" & c_CountDown & ")"
            btYes.BackColor = Color.Lime
            btYes.Enabled = True
            Timer1.Enabled = False
        Else
            btYes.Text = "กรุณารอ..(" & c_CountDown & ")"
            btYes.BackColor = Color.WhiteSmoke
            btYes.Enabled = False
        End If

    End Sub

    Private Sub btYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btYes.Click
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub btNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btNo.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub
End Class