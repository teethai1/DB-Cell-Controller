Imports XtraLibrary.SecsGem

Public Class S64F1
    Inherits SecsMessageBase

    Private m_List5 As SecsItemList
    Private m_DataID As SecsItemU2
    Private m_TUBEID_HEADER As SecsItemAscii
    Private m_TUBEID As SecsItemAscii
    Private m_LDPORTID_HEADER As SecsItemAscii
    Private m_LDPORTID As SecsItemU2

    Sub New()
        MyBase.New(64, 1, True)

        m_List5 = New SecsItemList("L5")

        m_DataID = New SecsItemU2("DataID", 1)
        m_TUBEID_HEADER = New SecsItemAscii("DNAME_TUBEID", "TUBEID")
        m_TUBEID = New SecsItemAscii("DVAL_TUBEID")
        m_LDPORTID_HEADER = New SecsItemAscii("DNAME_LDPORTID", "LDPORTID")
        m_LDPORTID = New SecsItemU2("DVAL_LDPORTID", 0)

        m_List5.AddItem(m_DataID)
        m_List5.AddItem(m_TUBEID_HEADER)
        m_List5.AddItem(m_TUBEID)
        m_List5.AddItem(m_LDPORTID_HEADER)
        m_List5.AddItem(m_LDPORTID)

        AddItem(m_List5)

    End Sub

    Public ReadOnly Property DataID() As UShort
        Get
            Return m_DataID.Value(0)
        End Get
    End Property

    Public ReadOnly Property TubeId() As String
        Get
            Return m_TUBEID.Value
        End Get
    End Property

    Public ReadOnly Property LoadPortId() As UShort
        Get
            Return m_LDPORTID.Value(0)
        End Get
    End Property

    ''' <summary>
    ''' FOR DEBUG
    ''' </summary>
    ''' <param name="tubeId"></param>
    ''' <remarks></remarks>
    Public Sub SubTubeID(ByVal tubeId As String)
        m_TUBEID.Value = tubeId
    End Sub

End Class
