Imports XtraLibrary.SecsGem

Public Class S7F2
    Inherits SecsMessageBase

    Private m_PPGNT As New SecsItemBinary("IDTYP")

    Public Sub New()
        MyBase.New(7, 2, False)
        Me.AddItem(m_PPGNT)
    End Sub

    Public ReadOnly Property PPGNT() As PPGNT
        Get
            Return CType(m_PPGNT.Value(0), CellController.PPGNT)
        End Get
    End Property

End Class
