Imports XtraLibrary.SecsGem

Public Class S64F4
    Inherits SecsMessageBase

    Public Sub New()
        MyBase.New(64, 4, False)
        Dim ackOk As SecsItemBinary = New SecsItemBinary("ACK")
        ackOk.Value = New Byte() {0} 'OK
        AddItem(ackOk)
    End Sub

End Class
