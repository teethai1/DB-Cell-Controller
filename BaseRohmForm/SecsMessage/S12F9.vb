Imports XtraLibrary.SecsGem

Public Class S12F9

    Inherits SecsMessageBase
    Dim m_MID As New SecsItemAscii("MID")
    Dim m_IDTYP As New SecsItemBinary("IDTYP")
    Dim m_STRP As New SecsItemI2("STRP")
    Dim m_BINLT As New SecsItemAscii("BINLT")


    Public Sub New()
        MyBase.New(12, 9, True)
        Dim M_LIST As New SecsItemList("L4")
        AddItem(M_LIST)
        M_LIST.AddItem(m_MID)
        M_LIST.AddItem(m_IDTYP)
        M_LIST.AddItem(m_STRP)
        M_LIST.AddItem(m_BINLT)
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

    Public ReadOnly Property STRP() As Short
        Get
            Return m_STRP.Value(0)
        End Get
    End Property
    Public ReadOnly Property BINLT() As String
        Get
            Return m_BINLT.Value
        End Get
    End Property
End Class
