Public Class QrInput
    Dim QrT As QrType
    Sub New(ByVal QrInputType As QrType)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        QrT = QrInputType
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then

            If QrT = QrType.WorkingSlip Then
                If TextBox1.Text.Length = 252 Or TextBox1.Text.Length = My.Settings.WorkingSlipQRLenght Then              '160629 \783 Support New QR
                    Dim WorkSlipQR As New WorkingSlipQRCode
                    WorkSlipQR.SplitQRCode(TextBox1.Text.ToUpper)
                    OprData.ReloadLot = WorkSlipQR.LotNo
                Else
                    'Clear for New value input
                    OprData.ReloadLot = ""

                End If

            End If

            If QrT = QrType.OPID Then
                OprData.ReloadLot = ""
                If TextBox1.Text.Length = 6 Then                   'OPID Qr length is 6
                    If IsNumeric(Microsoft.VisualBasic.Right(TextBox1.Text, 5)) Then '170116 \783 Support subcontact Sxxxxx
                        OprData.ReloadLot = TextBox1.Text
                    End If
                End If

            End If
          

            TextBox1.Text = ""
            Me.Close()
        End If



    End Sub

    Private Sub QrInput_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.CenterToParent()
    End Sub

    Private Sub QrInput_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        TextBox1.Focus()
    End Sub

    Public Enum QrType
        WorkingSlip
        DicingSlip
        OPID
    End Enum

End Class