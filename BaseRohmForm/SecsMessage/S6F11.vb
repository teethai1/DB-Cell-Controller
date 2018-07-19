'Option Strict Off                             '160722 \783  common secs data type
Imports XtraLibrary.SecsGem

Public Class S6F11
    Inherits SecsMessageBase

    Private m_L3 As SecsItemList
    'Private m_DATAID As SecsItemU4
    Private m_CEID As New SecsDataType
    Private m_CEIDU4 As SecsItemU4
    Private m_CEIDU2 As SecsItemU2
    Private m_CEIDU1 As SecsItemU1
    Private m_La As SecsItemList

    Public Sub New()
        MyBase.New(6, 11, True)

        m_L3 = New SecsItemList("L3")
        Me.AddItem(m_L3)
        Dim SecsData As New SecsDataType            '160722 \783  common secs data type
        'If SecsData.DATAID.GetType.Name = "UInt32" Then
        '    Dim m_DataId As New SecsItemU4("DATAID")
        '    m_L3.AddItem(m_DataId)
        'ElseIf SecsData.DATAID.GetType.Name = "UInt16" Then
        '    Dim m_DataId As New SecsItemU2("DATAID")
        '    m_L3.AddItem(m_DataId)
        'ElseIf SecsData.DATAID.GetType.Name = "Byte" Then
        '    Dim m_DataId As New SecsItemU1("DATAID")
        '    m_L3.AddItem(m_DataId)
        'End If

        'm_DATAID = New SecsItemU4("DATAID")
        'm_L3.AddItem(m_DATAID)


        If SecsData.DATAID = SecsDataType.DATAIDType.U4 Then
            Dim m_DataId As New SecsItemU4("DATAID")
            m_L3.AddItem(m_DataId)
        ElseIf SecsData.DATAID = SecsDataType.DATAIDType.U2 Then
            Dim m_DataId As New SecsItemU2("DATAID")
            m_L3.AddItem(m_DataId)
        ElseIf SecsData.DATAID = SecsDataType.DATAIDType.U1 Then
            Dim m_DataId As New SecsItemU1("DATAID")
            m_L3.AddItem(m_DataId)
        Else
            Dim m_DataId As New SecsItemU2("DATAID")
            m_L3.AddItem(m_DataId)
        End If



        If m_CEID.CEID.GetType.Name = "UInt32" Then
            m_CEIDU4 = New SecsItemU4("CEID")
            m_L3.AddItem(m_CEIDU4)
        ElseIf m_CEID.CEID.GetType.Name = "UInt16" Then
            m_CEIDU2 = New SecsItemU2("CEID")           's6f11 will throw error if type not U2
            m_L3.AddItem(m_CEIDU2)

            'ElseIf m_CEID.CEID.GetType.Name = "Byte" Then
            '    m_CEIDU1 = New SecsItemU1("CEID")           's6f11 will throw error if type not U2
            '    m_L3.AddItem(m_CEIDU1)
        Else
            m_CEIDU4 = New SecsItemU4("CEID")
            m_L3.AddItem(m_CEIDU4)
        End If
       
        'La contains L2 and L2 contain "Report "
        'and "List of SV value mapped to Defined report"
        m_La = New SecsItemList("La")
        m_L3.AddItem(m_La)

    End Sub

    Public ReadOnly Property CEID() As String
        Get
            Dim Value As String
         

                If m_CEID.CEID.GetType.Name = "UInt16" Then
                    Value = m_CEIDU2.Value(0).ToString

                'ElseIf m_CEID.CEID.GetType.Name = "Byte" Then
                '    Value = m_CEIDU1.Value(0).ToString

                Else
                    Value = m_CEIDU4.Value(0).ToString
                End If

                Return Value
        End Get
    End Property


    Public Sub ApplyStatusVariableValue(ByVal eq As Equipment, ByVal reportDic As Dictionary(Of String, SecsDataType)) '160722 \783  common secs data type

        Dim secsItem_Lb As SecsItemList
        Dim reportID As String = ""
        Dim definedReport As New SecsDataType
        Dim secsItem_V As SecsItem
        Try

            For Each l2 As SecsItemList In m_La.Value
                If My.Settings.MCType = "Canon-D10R" Then
                    reportID = CType(l2.Value(0), SecsItemU2).Value(0).ToString
                Else
                    'reportID = l2.Value(0).Value(0)         ' if Option Strict Off 
                    If definedReport.RPTID.GetType.GetGenericArguments()(0).Name = "UInt32" Then
                        reportID = CType(l2.Value(0), SecsItemU4).Value(0).ToString
                    Else
                        reportID = CType(l2.Value(0), SecsItemU2).Value(0).ToString
                    End If
                End If



                If reportDic.ContainsKey(reportID) Then
                    'get DefinedReport object from dictionary
                    definedReport = reportDic(reportID)

                    secsItem_Lb = CType(l2.Value(1), SecsItemList)
                    For i As Integer = 0 To secsItem_Lb.Value.Count - 1

                        secsItem_V = secsItem_Lb.Value(i)

                        Select Case definedReport.VID(i)   '160722 \783  common secs data type
                            Case 151126269 'TotalGood 2100HS
                                eq.GoodPcs = CStr(CType(secsItem_V, SecsItemU4).Value(0))
                            Case 2031 'Current Status Change 2100HS
                                eq.EQStatus = CType(CType(secsItem_V, SecsItemU1).Value(0), EquipmentStateEsec)
                            Case 2009 'RecipeChanged 2100HS ,2009SSI
                                eq.CurrentPPID = CStr(CType(secsItem_V, SecsItemAscii).Value)
                            Case 110316 'TotalGood 2009SSI
                                eq.GoodPcs = CStr(CType(secsItem_V, SecsItemI4).Value(0))
                            Case 111634 'Current State Change 2009SSI
                                eq.EQStatus = CType(CType(secsItem_V, SecsItemU1).Value(0), EquipmentStateEsec)
                                'Canon
                            Case 1002 'Current State
                                eq.EQStatusCanon = CType(CType(secsItem_V, SecsItemU1).Value(0), EquipmentStateCanon)
                            Case 112   'Mount Number (GoodPCS)
                                eq.GoodPcs = CStr(CType(secsItem_V, SecsItemI4).Value(0))
                            Case 1011 'RecipeName
                                eq.CurrentPPID = CStr(CType(secsItem_V, SecsItemAscii).Value)
                            Case 1008 'ALarmID
                                eq.AlarmID = CStr(CType(secsItem_V, SecsItemU4).Value(0))
                            Case 1007 'AlarmCode
                                Dim DecToBinary As String = Convert.ToString(CType(secsItem_V, SecsItemBinary).Value(0), 2)
                                If DecToBinary.Length <= 7 Then   'AlarmClear
                                    eq.AlarmState = AlarmState.AlarmClear
                                Else 'AlarmSet
                                    eq.AlarmState = AlarmState.AlarmSet
                                End If
                        End Select
                    Next
                End If
            Next
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "ApplyStatusVariableValue()")
        End Try


    End Sub

End Class
