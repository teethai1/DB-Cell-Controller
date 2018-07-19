Imports XtraLibrary.SecsGem

Public Class S5F3
    Inherits SecsMessageBase
    Public Sub New(ByVal Enable As Boolean, ByVal ALID As UInteger)
        MyBase.New(5, 3, True)


        Dim m_ALID As New SecsItemU4("ALID")
        If Not ALID = Nothing Then
            m_ALID = New SecsItemU4("ALID", ALID)
        End If



        Dim m_ALED As New SecsItemBinary("ALED", &H80)
        If Not Enable Then
            m_ALED.Value(0) = &H0
        End If
        Dim M_LIST As New SecsItemList("L2")
        AddItem(M_LIST)
        M_LIST.AddItem(m_ALED)
        M_LIST.AddItem(m_ALID)

    End Sub
End Class
