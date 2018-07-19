Imports XtraLibrary.SecsGem

Public Class S6F4
    Inherits SecsMessageBase

    Public Sub New()
        MyBase.New(6, 4, False)
        Dim ackOk As SecsItemBinary = New SecsItemBinary("ACK", 0) 'OK only
        AddItem(ackOk)
    End Sub
End Class
