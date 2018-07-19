Imports XtraLibrary.SecsGem

Public Class S12F15

    Inherits SecsMessageBase

    Dim m_MID As New SecsItemAscii("MID")
    Dim m_IDTYP As New SecsItemBinary("IDTYP")


    Public Sub New()
        MyBase.New(12, 15, True)
        Dim M_LIST As New SecsItemList("L2")
        AddItem(M_LIST)

        M_LIST.AddItem(m_MID)
        M_LIST.AddItem(m_IDTYP)

    End Sub



    Public ReadOnly Property MID() As String
        Get
            Return m_MID.Value
        End Get
    End Property

    Public ReadOnly Property IDTYP() As Byte
        Get
            Return m_IDTYP.Value(0)
        End Get
    End Property

End Class
