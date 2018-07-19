Imports XtraLibrary.SecsGem

Public Class S2F34
    Inherits SecsMessageBase

    Private m_DRACK As SecsItemBinary

    Public Sub New()
        MyBase.New(2, 34, False)
        m_DRACK = New SecsItemBinary("DRACK")
        Me.AddItem(m_DRACK)
    End Sub

    Public ReadOnly Property DRACK() As DRACK
        Get
            Return CType(m_DRACK.Value(0), CellController.DRACK)     'Root name space of this project
        End Get
    End Property

End Class
