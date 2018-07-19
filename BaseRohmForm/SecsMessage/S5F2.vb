Imports XtraLibrary.SecsGem

Public Class S5F2
    Inherits SecsMessageBase

    Private m_ACK5 As SecsItemBinary

    Public Sub New()
        MyBase.New(5, 2, False)
        m_ACK5 = New SecsItemBinary("ACK5", 0) 'ok only
        AddItem(m_ACK5)
    End Sub

End Class
