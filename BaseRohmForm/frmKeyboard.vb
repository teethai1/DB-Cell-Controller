

Public Class frmKeyboard
    Public TargetText As TextBox
    Private Sub BtQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtQ.Click, BtZ.Click, BtY.Click, BtX.Click, BtW.Click, BtV.Click, BtU.Click, Btt.Click, btSpaceBar.Click, BtS.Click, BtR.Click, BtP.Click, BtO.Click, BtN.Click, BtM.Click, BtL.Click, BtK.Click, BtI.Click, BtH.Click, BtG.Click, BtF.Click, BtEnter.Click, BtE.Click, BtD.Click, BtC.Click, BtBS.Click, BtB.Click, BtA.Click, Bt9.Click, Bt8.Click, Bt7.Click, Bt6.Click, Bt5.Click, Bt4.Click, Bt3.Click, Bt2.Click, Bt1.Click, Bt0.Click, BtJ.Click, btClose.Click, btpoint.Click, Button1.Click
        Dim bt As Button = CType(sender, Button)
        TargetText.Focus()
        If bt.Text = "BACKSPACE" Then
            TargetText.Clear()
        ElseIf bt.Text = "SPACE BAR" Then
            SendKeys.Send(" ")
        ElseIf bt.Text = "TAB" Then
            SendKeys.Send("{TAB}")
        ElseIf bt.Text = "ENTER" OrElse bt.Text = "Close" Then
            Me.Hide()
        Else
            SendKeys.Send(bt.Text)
        End If
    End Sub

End Class