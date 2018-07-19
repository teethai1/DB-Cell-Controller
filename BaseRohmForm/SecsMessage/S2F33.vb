Imports XtraLibrary.SecsGem

Public Class S2F33                                          '160722 \783  common secs data type
    Inherits SecsMessageBase

    Private m_L2 As SecsItemList
    'Private m_DataId As SecsItemU4

    Private m_La As SecsItemList

    'Comment: a=0 means delete all reports and event links, b=0 means delete the RPTID type and its event links

    Public Sub New(ByVal DataID As String)
        MyBase.New(2, 33, True)
        Dim SecsData As New SecsDataType
        m_L2 = New SecsItemList("L2")
        Me.AddItem(m_L2)
        'If SecsData.DATAID.GetType.Name = "UInt32" Then
        '    Dim m_DataId As New SecsItemU4("DATAID", CUInt(DataID))
        '    m_L2.AddItem(m_DataId)
        'ElseIf SecsData.DATAID.GetType.Name = "UInt16" Then
        '    Dim m_DataId As New SecsItemU2("DATAID", CUShort(DataID))
        '    m_L2.AddItem(m_DataId)
        'ElseIf SecsData.DATAID.GetType.Name = "Byte" Then
        '    Dim m_DataId As New SecsItemU1("DATAID", CByte(DataID))
        '    m_L2.AddItem(m_DataId)
        'Else
        '    Dim m_DataId As New SecsItemU1("DATAID", CByte(DataID))
        '    m_L2.AddItem(m_DataId)
        'End If


        If SecsData.DATAID = SecsDataType.DATAIDType.U4 Then
            Dim m_DataId As New SecsItemU4("DATAID", CUInt(DataID))
            m_L2.AddItem(m_DataId)
        ElseIf SecsData.DATAID = SecsDataType.DATAIDType.U2 Then
            Dim m_DataId As New SecsItemU2("DATAID", CUShort(DataID))
            m_L2.AddItem(m_DataId)
        ElseIf SecsData.DATAID = SecsDataType.DATAIDType.U1 Then
            Dim m_DataId As New SecsItemU1("DATAID", CByte(DataID))
            m_L2.AddItem(m_DataId)
        Else
            Dim m_DataId As New SecsItemU2("DATAID", CUShort(DataID))
            m_L2.AddItem(m_DataId)
        End If

        m_La = New SecsItemList("La")
        m_L2.AddItem(m_La)

    End Sub

    Public Sub SetDeleteAllReport()
        m_La.Clear() 'a=0 means delete all reports and event links
    End Sub

    <Obsolete()> _
    Public Sub SetReportToDelete(ByVal ParamArray rptIdArray As UInt32())

        For Each rptId As UInt32 In rptIdArray
            Dim l2 As SecsItemList = New SecsItemList("L2")
            m_La.AddItem(l2)

            Dim rptIdItem As SecsItemU4 = New SecsItemU4("RPTID", rptId)
            l2.AddItem(rptIdItem)

            Dim lbItem As SecsItemList = New SecsItemList("Lb") 'b=0 means delete the RPTID type and its event links
            l2.AddItem(lbItem)

        Next

    End Sub

    Public Sub AddReport(ByVal rptId As UInt32, ByVal ParamArray vidArray As UInt32())

        Dim l2 As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2)

        Dim rptIdItem As SecsItemU4 = New SecsItemU4("RPTID", rptId)
        l2.AddItem(rptIdItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2.AddItem(lbItem)

        For Each vid As UInt32 In vidArray
            lbItem.AddItem(New SecsItemU4("VID", vid))
        Next

    End Sub



    Public Sub AddReport(ByVal rptId As UInt16, ByVal ParamArray vidArray As UInt32()) '160722 \783  common secs data type

        Dim l2 As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2)

        Dim rptIdItem As SecsItemU2 = New SecsItemU2("RPTID", rptId)
        l2.AddItem(rptIdItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2.AddItem(lbItem)

        For Each vid As UInt32 In vidArray
            lbItem.AddItem(New SecsItemU4("VID", vid))
        Next

    End Sub


    Public Sub AddReport(ByVal rptId As UInt16, ByVal ParamArray vidArray As UInt16())  '160722 \783  common secs data type

        Dim l2 As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2)

        Dim rptIdItem As SecsItemU2 = New SecsItemU2("RPTID", rptId)
        l2.AddItem(rptIdItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2.AddItem(lbItem)

        For Each vid As UInt16 In vidArray
            lbItem.AddItem(New SecsItemU2("VID", vid))
        Next

    End Sub


    Public Sub AddReport(ByVal rptId As UInt32, ByVal ParamArray vidArray As UInt16())  '160722 \783  common secs data type

        Dim l2 As SecsItemList = New SecsItemList("L2")
        m_La.AddItem(l2)

        Dim rptIdItem As SecsItemU4 = New SecsItemU4("RPTID", rptId)
        l2.AddItem(rptIdItem)

        Dim lbItem As SecsItemList = New SecsItemList("Lb")
        l2.AddItem(lbItem)

        For Each vid As UInt16 In vidArray
            lbItem.AddItem(New SecsItemU2("VID", vid))
        Next

    End Sub

End Class
