Imports XtraLibrary.SecsGem

Public Class S1F3

    Inherits SecsMessageBase

    Private m_Ln As SecsItemList

    Public Sub New()
        MyBase.New(1, 3, True)
        m_Ln = New SecsItemList("Ln")
        AddItem(m_Ln)
    End Sub

    Public Sub AddSvid(ByVal svid As UInteger)
        sv.Add(svid.ToString)
        Dim svidItem As SecsItemU4 = New SecsItemU4("SVID" & m_Ln.Value.Count.ToString(), svid)
        m_Ln.AddItem(svidItem)
    End Sub

    Private _sv As New List(Of String)
    Public ReadOnly Property sv() As List(Of String)
        Get
            Return _sv
        End Get
    End Property

End Class
