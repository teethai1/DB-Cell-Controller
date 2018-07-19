Imports XtraLibrary.SecsGem

Public Class S1F14E
    Inherits SecsMessageBase


    Private m_COMMACK As SecsItemBinary
    Private m_MDLN As SecsItemAscii
    Private m_SOFTREV As SecsItemAscii

    Public Sub New()
        MyBase.New(1, 14, False)

        Dim l2_first As SecsItemList = New SecsItemList("L2")
        Me.AddItem(l2_first)

        m_COMMACK = New SecsItemBinary("COMMACK")
        l2_first.AddItem(m_COMMACK)

        Dim l2_snd As SecsItemList = New SecsItemList("L2")
        l2_first.AddItem(l2_snd)

        m_MDLN = New SecsItemAscii("MDLN")
        l2_snd.AddItem(m_MDLN)

        m_SOFTREV = New SecsItemAscii("SOFTREV")
        l2_snd.AddItem(m_SOFTREV)

    End Sub

    Public ReadOnly Property COMMACK As COMMACK
        Get
            Return CType(m_COMMACK.Value(0), CellController.COMMACK)
        End Get
    End Property

    Public ReadOnly Property MDLN As String
        Get
            Return m_MDLN.Value
        End Get
    End Property

    Public ReadOnly Property SOFTREV As String
        Get
            Return m_SOFTREV.Value
        End Get
    End Property
End Class
