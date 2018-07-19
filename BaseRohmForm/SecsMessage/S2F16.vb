Imports XtraLibrary.SecsGem
Public Class S2F16
    Inherits SecsMessageBase
    Private m_EAC As SecsItemBinary

    Public Sub New()
        MyBase.New(2, 16, False)
        m_EAC = New SecsItemBinary("EAC")
        Me.AddItem(m_EAC)
    End Sub

    Public ReadOnly Property EAC As EAC
        Get
            Return CType(m_EAC.Value(0), CellController.EAC)
        End Get
    End Property



End Class
