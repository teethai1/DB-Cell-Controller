Imports XtraLibrary.SecsGem
Public Class S2F13
    Inherits SecsMessageBase

    Private m_Ln As SecsItemList

    Public Sub New()
        MyBase.New(2, 13, True)
        m_Ln = New SecsItemList("Ln")
        AddItem(m_Ln)
    End Sub

    Public Sub AddEcid(ByVal ecid As UInteger)
        Dim Type As New SecsDataType
        'If Type.ECID.GetType.Name = "UInt16" Then    '160803 \783 Type Common
        '    Dim ecidItem As SecsItemU2 = New SecsItemU2("ECID" & m_Ln.Value.Count.ToString(), CUShort(ecid))
        '    m_Ln.AddItem(ecidItem)
        'Else
        '    Dim ecidItem As SecsItemU4 = New SecsItemU4("ECID" & m_Ln.Value.Count.ToString(), ecid)
        '    m_Ln.AddItem(ecidItem)
        'End If

        If Type.ECID = ECIDSecsForm.U2 Then    '161222 \783 Config SECSGEM
            Dim ecidItem As SecsItemU2 = New SecsItemU2("ECID" & m_Ln.Value.Count.ToString(), CUShort(ecid))
            m_Ln.AddItem(ecidItem)
        Else
            Dim ecidItem As SecsItemU4 = New SecsItemU4("ECID" & m_Ln.Value.Count.ToString(), ecid)
            m_Ln.AddItem(ecidItem)
        End If



    End Sub


End Class
