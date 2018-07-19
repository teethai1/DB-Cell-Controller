Imports XtraLibrary.SecsGem
Public Class S12F2
    Inherits SecsMessageBase
    Private m_SDACK As SecsItemBinary
    Public Sub New()
        MyBase.New(12, 2, False)
        m_SDACK = New SecsItemBinary("SDACK", 0) 'OK only
        AddItem(m_SDACK)
    End Sub

End Class
