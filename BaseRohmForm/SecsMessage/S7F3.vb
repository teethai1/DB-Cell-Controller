Imports XtraLibrary.SecsGem

Public Class S7F3
    Inherits SecsMessageBase

    Private m_L2 As SecsItemList
    Private m_PPID As SecsItemAscii
    Private m_Ln As SecsItemBinary

    Public Sub New()
        MyBase.New(7, 3, True)

        m_L2 = New SecsItemList("L2")
        AddItem(m_L2)

    End Sub

    Public Sub Setparameter(ByVal strPPID As String, ByVal binLength As Byte())
        m_PPID = New SecsItemAscii("RCMD", strPPID)
        m_L2.AddItem(m_PPID)
        m_Ln = New SecsItemBinary("Bin", binLength)
        m_L2.AddItem(m_Ln)

    End Sub

End Class
