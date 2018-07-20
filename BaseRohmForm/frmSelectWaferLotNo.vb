Public Class frmSelectWaferLotNo
    Public m_waferSelect As String
    Public m_currentwafer As String
    Private Sub frmSelectWaferLotNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each strdata As String In CellConTag.WaferLotNoListSplited
            cbWaferLotNo.Items.Add(strdata)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If cbWaferLotNo.Text = "" Then
            m_frmWarningDialog("กรุณาเลือก Wafer LotNo", True, 30000)
            Exit Sub
            'ElseIf cbWaferLotNo.Text = m_currentwafer Then
            '    frmWarning.WarningTimeout("Wafer LotNo นี้ใช้งานอยู่แล้ว กรุณาเลือก Wafer LotNo ใหม่", 30000)
            '    frmWarning.ShowDialog()
            '    Exit Sub
        End If

        m_waferSelect = cbWaferLotNo.Text
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public Sub New(ByVal currentWafer As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_currentwafer = currentWafer
    End Sub
End Class