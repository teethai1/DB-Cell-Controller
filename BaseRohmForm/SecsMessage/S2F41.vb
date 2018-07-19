Imports XtraLibrary.SecsGem

Public Class S2F41
    Inherits SecsMessageBase

    Private m_L2 As SecsItemList
    Private m_RCMD As SecsItemAscii
    Private m_Ln As SecsItemList

    Public Sub New()
        MyBase.New(2, 41, True)

        m_L2 = New SecsItemList("L2")
        AddItem(m_L2)

        m_RCMD = New SecsItemAscii("RCMD")
        m_L2.AddItem(m_RCMD)

        m_Ln = New SecsItemList("Ln")
        m_L2.AddItem(m_Ln)

    End Sub

    Public Property RemoteCommand() As String
        Get
            Return m_RCMD.Value
        End Get
        Set(ByVal value As String)
            m_RCMD.Value = value
        End Set
    End Property

    Public Sub AddVariable(ByVal cpName As String, ByVal cpValue As String)
        Dim l2 As SecsItemList = New SecsItemList("L2")
        m_Ln.AddItem(l2)

        Dim l2_cpName As SecsItemAscii = New SecsItemAscii("CPNAME" & m_Ln.Value.Count.ToString())
        l2_cpName.Value = cpName
        l2.AddItem(l2_cpName)
        Dim l2_cpVal As SecsItemAscii = New SecsItemAscii("CPVAL" & m_Ln.Value.Count.ToString())
        l2_cpVal.Value = cpValue
        l2.AddItem(l2_cpVal)
    End Sub

    Public Sub AddVariable(ByVal cpValue As String)
        Dim l2 As SecsItemList = New SecsItemList("L1")
        m_Ln.AddItem(l2)
        Dim l2_cpVal As SecsItemAscii = New SecsItemAscii("CPVAL" & m_Ln.Value.Count.ToString())
        l2_cpVal.Value = cpValue
        l2.AddItem(l2_cpVal)
    End Sub

    Public Sub AddVariable(ByVal cpName As String, ByVal cpValue As UInteger)
        Dim l2 As SecsItemList = New SecsItemList("L2")
        m_Ln.AddItem(l2)

        Dim l2_cpName As SecsItemAscii = New SecsItemAscii("CPNAME" & m_Ln.Value.Count.ToString())
        l2_cpName.Value = cpName
        l2.AddItem(l2_cpName)
        Dim l2_cpVal As SecsItemU4 = New SecsItemU4("CPVAL" & m_Ln.Value.Count.ToString(), cpValue)
        l2.AddItem(l2_cpVal)
    End Sub

End Class
