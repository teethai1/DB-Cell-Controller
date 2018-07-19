Imports System.ComponentModel
Imports Rohm.Apcs.Tdc


Public Class INPUTQty
    Public reqmode As RunModeType
    Public m_tdcErrMes As String
    'Protected Overrides ReadOnly Property CreateParams() As CreateParams   'Disable Close(x) Button
    '    Get
    '        Dim param As CreateParams = MyBase.CreateParams
    '        param.ClassStyle = param.ClassStyle Or &H200
    '        Return param
    '    End Get
    'End Property

    Private Sub K7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles K7.Click, K0.Click, K00.Click, K1.Click, K2.Click, K3.Click, K4.Click, K5.Click, K6.Click, K8.Click, K9.Click
        lbResult.Text += CType(sender, Button).Text
    End Sub



    Private Sub KBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KBS.Click
        If lbResult.Text.Length = 0 Then
            Exit Sub
        End If
        lbResult.Text = Microsoft.VisualBasic.Left(lbResult.Text, lbResult.Text.Length - 1)
    End Sub
    Dim NumCheck As Integer

    Private Sub KEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KEnter.Click
        If Not IsNumeric(lbResult.Text) Then
            lbResult.Text = ""
            Exit Sub
        End If

        NumCheck = CInt(lbResult.Text)

        If NumCheck = 0 Then
            Exit Sub
        End If


        Me.DialogResult = Windows.Forms.DialogResult.OK


    End Sub


    Private Sub KClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KClear.Click
        lbResult.Text = ""
    End Sub


    Private Sub INPUTQty_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        KEnter.Focus()
    End Sub

End Class


