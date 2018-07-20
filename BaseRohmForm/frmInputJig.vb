Imports CellController.JIGWebService

Public Class frmInputJig
    Dim c_LotNo As String
    Dim c_McNo As String
    Dim c_McType As String
    Dim c_OpNo As String
    Public c_ItemCode As String
    Private Sub frmInputJig_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbRevQr.Focus()
    End Sub

    Private Sub tbRevQr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbRevQr.KeyPress

        If tbRevQr.Text.Length <= 10 Then
            ProgressBar1.Value = tbRevQr.Text.Length
        End If

        If e.KeyChar = Chr(13) Then
            If tbRevQr.Text.Length <> 9 Then
                m_frmWarningDialog("QrCode is invalid.  Please try again ", True)
                tbRevQr.Text = ""
                tbRevQr.Focus()
                Exit Sub
            ElseIf Not tbRevQr.Text.Contains("JIG") Then
                m_frmWarningDialog("QrCode is invalid.  Please try again ", True)
                tbRevQr.Text = ""
                tbRevQr.Focus()
                Exit Sub
            End If

            If CheckCollet(c_LotNo, tbRevQr.Text, c_McNo, c_McType, c_OpNo) = True Then
                Para.RubberColletID = c_ItemCode
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                tbRevQr.Text = ""
                tbRevQr.Focus()
                Exit Sub
            End If


        End If
    End Sub


    Function CheckCollet(ByVal lotNo As String, ByVal qrCode As String, ByVal mcNo As String, ByVal mcType As String, ByVal opNo As String) As Boolean
        Dim colletService As JIGServiceSoapClient = New JIGServiceSoapClient
        Dim ResultClass As CheckResult = colletService.SetupCollet(lotNo, qrCode, mcNo, mcType, opNo)
        ' Dim ResultClass1 As CheckResultJIGInfo = colletService.SetupColletWithInfo(lotNo, qrCode, mcNo, mcType, opNo)
        If ResultClass.IsPass = True Then
            c_ItemCode = ResultClass.ItemCode
            Return True
        Else
            m_frmWarningDialog(ResultClass.ErrorMessage, False)
            Return False
        End If
    End Function

    Public Sub New(ByVal lotNo As String, ByVal mcNo As String, ByVal mcType As String, ByVal opNo As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        c_LotNo = lotNo
        c_McNo = mcNo
        c_McType = mcType
        c_OpNo = opNo
    End Sub
End Class