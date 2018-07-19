Imports XtraLibrary.SecsGem

Public Class S64F11
    Inherits SecsMessageBase

    Private m_RootItem As SecsItemList

    Private m_DataId As SecsItemU2
    Private m_DvName1 As SecsItemAscii
    Private m_WorkId As SecsItemAscii

    Public Sub New()
        MyBase.New(64, 11, True)

        m_RootItem = New SecsItemList("L3")
        AddItem(m_RootItem)

        m_DataId = New SecsItemU2("DATAID")
        m_DvName1 = New SecsItemAscii("DVNAME1")
        m_WorkId = New SecsItemAscii("DVVAL1")

        m_RootItem.AddItem(m_DataId)
        m_RootItem.AddItem(m_DvName1)
        m_RootItem.AddItem(m_WorkId)

    End Sub


    Public ReadOnly Property DataId() As UShort
        Get
            Return m_DataId.Value(0)
        End Get
    End Property

    Public ReadOnly Property WorkId() As String
        Get
            Return m_WorkId.Value
        End Get
    End Property

End Class
