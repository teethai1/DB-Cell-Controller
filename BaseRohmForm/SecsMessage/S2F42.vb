Imports XtraLibrary.SecsGem

Public Class S2F42
    Inherits SecsMessageBase

    Private m_RootList As SecsItemList

    Private m_HCACK As SecsItemBinary
    Private m_HavingErrorParam As SecsItemList

    Public Sub New()
        MyBase.New(2, 42, False)

        m_RootList = New SecsItemList("L2")
        AddItem(m_RootList)

        m_HCACK = New SecsItemBinary("HCACK")
        m_RootList.AddItem(m_HCACK)

        m_HavingErrorParam = New SecsItemList("Ln")
        m_RootList.AddItem(m_HavingErrorParam)

    End Sub

    Public ReadOnly Property HCACK() As HCACK
        Get
            Return CType(m_HCACK.Value(0), HCACK)
        End Get
    End Property





End Class
