Imports XtraLibrary.SecsGem

Public Class S2F37
    Inherits SecsMessageBase

    Private m_CEED As SecsItemBoolean
    Private m_Ln As SecsItemList


    Public Sub New()
        MyBase.New(2, 37, True)

        Dim l2 As SecsItemList = New SecsItemList("L2")
        Me.AddItem(l2)

        m_CEED = New SecsItemBoolean("CEED", True)
        l2.AddItem(m_CEED)

        m_Ln = New SecsItemList("Ln")
        l2.AddItem(m_Ln)

    End Sub

    Public Sub SetEnable(ByVal ParamArray ceidArray As UInt32())
        PrivateSet(True, ceidArray)
    End Sub

    Public Sub SetDisable(ByVal ParamArray ceidArray As UInt32())
        PrivateSet(False, ceidArray)
    End Sub

    Private Sub PrivateSet(ByVal ceed As Boolean, ByVal ParamArray ceidArray As UInt32())
        m_CEED.Value(0) = ceed
        m_Ln.Clear()
        For Each ceid As UInt32 In ceidArray
            m_Ln.AddItem(New SecsItemU4("CEID", ceid))
        Next
    End Sub

    Public Sub DisableAll()
        m_CEED.Value(0) = False
        m_Ln.Clear()
    End Sub

    Public Sub EnableAll()
        m_CEED.Value(0) = True
        m_Ln.Clear()
    End Sub

End Class
