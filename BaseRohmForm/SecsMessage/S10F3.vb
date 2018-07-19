Imports XtraLibrary.SecsGem

Public Class S10F3
    Inherits SecsMessageBase
    Public Sub New(ByVal TID As Byte, ByVal MSG As String)
        MyBase.New(10, 3, True)
        Dim m_TEXT As New SecsItemAscii("TEXT", MSG)
        Dim m_TID As New SecsItemBinary("TID", TID)
        Dim M_LIST As New SecsItemList("L2")
        AddItem(M_LIST)
        M_LIST.AddItem(m_TID)
        M_LIST.AddItem(m_TEXT)

    End Sub

End Class
