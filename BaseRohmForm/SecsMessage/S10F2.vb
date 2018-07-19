Imports XtraLibrary.SecsGem

Public Class S10F2
    Inherits SecsMessageBase

    Private m_ACKC10 As SecsItemBinary

    Public Sub New(ByVal Ack As ACKC10)
        MyBase.New(10, 2, False)

        m_ACKC10 = New SecsItemBinary("ACKC10", Ack)
        Me.AddItem(m_ACKC10)

    End Sub


   



End Class
