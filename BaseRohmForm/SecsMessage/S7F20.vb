Imports XtraLibrary.SecsGem

Public Class S7F20
    Inherits SecsMessageBase

    Private m_List As SecsItemList

    Public Sub New()
        MyBase.New(7, 20, False)
        m_List = New SecsItemList("Ln")
        AddItem(m_List)
    End Sub

    Public Function GetPPIDs() As String()

        Dim ppidList As List(Of String) = New List(Of String)
        For Each item As SecsItem In m_List.Value
            ppidList.Add(DirectCast(item.Value, String))
        Next
        Return ppidList.ToArray()

    End Function
End Class
