Public Class LinkedReport

    Private m_CEID As UInt32
    Public Property CEID() As UInt32
        Get
            Return m_CEID
        End Get
        Set(ByVal value As UInt32)
            m_CEID = value
        End Set
    End Property

    Private m_RPTIDList As List(Of UInt32)
    Public ReadOnly Property RPTIDList() As List(Of UInt32)
        Get
            Return m_RPTIDList
        End Get
    End Property

    Public Sub New()
        m_RPTIDList = New List(Of UInt32)
    End Sub

End Class
