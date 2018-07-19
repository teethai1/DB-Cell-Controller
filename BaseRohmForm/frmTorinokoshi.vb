Imports System.IO

Public Class frmTorinokoshi

    Private Sub frmTorinokoshi_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveTorinokoshiTable()
    End Sub

    Private Sub frmTorinokoshi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadTorinokoshiTable()
    End Sub

    Private Sub SaveTorinokoshiTable()
        Dim filepath As String = My.Application.Info.DirectoryPath & "\TorinokoshiTable.xml"
        Using sw As New StreamWriter(filepath)
            DBxDataSet.TorinokoshiPackage.WriteXml(sw)
        End Using
    End Sub
    Private Sub LoadTorinokoshiTable()
        Dim filepath As String = My.Application.Info.DirectoryPath & "\TorinokoshiTable.xml"
        If File.Exists(filepath) <> True Then
            Exit Sub
        End If

        Using sr As New StreamReader(filepath)
            DBxDataSet.TorinokoshiPackage.ReadXml(sr)
        End Using

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If CellConTag.LotID <> "" Then
            If MessageBox.Show("คุณต้องเปลี่ยนเป็นโหมด Normal ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                CellConTag.Torinokoshi = False
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If CellConTag.LotID <> "" Then
            If MessageBox.Show("คุณต้องเปลี่ยนเป็นโหมด Torinokoshi ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                CellConTag.Torinokoshi = True
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub
End Class