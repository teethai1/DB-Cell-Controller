Imports XtraLibrary.SecsGem

Public Class S12F3
    Inherits SecsMessageBase
    Dim m_MID As New SecsItemAscii("MID")
    Dim m_IDTYP As New SecsItemBinary("IDTYP")
    Dim m_MAPFT As New SecsItemBinary("MAPFT")
    Dim m_FNLOC As New SecsItemU2("FNLOC")
    Dim m_FFROT As New SecsItemU2("FFROT")
    Dim m_ORLOC As New SecsItemBinary("ORLOC")
    Dim m_PRAXI As New SecsItemBinary("PRAXI")
    Dim m_BCEQU As New SecsItemAscii("BCEQU")
    Dim m_NULBC As New SecsItemAscii("NULBC")

    Public Sub New()
        MyBase.New(12, 3, True)
        Dim M_LIST As New SecsItemList("L4")
        AddItem(M_LIST)
        M_LIST.AddItem(m_MID)
        M_LIST.AddItem(m_IDTYP)
        M_LIST.AddItem(m_MAPFT)
        M_LIST.AddItem(m_FNLOC)
        M_LIST.AddItem(m_FFROT)
        M_LIST.AddItem(m_ORLOC)
        M_LIST.AddItem(m_PRAXI)
        M_LIST.AddItem(m_BCEQU)
        M_LIST.AddItem(m_NULBC)
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

    Public ReadOnly Property MAPFT() As Byte
        Get
            Return m_MAPFT.Value(0)
        End Get
    End Property
    Public ReadOnly Property FNLOC() As UInt16
        Get
            Return m_FNLOC.Value(0)
        End Get
    End Property

    Public ReadOnly Property FFROT() As UInt16
        Get
            Return m_FFROT.Value(0)
        End Get
    End Property

    Public ReadOnly Property ORLOC() As Byte
        Get
            Return m_ORLOC.Value(0)
        End Get
    End Property

    Public ReadOnly Property PRAXI() As Byte
        Get
            Return m_PRAXI.Value(0)
        End Get
    End Property

    Public ReadOnly Property BCEQU() As String
        Get
            Return m_BCEQU.Value
        End Get
    End Property

    Public ReadOnly Property NULBC() As String
        Get
            Return m_NULBC.Value
        End Get
    End Property

End Class
