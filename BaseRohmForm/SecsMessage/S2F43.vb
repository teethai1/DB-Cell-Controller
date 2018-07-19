Imports XtraLibrary.SecsGem
Public Class S2F43
    Inherits SecsMessageBase
    Private m_Lm As New SecsItemList("Lm")
   
    Public Sub New()
        MyBase.New(2, 43, True)
        AddItem(m_Lm)
    End Sub

    Public Sub AddStream(ByVal STRID As Byte, ByVal ParamArray FCNIDList As Byte())
        Dim m_L2 As New SecsItemList("L2")
        Dim m_Ln As New SecsItemList("Ln")
        Dim m_STRID As New SecsItemU1("STRID", STRID)
        Dim m_FCNID As SecsItemU1

        For i = 0 To FCNIDList.Count - 1
            m_FCNID = New SecsItemU1("FCNID", FCNIDList(i))
            m_Ln.AddItem(m_FCNID)
        Next

        m_L2.AddItem(m_STRID)
        m_L2.AddItem(m_Ln)
        m_Lm.AddItem(m_L2)
    End Sub

   

   



End Class
