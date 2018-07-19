Imports XtraLibrary.SecsGem

Public Class S6F3
    Inherits SecsMessageBase

    Private m_RootItem As SecsItemList

    'Private m_DataId As SecsItemU2

    Private m_CEID As SecsItemU2

    Private m_DvName1 As SecsItemAscii
    Private m_ModuleId As SecsItemAscii

    Private m_DvName2 As SecsItemAscii
    Private m_LotId As SecsItemAscii

    Private m_DvName3 As SecsItemAscii
    Private m_Result As SecsItemList

    Private m_DvName3_1 As SecsItemAscii
    Private m_DvVal3_1 As SecsItemList

    Private m_DvName3_2 As SecsItemAscii
    Private m_DvVal3_2 As SecsItemList

    Private m_MachinePass As SecsItemU1
    Private m_MachineBin As SecsItemU1
    Private m_MachineValid As SecsItemU1

    Private m_DvName4 As SecsItemAscii
    Private m_SupplyTubeId As SecsItemAscii

    Private m_DvName5 As SecsItemAscii
    Private m_OutputTubeId As SecsItemAscii

    Private m_DvName6 As SecsItemAscii
    Private m_PortId As SecsItemU1

    Private m_DvName7 As SecsItemAscii
    Private m_DvVal7 As SecsItemList

    Private m_StdfLotId As SecsItemAscii
    Private m_StdfEnd As SecsItemU1

    Private m_DvName8 As SecsItemAscii
    Private m_StartTime As SecsItemAscii

    Public Sub New()
        MyBase.New(6, 3, True)

        m_RootItem = New SecsItemList("L18")

        AddItem(m_RootItem)

        '1.) data id
        Dim SecsData As New SecsDataType            '160722 \783  common secs data type
        If SecsData.DATAID.GetType.Name = "UInt32" Then
            Dim m_DataId As New SecsItemU4("DATAID")
            m_RootItem.AddItem(m_DataId)
        Else
            Dim m_DataId As New SecsItemU2("DATAID")
            m_RootItem.AddItem(m_DataId)

        End If
        'm_DataId = New SecsItemU2("DATAID")
        'm_RootItem.AddItem(m_DataId)


        '2.) ceid
        m_CEID = New SecsItemU2("CEID")
        m_RootItem.AddItem(m_CEID)

        '3.) dvname module id
        m_DvName1 = New SecsItemAscii("DVNAME1")
        m_RootItem.AddItem(m_DvName1)
        '4.) module id
        m_ModuleId = New SecsItemAscii("DVVAL1")
        m_RootItem.AddItem(m_ModuleId)

        '5.) dvname lotid
        m_DvName2 = New SecsItemAscii("DVNAME2")
        m_RootItem.AddItem(m_DvName2)
        '6.) lotid
        m_LotId = New SecsItemAscii("DVVAL2")
        m_RootItem.AddItem(m_LotId)


        '********************************************
        '7.) dvname result
        m_DvName3 = New SecsItemAscii("DVNAME3")
        m_RootItem.AddItem(m_DvName3)
        '8.) list 4
        m_Result = New SecsItemList("List")
        m_RootItem.AddItem(m_Result)

        m_DvName3_1 = New SecsItemAscii("DVNAME3-1")
        m_Result.AddItem(m_DvName3_1)
        m_DvVal3_1 = New SecsItemList("List")
        m_Result.AddItem(m_DvVal3_1)

        m_DvName3_2 = New SecsItemAscii("DVNAME3-2")
        m_Result.AddItem(m_DvName3_2)
        m_DvVal3_2 = New SecsItemList("List")
        m_Result.AddItem(m_DvVal3_2)

        m_MachinePass = New SecsItemU1("PASS")
        m_DvVal3_2.AddItem(m_MachinePass)
        m_MachineBin = New SecsItemU1("BIN")
        m_DvVal3_2.AddItem(m_MachineBin)
        m_MachineValid = New SecsItemU1("VALID")
        m_DvVal3_2.AddItem(m_MachineValid)
        '********************************************

        '9.) dvname supply tube
        m_DvName4 = New SecsItemAscii("DVNAME4")
        m_RootItem.AddItem(m_DvName4)
        '10.) supply tube id
        m_SupplyTubeId = New SecsItemAscii("DVVAL4")
        m_RootItem.AddItem(m_SupplyTubeId)

        '11.) dvname output tube
        m_DvName5 = New SecsItemAscii("DVNAME5")
        m_RootItem.AddItem(m_DvName5)
        '12.) output tube id
        m_OutputTubeId = New SecsItemAscii("DVVAL5")
        m_RootItem.AddItem(m_OutputTubeId)

        '13.) dvname ulport ud
        m_DvName6 = New SecsItemAscii("DVNAME6")
        m_RootItem.AddItem(m_DvName6)
        '14.) ulport id
        m_PortId = New SecsItemU1("DVVAL6")
        m_RootItem.AddItem(m_PortId)

        '********************************************
        '15.) dvname stdf
        m_DvName7 = New SecsItemAscii("DVNAME7")
        m_RootItem.AddItem(m_DvName7)
        '16.) list 2
        m_DvVal7 = New SecsItemList("List")
        m_RootItem.AddItem(m_DvVal7)

        m_StdfLotId = New SecsItemAscii("STDFLOTEND")
        m_DvVal7.AddItem(m_StdfLotId)
        m_StdfEnd = New SecsItemU1("END")
        m_DvVal7.AddItem(m_StdfEnd)
        '********************************************

        m_DvName8 = New SecsItemAscii("DVNAME8")
        m_RootItem.AddItem(m_DvName8)
        m_StartTime = New SecsItemAscii("DVVAL8")
        m_RootItem.AddItem(m_StartTime)

    End Sub

    Public ReadOnly Property ModuleId() As String
        Get
            Return m_ModuleId.Value
        End Get
    End Property

    Public ReadOnly Property LotId() As String
        Get
            Return m_LotId.Value
        End Get
    End Property

    'Private m_DvName3_1 As SecsItemAscii
    'Private m_DvVal3_1 As SecsItemList

    ''' <summary>
    ''' (0,1) - Pass/Fail/Unit Disabled
    ''' </summary>
    ''' <value>0 - Pass, 1 - Fail</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MachinePass() As Byte
        Get
            Return m_MachinePass.Value(0)
        End Get
    End Property

    Public ReadOnly Property MachineBin() As Byte
        Get
            Return m_MachineBin.Value(0)
        End Get
    End Property

    Public ReadOnly Property MachineValid() As Byte
        Get
            Return m_MachineValid.Value(0)
        End Get
    End Property

    Public ReadOnly Property SupplyTubeId() As String
        Get
            Return m_SupplyTubeId.Value
        End Get
    End Property

    Public ReadOnly Property OutputTubeId() As String
        Get
            Return m_OutputTubeId.Value
        End Get
    End Property

    Public ReadOnly Property PortId() As Byte
        Get
            Return m_PortId.Value(0)
        End Get
    End Property

    Public ReadOnly Property StdfLotId() As String
        Get
            Return m_StdfLotId.Value
        End Get
    End Property

    Public ReadOnly Property StdfEnd() As Byte
        Get
            Return m_StdfEnd.Value(0)
        End Get
    End Property

    Public ReadOnly Property StartTime() As String
        Get
            Return m_StartTime.Value
        End Get
    End Property

    Public Function WorkIsPassed() As Boolean

        'testn, TEST = 0 - 255(0, 1, 5, 9, 10, 90, 99)
        '  0 :Pass (Active Head)
        '  1 :Fail (Active Head)
        '  5 :Skip (Active Head, but Skipped by previous Head Fail)
        '  8 :Communication Error (Active Head)
        '  9 :Unknown (Unexpected Error, fail to get Pass/Fail Info)
        '  10:Unrelated Head (Active Head: Parallel Head etc.)
        '  99:Disabled Head (Disconnected Head)
        '  Other: (Reserved)

        'Machine have only { Passed :0, Failed :1 }
        If m_MachinePass.Value(0) <> 0 Then
            Return False
        End If

        Dim testn As SecsItemU1
        Dim testList As SecsItemList
        Dim testResult As Byte
        For Each item As SecsItem In m_DvVal3_1.Value
            If TypeOf item Is SecsItemList Then
                testList = CType(item, SecsItemList)
                If testList.Value.Count = 4 Then
                    testn = CType(testList.Value(1), SecsItemU1)
                    testResult = testn.Value(0)

                    If testResult = 5 OrElse testResult = 10 OrElse testResult = 99 Then
                        Continue For
                    End If

                    If testResult <> 0 Then
                        Return False
                    End If
                Else
                    'throw error data format
                    Return False
                End If
            Else
                'throw error data format
                Return False
            End If
        Next
        Return True
    End Function

End Class
