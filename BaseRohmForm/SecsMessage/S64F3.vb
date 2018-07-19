Imports XtraLibrary.SecsGem

Public Class S64F3
    Inherits SecsMessageBase

    'root item
    Private m_List7 As SecsItemList
    'item 1
    Private m_DataId As SecsItemU2
    'item 2
    Private m_HEADER_1 As SecsItemAscii
    'item 3
    Private m_GoodTubeInfo As SecsItemList
    'item of item 3
    Private m_GoodTubeId As SecsItemAscii
    Private m_GoodTubeChange As SecsItemU1
    Private m_GoodTubeBCDSelect As SecsItemU1
    'item 4
    Private m_HEADER_2 As SecsItemAscii
    'item 5 
    Private m_NGTubeInfo As SecsItemList

    Private m_NGTube_1_ID As SecsItemAscii
    Private m_NGTube_1_Change As SecsItemU1

    Private m_NGTube_2_ID As SecsItemAscii
    Private m_NGTube_2_Change As SecsItemU1

    Private m_NGTube_3_ID As SecsItemAscii
    Private m_NGTube_3_Change As SecsItemU1

    Private m_NGTube_4_ID As SecsItemAscii
    Private m_NGTube_4_Change As SecsItemU1

    Private m_NGTube_5_ID As SecsItemAscii
    Private m_NGTube_5_Change As SecsItemU1

    Private m_NGTube_6_ID As SecsItemAscii
    Private m_NGTube_6_Change As SecsItemU1

    'item 6
    Private m_HEADER_3 As SecsItemAscii
    'item 7
    Private m_PurgBin As SecsItemAscii


    Public Sub New()
        MyBase.New(64, 3, True)

        m_List7 = New SecsItemList("L7")
        AddItem(m_List7)

        'item 1
        m_DataId = New SecsItemU2("DATAID")
        m_List7.AddItem(m_DataId)
        'item 2
        m_HEADER_1 = New SecsItemAscii("DVNAME1")
        m_List7.AddItem(m_HEADER_1)
        'item 3
        m_GoodTubeInfo = New SecsItemList("DVVAL1")
        m_List7.AddItem(m_GoodTubeInfo)
        'item of item 3
        m_GoodTubeId = New SecsItemAscii("DVVAL1-1")
        m_GoodTubeInfo.AddItem(m_GoodTubeId)
        m_GoodTubeChange = New SecsItemU1("DVVAL1-2")
        m_GoodTubeInfo.AddItem(m_GoodTubeChange)
        m_GoodTubeBCDSelect = New SecsItemU1("DVVAL1-3")
        m_GoodTubeInfo.AddItem(m_GoodTubeBCDSelect)
        'item 4
        m_HEADER_2 = New SecsItemAscii("DVNAME2")
        m_List7.AddItem(m_HEADER_2)
        'item 5 
        m_NGTubeInfo = New SecsItemList("DVVAL2")
        m_List7.AddItem(m_NGTubeInfo)

        Dim l2 As SecsItemList

        l2 = New SecsItemList("L2")
        m_NGTubeInfo.AddItem(l2)
        m_NGTube_1_ID = New SecsItemAscii("NG_1_TUBE_ID")
        l2.AddItem(m_NGTube_1_ID)
        m_NGTube_1_Change = New SecsItemU1("NG_1_TUBE_CHANGE")
        l2.AddItem(m_NGTube_1_Change)

        l2 = New SecsItemList("L2")
        m_NGTubeInfo.AddItem(l2)
        m_NGTube_2_ID = New SecsItemAscii("NG_2_TUBE_ID")
        l2.AddItem(m_NGTube_2_ID)
        m_NGTube_2_Change = New SecsItemU1("NG_2_TUBE_CHANGE")
        l2.AddItem(m_NGTube_2_Change)

        l2 = New SecsItemList("L2")
        m_NGTubeInfo.AddItem(l2)
        m_NGTube_3_ID = New SecsItemAscii("NG_3_TUBE_ID")
        l2.AddItem(m_NGTube_3_ID)
        m_NGTube_3_Change = New SecsItemU1("NG_3_TUBE_CHANGE")
        l2.AddItem(m_NGTube_3_Change)

        l2 = New SecsItemList("L2")
        m_NGTubeInfo.AddItem(l2)
        m_NGTube_4_ID = New SecsItemAscii("NG_4_TUBE_ID")
        l2.AddItem(m_NGTube_4_ID)
        m_NGTube_4_Change = New SecsItemU1("NG_4_TUBE_CHANGE")
        l2.AddItem(m_NGTube_4_Change)

        l2 = New SecsItemList("L2")
        m_NGTubeInfo.AddItem(l2)
        m_NGTube_5_ID = New SecsItemAscii("NG_5_TUBE_ID")
        l2.AddItem(m_NGTube_5_ID)
        m_NGTube_5_Change = New SecsItemU1("NG_5_TUBE_CHANGE")
        l2.AddItem(m_NGTube_5_Change)

        l2 = New SecsItemList("L2")
        m_NGTubeInfo.AddItem(l2)
        m_NGTube_6_ID = New SecsItemAscii("NG_6_TUBE_ID")
        l2.AddItem(m_NGTube_6_ID)
        m_NGTube_6_Change = New SecsItemU1("NG_6_TUBE_CHANGE")
        l2.AddItem(m_NGTube_6_Change)

        'item 6
        m_HEADER_3 = New SecsItemAscii("DVNAME3")
        m_List7.AddItem(m_HEADER_3)
        'item 7
        m_PurgBin = New SecsItemAscii("DVVAL3")
        m_List7.AddItem(m_PurgBin)

    End Sub

    Public ReadOnly Property GoodTubeId() As String
        Get
            Return m_GoodTubeId.Value
        End Get
    End Property

    Public ReadOnly Property GoodTubeChange() As Byte
        Get
            Return m_GoodTubeChange.Value(0)
        End Get
    End Property

    Public ReadOnly Property GoodTubeBCDSelect() As Byte
        Get
            Return m_GoodTubeBCDSelect.Value(0)
        End Get
    End Property

    Public ReadOnly Property NGTube_1_ID() As String
        Get
            Return m_NGTube_1_ID.Value
        End Get
    End Property

    Public ReadOnly Property NGTube_1_Change() As Byte
        Get
            Return m_NGTube_1_Change.Value(0)
        End Get
    End Property

    Public ReadOnly Property NGTube_2_ID() As String
        Get
            Return m_NGTube_2_ID.Value
        End Get
    End Property

    Public ReadOnly Property NGTube_2_Change() As Byte
        Get
            Return m_NGTube_2_Change.Value(0)
        End Get
    End Property

    Public ReadOnly Property NGTube_3_ID() As String
        Get
            Return m_NGTube_3_ID.Value
        End Get
    End Property

    Public ReadOnly Property NGTube_3_Change() As Byte
        Get
            Return m_NGTube_3_Change.Value(0)
        End Get
    End Property

    Public ReadOnly Property NGTube_4_ID() As String
        Get
            Return m_NGTube_4_ID.Value
        End Get
    End Property

    Public ReadOnly Property NGTube_4_Change() As Byte
        Get
            Return m_NGTube_4_Change.Value(0)
        End Get
    End Property

    Public ReadOnly Property NGTube_5_ID() As String
        Get
            Return m_NGTube_5_ID.Value
        End Get
    End Property

    Public ReadOnly Property NGTube_5_Change() As Byte
        Get
            Return m_NGTube_5_Change.Value(0)
        End Get
    End Property

    Public ReadOnly Property NGTube_6_ID() As String
        Get
            Return m_NGTube_6_ID.Value
        End Get
    End Property

    Public ReadOnly Property NGTube_6_Change() As Byte
        Get
            Return m_NGTube_6_Change.Value(0)
        End Get
    End Property

End Class
