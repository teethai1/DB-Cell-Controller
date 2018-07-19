Imports XtraLibrary.SecsGem

Public Class S6F12
    Inherits SecsMessageBase

    Private m_ACKC6 As SecsItemBinary

    Public Sub New()
        MyBase.New(6, 12, False)

        m_ACKC6 = New SecsItemBinary("ACK6", 0) 'OK only
        AddItem(m_ACKC6)

    End Sub

End Class
