Public Class DefinedReport                                  '

    'Private m_RPTID As SecsDataType
    'Public Property RPTID() As SecsDataType
    '    Get

    '        Return m_RPTID
    '    End Get
    '    Set(ByVal value As SecsDataType)
    '        m_RPTID = value
    '    End Set
    'End Property



    'Private m_SVIDList As List(Of UInt32)
    'Public ReadOnly Property SVIDList() As List(Of UInt32)
    '    Get
    '        Return m_SVIDList
    '    End Get
    'End Property

    'Private m_SVIDListU2 As List(Of UInt16)      '160722 \783  common secs data type
    'Public ReadOnly Property SVIDListU2() As List(Of UInt16)
    '    Get
    '        Return m_SVIDListU2
    '    End Get
    'End Property

    Private m_RPTSecsType As SecsDataType
    Public Property RPTSecsType() As SecsDataType
        Get

            Return m_RPTSecsType
        End Get
        Set(ByVal value As SecsDataType)
            m_RPTSecsType = value
        End Set
    End Property






    Public Sub New()                            '160722 \783  common secs data type
        'm_SVIDList = New List(Of UInt32)
        'm_SVIDListU2 = New List(Of UInt16)
        'm_RPTID = New SecsDataType
        m_RPTSecsType = New SecsDataType
    End Sub

End Class
