Imports XtraLibrary.SecsGem
Public Class S1F18
    Inherits SecsMessageBase
    Private m_ONLACK As SecsItemBinary
    Public Sub New()
        MyBase.New(1, 18, False)
        m_ONLACK = New SecsItemBinary("ONLACK")
        AddItem(m_ONLACK)
    End Sub
    Public ReadOnly Property ONLACK() As ONLACK
        Get

            Return CType(m_ONLACK.Value(0), CellController.ONLACK)
        End Get
    End Property

End Class
