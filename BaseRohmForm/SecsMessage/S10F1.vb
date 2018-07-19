Imports XtraLibrary.SecsGem

Public Class S10F1
    Inherits SecsMessageBase
    Dim m_TEXT As New SecsItemAscii("TID")
    Dim m_TID As New SecsItemBinary("TID")

    Public Sub New()
        MyBase.New(10, 1, True)
        Dim M_LIST As New SecsItemList("L2")
        AddItem(M_LIST)
        M_LIST.AddItem(m_TID)
        M_LIST.AddItem(m_TEXT)

    End Sub

    Public ReadOnly Property Text() As String
        Get
            Return m_TEXT.Value
        End Get
    End Property

    Public ReadOnly Property TIDx() As Byte
        Get
            Return m_TID.Value(0)
        End Get
    End Property
End Class
