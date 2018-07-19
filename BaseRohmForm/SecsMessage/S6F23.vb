Imports XtraLibrary.SecsGem
Public Class S6F23
    Inherits SecsMessageBase

    Public Sub New(ByVal RSDC As SpoolCode)
        MyBase.New(6, 23, True)
        Dim m_RSDC As New SecsItemU1("RSDC", RSDC)
        AddItem(m_RSDC)
    End Sub



End Class
