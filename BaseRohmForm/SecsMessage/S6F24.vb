Imports XtraLibrary.SecsGem
Public Class S6F24
    Inherits SecsMessageBase
    Private m_RSDA As New SecsItemBinary("RSDA")
    Public Sub New()
        MyBase.New(6, 24, False)
        'm_RSDA = New SecsItemBinary("RSDA")
        AddItem(m_RSDA)
    End Sub
    Public ReadOnly Property RSDA As RSDA
        Get
            Return CType(m_RSDA.Value(0), CellController.RSDA)
        End Get
    End Property


End Class
