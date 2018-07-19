Imports XtraLibrary.SecsGem
Public Class S7F17
    Inherits SecsMessageBase
    Private m_Ln As SecsItemList
    Public Sub New()
        MyBase.New(7, 17, True)
        m_Ln = New SecsItemList("Ln")
        AddItem(m_Ln)
    End Sub

    Public Sub AddPPID(ByVal PPID As String)

        Dim PPIDx As New SecsItemAscii("PPID", PPID)
        m_Ln.AddItem(PPIDx)
    End Sub

End Class
