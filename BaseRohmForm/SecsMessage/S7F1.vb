Imports XtraLibrary.SecsGem

Public Class S7F1
    Inherits SecsMessageBase

    Private m_L2 As SecsItemList
    Private m_PPID As SecsItemAscii
    Private m_Ln As SecsItemU4

    Public Sub New()
        MyBase.New(7, 1, True)

        m_L2 = New SecsItemList("L2")
        AddItem(m_L2)

        'm_PPID = New SecsItemAscii("RCMD", PPID)
        'm_L2.AddItem(m_PPID)

        'm_Ln = New SecsItemI1("Bin", CSByte(_binLength))
        'm_L2.AddItem(m_Ln)

    End Sub

    Public Sub Setparameter(ByVal strPPID As String, ByVal binLength As Integer)
        m_PPID = New SecsItemAscii("RCMD", strPPID)
        m_L2.AddItem(m_PPID)

        m_Ln = New SecsItemU4("Bin", CUInt(binLength))
        m_L2.AddItem(m_Ln)

    End Sub

End Class
