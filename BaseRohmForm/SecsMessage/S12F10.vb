Imports XtraLibrary.SecsGem

Public Class S12F10
    Inherits SecsMessageBase

    Private m_ACKC6 As SecsItemBinary

    Public Sub New()
        MyBase.New(12, 10, False)

        m_ACKC6 = New SecsItemBinary("ACK6", 0) 'OK only
        AddItem(m_ACKC6)

    End Sub
End Class
