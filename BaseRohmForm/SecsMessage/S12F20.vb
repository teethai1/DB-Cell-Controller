Imports XtraLibrary.SecsGem

Public Class S12F20
    Inherits SecsMessageBase

    Private m_LRACK As SecsItemBinary

    Public Sub New()
        MyBase.New(12, 20, False)
        m_LRACK = New SecsItemBinary("LRACK", 0)
        Me.AddItem(m_LRACK)

    End Sub


    Public ReadOnly Property LRACK() As LRACK
        Get
            Return CType(m_LRACK.Value(0), CellController.LRACK)
        End Get
    End Property

End Class
