Imports XtraLibrary.SecsGem

Public Class S64F12
    Inherits SecsMessageBase

    Private m_RootItem As SecsItemList

    Private m_DataId As SecsItemU2
    Private m_DvName1 As SecsItemAscii
    Private m_Result As SecsItemAscii
    Private m_DvName2 As SecsItemAscii
    Private m_TubeId As SecsItemAscii

    Public Sub New(ByVal req As S64F11)
        MyBase.New(64, 12, False)

        m_RootItem = New SecsItemList("L3")
        AddItem(m_RootItem)

        m_DataId = New SecsItemU2("DATAID", New UShort() {req.DataId})
        m_DvName1 = New SecsItemAscii("DVNAME1", "RESULT")
        m_Result = New SecsItemAscii("DVVAL1")
        m_DvName2 = New SecsItemAscii("DVNAME2", "TUBEID")
        m_TubeId = New SecsItemAscii("DVVAL2")

        m_RootItem.AddItem(m_DataId)
        m_RootItem.AddItem(m_DvName1)
        m_RootItem.AddItem(m_Result)
        m_RootItem.AddItem(m_DvName2)
        m_RootItem.AddItem(m_TubeId)

    End Sub

    Public Property DataId() As UShort
        Get
            Return m_DataId.Value(0)
        End Get
        Set(ByVal value As UShort)
            m_DataId.Value(0) = value
        End Set
    End Property


    Public Property Result() As String
        Get
            Return m_Result.Value
        End Get
        Set(ByVal value As String)
            m_Result.Value = value
        End Set
    End Property

    Public Property TubeId() As String
        Get
            Return m_TubeId.Value
        End Get
        Set(ByVal value As String)
            m_TubeId.Value = value
        End Set
    End Property

End Class
