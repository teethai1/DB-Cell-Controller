Imports XtraLibrary.SecsGem
Public Class S5F4
    Inherits SecsMessageBase

    Private m_ACKC5 As SecsItemBinary

    Public Sub New()
        MyBase.New(5, 4, False)
        m_ACKC5 = New SecsItemBinary("ACKC5")
        Me.AddItem(m_ACKC5)
    End Sub

    Public ReadOnly Property ACKC5() As ACKC5
        Get
            Return CType(m_ACKC5.Value(0), CellController.ACKC5)     'Root name space of this project
        End Get
    End Property

End Class
