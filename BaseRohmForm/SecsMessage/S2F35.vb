Imports XtraLibrary.SecsGem

Public Class S2F35                        '160722 \783  common secs data type
    Inherits SecsMessageBase

    Private m_L2 As SecsItemList
    'Private m_DataId As SecsItemU4
    Private m_La As SecsItemList

    Public Sub New(ByVal DataID As String)
        MyBase.New(2, 35, True)
        Dim SecsData As New SecsDataType
        m_L2 = New SecsItemList("L2")
        Me.AddItem(m_L2)
        'If SecsData.DATAID.GetType.Name = "UInt32" Then
        '    Dim m_DataId As New SecsItemU4("DATAID", DataID)
        '    m_L2.AddItem(m_DataId)
        'Else
        '    Dim m_DataId As New SecsItemU2("DATAID", DataID)
        '    m_L2.AddItem(m_DataId)

        'End If

            If SecsData.DATAID = SecsDataType.DATAIDType.U4 Then
                Dim m_DataId As New SecsItemU4("DATAID", CUInt(DataID))
                m_L2.AddItem(m_DataId)
            ElseIf SecsData.DATAID = SecsDataType.DATAIDType.U2 Then
                Dim m_DataId As New SecsItemU2("DATAID", CUShort(DataID))
                m_L2.AddItem(m_DataId)
            ElseIf SecsData.DATAID = SecsDataType.DATAIDType.U1 Then
                Dim m_DataId As New SecsItemU1("DATAID", CByte(CUShort(DataID)))
                m_L2.AddItem(m_DataId)
            Else
                Dim m_DataId As New SecsItemU2("DATAID", CUShort(DataID))
                m_L2.AddItem(m_DataId)
            End If

        m_La = New SecsItemList("La")
        m_L2.AddItem(m_La)
    End Sub

    Public Sub AddLink(ByVal ceid As UInt32, ByVal ParamArray rptidArray As UInt32())
        Dim l2Item As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2Item)

        Dim ceidItem As SecsItemU4 = New SecsItemU4("CEID", ceid)
        l2Item.AddItem(ceidItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2Item.AddItem(lbItem)

        Dim rptidItem As SecsItemU4
        For Each rptid As UInt32 In rptidArray
            rptidItem = New SecsItemU4("RPTID", rptid)
            lbItem.AddItem(rptidItem)
        Next
    End Sub

    Public Sub AddLink(ByVal ceid As UInt16, ByVal ParamArray rptidArray As UInt32())
        Dim l2Item As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2Item)

        Dim ceidItem As SecsItemU2 = New SecsItemU2("CEID", ceid)
        l2Item.AddItem(ceidItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2Item.AddItem(lbItem)

        Dim rptidItem As SecsItemU4
        For Each rptid As UInt32 In rptidArray
            rptidItem = New SecsItemU4("RPTID", rptid)
            lbItem.AddItem(rptidItem)
        Next
    End Sub

    Public Sub AddLink(ByVal ceid As UInt16, ByVal ParamArray rptidArray As UInt16())
        Dim l2Item As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2Item)

        Dim ceidItem As SecsItemU2 = New SecsItemU2("CEID", ceid)
        l2Item.AddItem(ceidItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2Item.AddItem(lbItem)

        Dim rptidItem As SecsItemU2
        For Each rptid As UInt16 In rptidArray
            rptidItem = New SecsItemU2("RPTID", rptid)
            lbItem.AddItem(rptidItem)
        Next
    End Sub

    Public Sub AddLink(ByVal ceid As UInt32, ByVal ParamArray rptidArray As UInt16())
        Dim l2Item As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2Item)

        Dim ceidItem As SecsItemU4 = New SecsItemU4("CEID", ceid)
        l2Item.AddItem(ceidItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2Item.AddItem(lbItem)

        Dim rptidItem As SecsItemU2
        For Each rptid As UInt16 In rptidArray
            rptidItem = New SecsItemU2("RPTID", rptid)
            lbItem.AddItem(rptidItem)
        Next
    End Sub

    Public Sub AddLinkCanon(ByVal ceid As UInt32, ByVal ParamArray rptidArray As UInt16())
        Dim l2Item As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2Item)

        Dim ceidItem As SecsItemU4 = New SecsItemU4("CEID", ceid)
        l2Item.AddItem(ceidItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2Item.AddItem(lbItem)

        Dim rptidItem As SecsItemU2
        For Each rptid As UInt16 In rptidArray
            rptidItem = New SecsItemU2("RPTID", rptid)
            lbItem.AddItem(rptidItem)
        Next
    End Sub

    Public Sub DisassosiateLink(ByVal ceid As UInt32)

        Dim l2Item As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2Item)

        Dim ceidItem As SecsItemU4 = New SecsItemU4("CEID")
        l2Item.AddItem(ceidItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2Item.AddItem(lbItem)

    End Sub
    Public Sub DisassosiateLink(ByVal ceid As UInt16)

        Dim l2Item As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2Item)

        Dim ceidItem As SecsItemU2 = New SecsItemU2("CEID")
        l2Item.AddItem(ceidItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2Item.AddItem(lbItem)

    End Sub
End Class
