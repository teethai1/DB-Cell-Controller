Public Class frmWarning

    Public Sub WarningTimeout(ByVal mes As String, ByVal timeoutms As Integer)
        lbwarningMes.Text = mes
        Timer1.Interval = timeoutms
        Timer1.Start()
        Me.TopMost = True
    End Sub

    Public Sub WarningTimeout(ByVal mes As String)
        lbwarningMes.Text = mes
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Close()
    End Sub
End Class