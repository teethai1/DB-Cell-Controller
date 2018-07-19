Imports XtraLibrary.SecsGem

''' <summary>
''' For Tube-Shoot to Box
''' </summary>
''' <remarks></remarks>
Public Class S64F9
    Inherits SecsMessageBase

    Private m_RootItem As SecsItemList

    Private m_DataId As SecsItemU2
    Private m_DvName1 As SecsItemAscii
    Private m_GoodTube As SecsItemAscii
    Private m_DvName2 As SecsItemAscii
    Private m_UnloadPortId As SecsItemU2
    Private m_DvName3 As SecsItemAscii
    Private m_PortEnd As SecsItemU1
    Private m_DvName4 As SecsItemAscii
    Private m_Modules As SecsItemList

    Public Sub New()
        MyBase.New(64, 9, True)

        m_RootItem = New SecsItemList("L7")
        AddItem(m_RootItem)

        m_DataId = New SecsItemU2("DATAID")

        m_DvName1 = New SecsItemAscii("DVNAME1")
        m_GoodTube = New SecsItemAscii("DVVAL1")

        m_DvName2 = New SecsItemAscii("DVNAME2")
        m_UnloadPortId = New SecsItemU2("DVVAL2")

        m_DvName3 = New SecsItemAscii("DVNAME3")
        m_PortEnd = New SecsItemU1("DVVAL3")

        m_DvName4 = New SecsItemAscii("DVNAME4")
        m_Modules = New SecsItemList("DVVAL4")

        m_RootItem.AddItem(m_DataId)
        m_RootItem.AddItem(m_DvName1)
        m_RootItem.AddItem(m_GoodTube)
        m_RootItem.AddItem(m_DvName2)
        m_RootItem.AddItem(m_UnloadPortId)
        m_RootItem.AddItem(m_DvName3)
        m_RootItem.AddItem(m_PortEnd)
        m_RootItem.AddItem(m_DvName4)
        m_RootItem.AddItem(m_Modules)

    End Sub

    Public ReadOnly Property GoodTubeId() As String
        Get
            Return m_GoodTube.Value
        End Get
    End Property

    Public ReadOnly Property UnloadPortId() As UShort
        Get
            Return m_UnloadPortId.Value(0)
        End Get
    End Property

    Public ReadOnly Property PortEnd() As Byte
        Get
            Return m_PortEnd.Value(0)
        End Get
    End Property

    Public Function GetModules() As String()
        Dim list As List(Of String) = New List(Of String)
        For Each item As SecsItem In m_Modules.Value
            If TypeOf item Is SecsItemAscii Then
                list.Add(CType(item, SecsItemAscii).Value)
            End If
        Next
        Dim ret As String() = list.ToArray()
        list.Clear()
        list = Nothing
        Return ret
    End Function

    ''' <summary>
    ''' FOR DEBUG
    ''' </summary>
    ''' <param name="tubeID"></param>
    ''' <remarks></remarks>
    Public Sub SetTubeID(ByVal tubeID As String)
        m_GoodTube.Value = tubeID
    End Sub

    ''' <summary>
    ''' FOR DEBUG
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddModule(ByVal serialNo As String)
        Dim item As SecsItemAscii = New SecsItemAscii("VAL")
        item.Value = serialNo
        m_Modules.AddItem(item)
    End Sub

    Public Function CountModule() As Integer
        Return m_Modules.Value.Count
    End Function

End Class
