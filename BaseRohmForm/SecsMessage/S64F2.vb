Imports XtraLibrary.SecsGem

Public Class S64F2
    Inherits SecsMessageBase

    Private m_List13 As SecsItemList
    Private m_DataID As SecsItemU2
    Private m_TUBEID_HEADER As SecsItemAscii
    Private m_TUBEID As SecsItemAscii
    Private m_RESULT_HEADER As SecsItemAscii
    Private m_RESULT As SecsItemAscii
    Private m_LOTID_HEADER As SecsItemAscii
    Private m_LOTID As SecsItemAscii
    Private m_MODULECOUNT_HEADER As SecsItemAscii
    Private m_ListN As SecsItemList
    Private m_TUBECOUNT_HEADER As SecsItemAscii
    Private m_TUBECOUNT As SecsItemU2
    Private m_MODULECOUNT2_HEADER As SecsItemAscii
    Private m_MODULECOUNT2 As SecsItemU2


    Sub New(ByVal s62f1 As S64F1)
        MyBase.New(64, 2, False)

        m_List13 = New SecsItemList("L13")

        m_DataID = New SecsItemU2("DATAID")
        m_DataID.Value(0) = s62f1.DataID

        m_TUBEID_HEADER = New SecsItemAscii("DNAME_1")
        m_TUBEID_HEADER.Value = "TUBEID"

        m_TUBEID = New SecsItemAscii("DVAL_1")
        m_TUBEID.Value = s62f1.TubeId

        m_RESULT_HEADER = New SecsItemAscii("DVNAME_2")
        m_RESULT_HEADER.Value = "RESULT"

        m_RESULT = New SecsItemAscii("DVAL_2")
        m_RESULT.Value = ""

        m_LOTID_HEADER = New SecsItemAscii("DNAME_3")
        m_LOTID_HEADER.Value = "LOTID"

        m_LOTID = New SecsItemAscii("DVAL_3") 'need to be set

        m_MODULECOUNT_HEADER = New SecsItemAscii("DNAME_4")
        m_MODULECOUNT_HEADER.Value = "MODULEID"

        m_ListN = New SecsItemList("MODULEn")

        m_TUBECOUNT_HEADER = New SecsItemAscii("DNAME_5")
        m_TUBECOUNT_HEADER.Value = "TUBECOUNT"

        m_TUBECOUNT = New SecsItemU2("DVAL_5") 'fixed 1
        m_TUBECOUNT.Value(0) = 1

        m_MODULECOUNT2_HEADER = New SecsItemAscii("DNAME_6")
        m_MODULECOUNT2_HEADER.Value = "MODULECOUNT"

        m_MODULECOUNT2 = New SecsItemU2("DVAL_6") 'neet to be set

        m_List13.AddItem(m_DataID)
        m_List13.AddItem(m_TUBEID_HEADER)
        m_List13.AddItem(m_TUBEID)
        m_List13.AddItem(m_RESULT_HEADER)
        m_List13.AddItem(m_RESULT)
        m_List13.AddItem(m_LOTID_HEADER)
        m_List13.AddItem(m_LOTID)
        m_List13.AddItem(m_MODULECOUNT_HEADER)
        m_List13.AddItem(m_ListN)
        m_List13.AddItem(m_TUBECOUNT_HEADER)
        m_List13.AddItem(m_TUBECOUNT)
        m_List13.AddItem(m_MODULECOUNT2_HEADER)
        m_List13.AddItem(m_MODULECOUNT2)

        AddItem(m_List13)

    End Sub

    Public Property LotId() As String
        Get
            Return m_LOTID.Value
        End Get
        Set(ByVal value As String)
            m_LOTID.Value = value
        End Set
    End Property

    Public ReadOnly Property TubeId() As String
        Get
            Return m_TUBEID.Value
        End Get
    End Property

    Public Sub AddModule(ByVal moduleSerialNo As String)
        Dim item As SecsItemAscii = New SecsItemAscii("A")
        item.Value = moduleSerialNo
        m_ListN.AddItem(item)
        m_MODULECOUNT2.Value(0) = CType(m_ListN.Value.Count, UShort)
    End Sub

    Public ReadOnly Property ModuleCount() As Integer
        Get
            Return m_ListN.Value.Count
        End Get
    End Property

    ''' <summary>
    ''' result of IDREAD [OK/NG/WAIT]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Result() As String
        Get
            Return m_RESULT.Value
        End Get
        Set(ByVal value As String)
            m_RESULT.Value = value
        End Set
    End Property

End Class
