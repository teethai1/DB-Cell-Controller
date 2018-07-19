Imports XtraLibrary.SecsGem

Public Class S2F36
    Inherits SecsMessageBase

    Private m_LRACK As SecsItemBinary

    Public Sub New()
        MyBase.New(2, 36, False)

        m_LRACK = New SecsItemBinary("LRACK")
        Me.AddItem(m_LRACK)

    End Sub


    Public ReadOnly Property LRACK() As LRACK
        Get
            Return CType(m_LRACK.Value(0), CellController.LRACK)
        End Get
    End Property

End Class
