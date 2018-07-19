Imports XtraLibrary.SecsGem

Public Class S1F14
    Inherits SecsMessageBase

    Private m_L2 As SecsItemList
    Private m_COMMACK As SecsItemBinary
    Private m_L0 As SecsItemList


    Public Sub New()
        MyBase.New(1, 14, False)

        m_L2 = New SecsItemList("L2")
        AddItem(m_L2)

        m_COMMACK = New SecsItemBinary("COMMACK", &H0) 'OK only
        m_L2.AddItem(m_COMMACK)

        m_L0 = New SecsItemList("L0")
        m_L2.AddItem(m_L0)

    End Sub

 



End Class
