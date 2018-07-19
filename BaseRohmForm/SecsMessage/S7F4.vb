Imports XtraLibrary.SecsGem

Public Class S7F4
    Inherits SecsMessageBase

    Private m_ACK7 As New SecsItemBinary("ACK7")

    Public Sub New()
        MyBase.New(7, 4, False)
        Me.AddItem(m_ACK7)
    End Sub

    Public ReadOnly Property ACK7() As ACKC7
        Get
            Return CType(m_ACK7.Value(0), ACKC7)
        End Get
    End Property

End Class
