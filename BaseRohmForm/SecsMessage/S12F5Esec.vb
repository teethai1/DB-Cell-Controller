Imports XtraLibrary.SecsGem

Public Class S12F5Esec
    Inherits SecsMessageBase
    Dim m_MID As New SecsItemAscii("MID")
    Dim m_IDTYP As New SecsItemBinary("IDTYP")
    Dim m_MAPFT As New SecsItemBinary("MAPFT")
    Dim m_MLCL As New SecsItemU4("MLCL")


    Public Sub New()
        MyBase.New(12, 5, True)
        Dim M_LIST As New SecsItemList("L4")
        AddItem(M_LIST)
        M_LIST.AddItem(m_MID)
        M_LIST.AddItem(m_IDTYP)
        M_LIST.AddItem(m_MAPFT)
        M_LIST.AddItem(m_MLCL)
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
    Public ReadOnly Property MLCL() As UInt32
        Get
            Return m_MLCL.Value(0)
        End Get
    End Property


End Class
