Imports XtraLibrary.SecsGem
Public Class S12F2CanonUpload
    Inherits SecsMessageBase
    Private m_ACKC6 As SecsItemBinary
    Public Sub New()
        MyBase.New(12, 2, False)
        m_ACKC6 = New SecsItemBinary("ACK6", 0) 'OK only
        AddItem(m_ACKC6)
    End Sub

End Class
