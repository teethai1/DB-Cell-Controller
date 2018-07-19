Imports XtraLibrary.SecsGem
Public Class S7F18
    Inherits SecsMessageBase
    Private m_ACKC7 As New SecsItemBinary("ACKC7")
    Public Sub New()
        MyBase.New(7, 18, False)
        AddItem(m_ACKC7)
    End Sub

    Public ReadOnly Property ACK7() As ACKC7
        Get
            Return CType(m_ACKC7.Value(0), ACKC7)
        End Get
    End Property

End Class
